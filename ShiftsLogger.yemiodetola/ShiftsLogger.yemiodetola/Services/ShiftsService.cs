using Microsoft.EntityFrameworkCore;
using ShfitsLogger.yemiodetola.Contexts;
using ShfitsLogger.yemiodetola.Models;

namespace ShfitsLogger.yemiodetola.Services;


public interface IShiftService
{
  Task<IEnumerable<Shift>> GetAllShiftsAsync();
  Task<Shift?> GetShiftByIdAsync(int id);
  Task<Shift> CreateShiftAsync(Shift shift);
  Task<Shift?> UpdateShiftAsync(int id, Shift shift);
  Task<bool> DeleteShiftAsync(int id);
}
public class ShiftsService : IShiftService
{
  private readonly ShiftsContext _context;

  public ShiftsService(ShiftsContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
  {
    return await _context.Shifts
            .OrderByDescending(s => s.StartedAt)
            .ToListAsync();
  }

  public async Task<Shift?> GetShiftByIdAsync(int id)
  {
    return await _context.Shifts.FindAsync(id);
  }

  public async Task<Shift> CreateShiftAsync(Shift shift)
  {
    if (shift.FinishedAt <= shift.StartedAt)
    {
      throw new ArgumentException("Finish time must be after start time");
    }

    _context.Shifts.Add(shift);
    await _context.SaveChangesAsync();
    return shift;
  }


  public async Task<Shift?> UpdateShiftAsync(int id, Shift shift)
  {
    var existingShift = await _context.Shifts.FindAsync(id);
    if (existingShift == null)
    {
      return null;
    }

    if (shift.FinishedAt <= shift.StartedAt)
    {
      throw new ArgumentException("Finish time must be after start time");
    }

    existingShift.Name = shift.Name;
    existingShift.StartedAt = shift.StartedAt;
    existingShift.FinishedAt = shift.FinishedAt;

    await _context.SaveChangesAsync();
    return existingShift;
  }

  public async Task<bool> DeleteShiftAsync(int id)
  {
    var shift = await _context.Shifts.FindAsync(id);
    if (shift == null)
    {
      return false;
    }

    _context.Shifts.Remove(shift);
    await _context.SaveChangesAsync();
    return true;
  }

}
