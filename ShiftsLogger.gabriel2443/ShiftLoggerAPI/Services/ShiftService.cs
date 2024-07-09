using Microsoft.EntityFrameworkCore;
using ShiftLoggerAPI.Data;
using ShiftLoggerAPI.Models;

namespace ShiftLoggerAPI.Services;

public class ShiftService : IShiftService
{
    private readonly DataContext _context;

    public ShiftService(DataContext context)
    {
        _context = context;
    }

    public async Task CreateShift(Shift shift)
    {
        shift.Duration = shift.EndTime - shift.StartTime;
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Shift>> GetShifts()
    {
        var shifts = await _context.Shifts.ToListAsync();
        return shifts;
    }

    public async Task<Shift> GetShiftById(int id)
    {
        return await _context.Shifts.FindAsync(id);
    }

    public async Task UpdateShift(int id, Shift updatedShift)
    {
        var shiftDb = await _context.Shifts.FindAsync(id);
        if (id != updatedShift.Id) Console.WriteLine("Id not found");

        if (shiftDb is null) Console.WriteLine("Shift not found");

        shiftDb.FullName = updatedShift.FullName;
        shiftDb.StartTime = updatedShift.StartTime;
        shiftDb.EndTime = updatedShift.EndTime;
        shiftDb.Duration = updatedShift.EndTime - updatedShift.StartTime;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteShift(int id)
    {
        var shiftDb = await _context.Shifts.FindAsync(id);
        if (shiftDb is null) Console.WriteLine("Shift not found");

        _context.Shifts.Remove(shiftDb);
        await _context.SaveChangesAsync();
    }
}