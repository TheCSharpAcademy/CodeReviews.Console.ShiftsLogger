using Microsoft.EntityFrameworkCore;
using ShiftsLogger.KamilKolanowski.Models;
using ShiftsLogger.KamilKolanowski.Models.Data;

namespace ShiftsLoggerAPI.Services;

internal class ShiftDbService
{
    private readonly ShiftsLoggerDbContext _context;

    public ShiftDbService(ShiftsLoggerDbContext context)
    {
        _context = context;
    }

    internal async Task AddShiftAsync(Shift shift)
    {
        try
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add Shift due to: {ex.Message}");
        }
    }

    internal async Task UpdateShiftAsync(Shift shift)
    {
        try
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to edit Shift due to: {ex.Message}");
        }
    }

    internal async Task DeleteShiftAsync(int shiftId)
    {
        try
        {
            var shift = await _context.Shifts.FindAsync(shiftId);
            if (shift != null)
            {
                _context.Shifts.Remove(shift);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete Shift due to: {ex.Message}");
        }
    }

    internal async Task<Shift?> ReadShiftAsync(int id)
    {
        try
        {
            return await _context.Shifts.FirstOrDefaultAsync(shift => shift.ShiftId == id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read Shift: {ex.Message}");
            return null;
        }
    }

    internal async Task<List<Shift>> ReadAllShiftsAsync()
    {
        try
        {
            return await _context.Shifts.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read all Shifts: {ex.Message}");
            return new List<Shift>();
        }
    }
}
