using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Repository;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<List<Employee>> GetAllWithShiftsAsync()
    {
        try
        {
            return await _dbSet
                .Include(s => s.Shifts)
                .ToListAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while retrieving all employees", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching all employees", ex);
        }
    }

    public async Task<Employee> GetByIdWithShiftsAsync(long id)
    {
        try
        {
            var employee = await _dbSet
                .Include(x => x.Shifts)
                .SingleOrDefaultAsync(x => x.EmployeeId == id);

            if (employee == null)
            {
                return null;
            }
            return employee;
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while retrieving an employee", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error finding employee", ex);
        }

    }

    public async Task<Employee> UpdateWithShiftsAsync(long id, EmployeeDTO entity)
    {
        try
        {
            var savedEmployee = await _dbSet
                        .Include(s => s.Shifts)
                        .SingleOrDefaultAsync(x => x.EmployeeId == id);

            if (savedEmployee == null)
            {
                return null;
            }
            _context.Entry(savedEmployee).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return savedEmployee;

        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while updating an employee", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating the Employee", ex);
        }

    }

    public async Task<Employee> CreateWithShiftAsync(EmployeeDTO entity)
    {
        try
        {
            var employee = new Employee
            {
                Name = entity.Name
            };
            await _dbSet.AddAsync(employee);
            await _context.SaveChangesAsync();

            var employeeWithShifts = await _dbSet
                .Include(shift => shift.Shifts)
                .SingleOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (employeeWithShifts == null)
            {
                return null;
            }
            return employeeWithShifts;

        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while creating an employee", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating employee", ex);
        }
    }

    public async Task<bool> DeleteEmployeeAsync(long id)
    {
        return await base.DeleteAsync(id);
    }

    public async Task<bool> EmployeeExistsAsync(long id)
    {
        return await _dbSet.AnyAsync(e => e.EmployeeId == id);
    }
}