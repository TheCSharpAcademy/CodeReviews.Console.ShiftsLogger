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
        return await _dbSet
            .Include(s => s.Shifts)
            .ToListAsync();
    }

    public async Task<Employee> GetByIdWithShiftsAsync(long id)
    {
        var employee = await _dbSet
            .Include(x => x.Shifts)
            .SingleOrDefaultAsync(x => x.EmployeeId == id);

        return employee;
    }

    public async Task<Employee> UpdateWithShiftsAsync(long id, EmployeeDTO entity)
    {
        var savedEmployee = await _dbSet
                    .Include(s => s.Shifts)
                    .SingleOrDefaultAsync(x => x.EmployeeId == id);

        _context.Entry(savedEmployee).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return savedEmployee;
    }

    public async Task<Employee> CreateWithShiftAsync(EmployeeDTO entity)
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

        return employeeWithShifts;
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