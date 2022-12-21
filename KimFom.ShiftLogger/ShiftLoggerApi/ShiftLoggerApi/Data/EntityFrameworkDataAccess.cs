using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.DataContext;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Data;

public class EntityFrameworkDataAccess : IDataAccess
{
    private readonly ShiftContext _shiftContext;

    public EntityFrameworkDataAccess(ShiftContext shiftContext)
    {
        _shiftContext = shiftContext;
    }
    
    public async Task<List<Shift>> GetShiftsAsync()
    {
        return await _shiftContext.Shifts.ToListAsync();
    }

    public async Task<Shift> GetShiftByIdAsync(int id)
    {
        return await _shiftContext.Shifts.FirstAsync(x => x.Id == id);
    }

    public async Task AddShiftAsync(Shift shift)
    {
        await _shiftContext.AddAsync(shift);
        await _shiftContext.SaveChangesAsync();
    }

    public async Task UpdateShiftAsync(int id, Shift shift)
    {
        var oldShift = await GetShiftByIdAsync(id);
        oldShift.EndTime = shift.EndTime;
        oldShift.Duration = shift.Duration;
        await _shiftContext.SaveChangesAsync();
    }

    public async Task DeleteShiftAsync(int id)
    {
        var shift = await GetShiftByIdAsync(id);
        _shiftContext.Remove(shift);
        await _shiftContext.SaveChangesAsync();
    }
}