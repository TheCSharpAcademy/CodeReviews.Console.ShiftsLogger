using Microsoft.EntityFrameworkCore;
using ShiftApi.DTOs;
using ShiftApi.Models;

namespace ShiftApi.Service;

public class ShiftService
{
    private readonly ShiftApiContext _context;

    public ShiftService(ShiftApiContext context)
    {
        _context = context;
    }

    public async Task<List<ShiftDTO>> GetAllShifts()
    {
        var shifts = await _context.Shifts
            .Include(s => s.Employee)
            .Select(s => new ShiftDTO
            {
                ShiftId = s.ShiftId,
                ShiftStartTime = s.ShiftStartTime,
                ShiftEndTime = s.ShiftEndTime
            })
            .ToListAsync();

        return shifts;
    }

    public async Task<ShiftDTO?> GetById(int id)
    {
        var shift = await _context.Shifts
            .Include(s => s.Employee)
            .FirstOrDefaultAsync(s => s.ShiftId == id);

        var shiftDTO = new ShiftDTO
        {
            ShiftId = shift.ShiftId,
            ShiftStartTime = shift.ShiftStartTime,
            ShiftEndTime = shift.ShiftEndTime
        };
        return shiftDTO;
    }

    public async Task Post(Shift shift)
    {
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();
    }

    public async Task Put(int id, ShiftDTO shiftDTO)
    {
        var shift = await _context.Shifts.FindAsync(id);

        shift.ShiftStartTime = shiftDTO.ShiftStartTime;
        shift.ShiftEndTime = shiftDTO.ShiftEndTime;

        _context.Shifts.Update(shift);

        await _context.SaveChangesAsync();
    }

    public async Task Delete(Shift shift)
    {
        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();
    }

    internal bool ShiftExists(Shift shift)
    {
        return _context.Shifts.Any(s => s.ShiftId == shift.ShiftId);
    }

    internal async Task<Shift?> FindAsync(int id)
    {
        var shift = await _context.Shifts.FindAsync(id);
        return shift;
    }
}
