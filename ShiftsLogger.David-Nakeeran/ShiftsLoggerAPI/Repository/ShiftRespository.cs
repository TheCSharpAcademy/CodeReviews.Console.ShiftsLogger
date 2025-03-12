using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Repository;

public class ShiftRepository : Repository<Shift>, IShiftRepository
{

    public ShiftRepository(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<List<Shift>> GetAllWithEmployeeAsync()
    {
        return await _dbSet
                    .Include(s => s.Employee)
                    .ToListAsync();
    }

    public async Task<Shift> GetByIdWithEmployeeAsync(long id)
    {
        var shift = await _dbSet
            .Include(x => x.Employee)
            .SingleOrDefaultAsync(x => x.ShiftId == id);

        return shift;
    }

    public async Task<Shift> CreateWithEmployeeAsync(ShiftDTO entity)
    {
        var shift = new Shift
        {
            EmployeeId = entity.EmployeeId,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
        };
        await _dbSet.AddAsync(shift);
        await _context.SaveChangesAsync();

        var shiftWithEmployee = await _dbSet
            .Include(shift => shift.Employee)
            .SingleOrDefaultAsync(s => s.ShiftId == shift.ShiftId);

        return shiftWithEmployee;
    }

    public async Task<Shift> UpdateWithEmployeeAsync(long id, ShiftDTO entity)
    {
        var savedShift = await _dbSet
                    .Include(s => s.Employee)
                    .SingleOrDefaultAsync(x => x.ShiftId == id);

        _dbSet.Entry(savedShift).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return savedShift;
    }

    public async Task<bool> DeleteShiftAsync(long id)
    {
        return await base.DeleteAsync(id);
    }
}