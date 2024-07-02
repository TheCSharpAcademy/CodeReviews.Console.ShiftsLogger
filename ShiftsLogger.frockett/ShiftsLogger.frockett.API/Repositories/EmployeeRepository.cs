using ShiftsLogger.frockett.API.Data;
using ShiftsLogger.frockett.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.frockett.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ShiftLogContext context;

    public EmployeeRepository(ShiftLogContext context)
    {
        this.context = context;
    }

    public async Task<Employee> AddEmployeeAsync(Employee employee)
    {
        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await context.Employees
                            .Include(e => e.Shifts)
                            .OrderBy(e => e.Name)
                            .ToListAsync();
    }
    public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
        return await context.Employees
                            .Include(e => e.Shifts)
                            .FirstOrDefaultAsync(e => e.Id == employeeId);
    }
    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        var employeeToUpdate = await context.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);

        // Currently only employee name can be updated.
        if (employee.Name != null)
        {
            employeeToUpdate.Name = employee.Name;
            await context.SaveChangesAsync();
        }
        return employeeToUpdate;
    }
    public async Task DeleteEmployeeAsync(int employeeId)
    {
        var employeeToDelete = await context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employeeToDelete != null)
        {
            context.Employees.Remove(employeeToDelete);
            await context.SaveChangesAsync();
        }
    }
}
