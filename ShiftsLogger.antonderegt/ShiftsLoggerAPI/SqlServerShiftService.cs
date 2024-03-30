using Microsoft.EntityFrameworkCore;
using ShiftsLoggerLibrary.DTOs;
using ShiftsLoggerLibrary.Models;

namespace ShiftsLoggerAPI;

public class SqlServerShiftService(ShiftsContext context) : IShiftService
{
    private readonly ShiftsContext _context = context;

    public async Task<List<Shift>> GetShiftsAsync()
    {
        return await _context.Shifts.ToListAsync();
    }

    public async Task<List<Shift>> GetShiftsByEmployeeIdAsync(int id)
    {
        return await _context.Shifts.Where(shift => shift.EmployeeId == id).ToListAsync();
    }

    public async Task<Shift?> GetRunningShiftsByEmployeeIdAsync(int id)
    {
        return await _context.Shifts
                        .Where(shift => shift.EmployeeId == id)
                        .Where(shift => shift.EndTime == null)
                        .FirstOrDefaultAsync();
    }

    public async Task<Shift?> GetShiftByIdAsync(int id)
    {
        return await _context.Shifts.FirstOrDefaultAsync(shift => shift.Id == id);
    }

    public async Task<Shift?> StartShiftAsync(StartShift startShift)
    {
        bool employeeIsOnShift = await _context.Shifts.AnyAsync(shift => shift.EmployeeId == startShift.EmployeeId && shift.EndTime == null);

        if (employeeIsOnShift)
        {
            return null;
        }

        Shift newShift = new() { EmployeeId = startShift.EmployeeId, StartTime = startShift.StartTime };
        await _context.Shifts.AddAsync(newShift);
        await _context.SaveChangesAsync();

        return newShift;
    }

    public async Task<Shift?> EndShiftAsync(EndShift endShift)
    {
        Shift? shift = await _context.Shifts.FirstOrDefaultAsync(shift => shift.Id == endShift.Id && shift.EndTime == null);

        if (shift == null)
        {
            return null;
        }

        if (endShift.EndTime < shift.StartTime)
        {
            return null;
        }

        shift.EndTime = endShift.EndTime;
        await _context.SaveChangesAsync();

        return shift;
    }

    public async Task<Shift?> UpdateShiftAsync(Shift updatedShift)
    {
        Shift? shift = await _context.Shifts.FirstOrDefaultAsync(shift => shift.Id == updatedShift.Id);

        if (shift != null)
        {
            shift.StartTime = updatedShift.StartTime;
            shift.EndTime = updatedShift.EndTime;
            shift.EmployeeId = updatedShift.EmployeeId;
        }

        await _context.SaveChangesAsync();
        return shift;
    }

    public async Task<bool> DeleteShiftByIdAsync(int id)
    {
        Shift? shift = await _context.Shifts.FirstOrDefaultAsync(shift => shift.Id == id);

        if (shift != null)
        {
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}