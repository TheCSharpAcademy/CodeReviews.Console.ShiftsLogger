using Microsoft.EntityFrameworkCore;

using API.Models;

namespace API.Services;

public class EmployeeService
{
    private ShiftsLoggerContext _context;

    public EmployeeService(ShiftsLoggerContext context)
	{
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployeeAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }
}
