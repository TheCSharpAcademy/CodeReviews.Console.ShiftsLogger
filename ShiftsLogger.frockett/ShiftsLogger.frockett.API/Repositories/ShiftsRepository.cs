using Microsoft.EntityFrameworkCore;
using ShiftsLogger.frockett.API.Data;
using ShiftsLogger.frockett.API.Models;

namespace ShiftsLogger.frockett.API.Repositories;

public class ShiftsRepository : IShiftsRepository
{
    private readonly ShiftLogContext context;

    public ShiftsRepository(ShiftLogContext context)
    {
        this.context = context;
    }

    public async Task<Shift> AddShiftAsync(Shift shift)
    {
        await context.Shifts.AddAsync(shift);
        await context.SaveChangesAsync();
        return await context.Shifts
                            .Include(s => s.Employee)
                            .FirstOrDefaultAsync(s => s.Id == shift.Id);
    }
    public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
    {
        return await context.Shifts
            .Include(s => s.Employee)
            .OrderBy(s => s.StartTime).ToListAsync();
    }

    public async Task<Shift> GetShiftByIdAsync(int shiftId)
    {
        return await context.Shifts.FindAsync(shiftId);
    }
    public async Task<IEnumerable<Shift>> GetShiftsByEmployeeIdAsync(int employeeId) 
    { 
        return await context.Shifts
                            .Where(s => s.EmployeeId == employeeId)
                            .ToListAsync();
    }
    public async Task<Shift> UpdateShiftAsync(Shift shift)
    {
        context.Shifts.Update(shift);
        await context.SaveChangesAsync();
        return shift;
    }
    public async Task<bool> DeleteShiftAsync(int shiftId)
    {
        var shift = await context.Shifts.FindAsync(shiftId);
        if (shift != null)
        {
            context.Shifts.Remove(shift);
            await context.SaveChangesAsync();
            return true;
        }
        else return false;
    }
}
