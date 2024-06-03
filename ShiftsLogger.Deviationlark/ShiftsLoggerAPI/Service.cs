using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Controllers;

namespace ShiftsLogger;
public class Service
{
    private readonly ShiftsContext _context;

    public Service(ShiftsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShiftModel>> GetShiftsAsync()
    {
        return await _context.Shifts.ToListAsync();
    }
    public async Task<ShiftModel> GetShiftAsync(long id)
    {
        return await _context.Shifts.FindAsync(id);
    }
    public async Task<bool> PutShiftAsync(long id, ShiftModel shiftModel)
    {
        if (id != shiftModel.Id)
        {
            return false;
        }

        _context.Entry(shiftModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShiftModelExists(id))
            {
                return false;
            }
            else
            {
                throw;
            }
        }
        return true;
    }
    public async Task<ShiftModel> CreateShiftAsync(ShiftModel shiftModel)
    {
        _context.Shifts.Add(shiftModel);
        await _context.SaveChangesAsync();

        return shiftModel;
    }
    public async Task<bool> DeleteShiftAsync(long id)
    {
        var shiftModel = await _context.Shifts.FindAsync(id);
        if (shiftModel == null)
        {
            return false;
        }

        _context.Shifts.Remove(shiftModel);
        await _context.SaveChangesAsync();

        return true;
    }
    public bool ShiftModelExists(long id)
    {
        return _context.Shifts.Any(e => e.Id == id);
    }
}