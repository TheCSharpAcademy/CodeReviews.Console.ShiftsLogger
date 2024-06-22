using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.DataAccess;
using ShiftsLoggerApi.Models;

namespace ShiftsLoggerApi.Services;

public class ShiftService : IShiftService
{
    private readonly ShiftContext _context;

    public ShiftService(ShiftContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShiftModel>> GetShiftsAsync()
    {
        return await _context.ShiftModels.ToListAsync();
    }

    public async Task<ShiftModel> GetShiftByIdAsync(int id)
    {
        return await _context.ShiftModels.FindAsync(id);
    }

    public async Task<ShiftModel> CreateShiftAsync(ShiftModel shiftModel)
    {
        _context.ShiftModels.Add(shiftModel);
        await _context.SaveChangesAsync();
        return shiftModel;
    }

    public async Task UpdateShiftAsync(int id, ShiftModel shiftModel)
    {
        _context.Entry(shiftModel).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteShiftAsync(int id)
    {
        var shiftModel = await _context.ShiftModels.FindAsync(id);
        if (shiftModel != null)
        {
            _context.ShiftModels.Remove(shiftModel);
            await _context.SaveChangesAsync();
        }
    }
}