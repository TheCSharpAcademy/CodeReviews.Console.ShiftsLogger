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
        try
        {
            return await _dbSet
                        .Include(s => s.Employee)
                        .ToListAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while retrieving all shifts", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching all shifts", ex);
        }
    }

    public async Task<Shift> GetByIdWithEmployeeAsync(long id)
    {
        try
        {
            var shift = await _dbSet
                .Include(x => x.Employee)
                .SingleOrDefaultAsync(x => x.ShiftId == id);

            if (shift == null)
            {
                return null;
            }
            return shift;

        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred retrieving a shift", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error finding shift", ex);
        }
    }

    public async Task<Shift> CreateWithEmployeeAsync(ShiftDTO entity)
    {
        try
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

            if (shiftWithEmployee == null)
            {
                return null;
            }
            return shiftWithEmployee;

        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while creating a shift", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating shift", ex);
        }
    }

    public async Task<Shift> UpdateWithEmployeeAsync(long id, ShiftDTO entity)
    {
        try
        {
            var savedShift = await _dbSet
                        .Include(s => s.Employee)
                        .SingleOrDefaultAsync(x => x.ShiftId == id);

            if (savedShift == null)
            {
                return null;
            }
            _dbSet.Entry(savedShift).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return savedShift;

        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while updating shift", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating the shift", ex);
        }
    }

    public async Task<bool> DeleteShiftAsync(long id)
    {
        return await base.DeleteAsync(id);
    }
}