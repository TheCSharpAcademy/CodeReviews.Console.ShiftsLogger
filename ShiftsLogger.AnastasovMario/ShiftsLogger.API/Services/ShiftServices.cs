using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Contracts;
using ShiftsLogger.API.Data;
using ShiftsLogger.API.Models.Shifts;

namespace ShiftsLogger.API.Services
{
  public class ShiftServices(ApplicationDbContext context) : IShiftServices
  {
    private readonly ApplicationDbContext _context = context;

    public async Task AddShiftAsync(Shift newShift)
    {
      var shift = new Shift()
      {
        WorkerId = newShift.WorkerId,
        Start = newShift.Start,
        End = newShift.End,
      };

      await _context.AddAsync(shift);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteShiftAsync(int id)
    {
      var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Shift with this Id doesn't exists");

      _context.Shifts.Remove(shift);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ShiftDto>> GetAllShiftsAsync()
    {
      return await _context.Shifts
        .Select(s => new ShiftDto
        {
          Worker = s.Worker!.FirstName + " " + s.Worker.LastName,
          StartShift = s.Start.ToString("MM/dd/yyyy HH:mm"),
          EndShift = s.End.ToString("MM/dd/yyyy HH:mm")
        })
        .ToListAsync();
    }

    public async Task<ShiftDto> GetShiftByIdAsync(int id)
    {
      var shift = await _context.Shifts
        .Where(p => p.Id == id)
        .Select(s => new ShiftDto
        {
          Worker = s.Worker.FirstName + " " + s.Worker.LastName,
          StartShift = s.Start.ToString(),
          EndShift = s.End.ToString(),
        })
        .FirstOrDefaultAsync();

      if (shift == null)
      {
        throw new Exception("Shift with this Id doesn't exist");
      }

      return shift;
    }

    public async Task UpdateShiftAsync(int shiftId, ShiftEditDto updatedShift)
    {
      var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.Id == shiftId) ?? throw new Exception("Shift with this Id doesn't exist");

      shift.Start = updatedShift.StartShift;
      shift.End = updatedShift.EndShift;

      await _context.SaveChangesAsync();
    }
  }
}
