using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger;

public class EmployeeService
{
    public readonly ShiftContext _context;

    public EmployeeService(ShiftContext shiftContext)
    {
        _context = shiftContext;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployees()
    {
        return await _context.Employees
            .Include(e => e.Shifts)
            .Select(e => EmployeeMapper.MapToDto(e))
            .ToListAsync();
    }

    public async Task<EmployeeDto?> GetEmployeeById(int id)
    {
        return await _context.Employees
            .Include(e => e.Shifts)
            .Where(e => e.EmployeeId == id)
            .Select(e => EmployeeMapper.MapToDto(e))
            .FirstOrDefaultAsync();
    }

    public async Task UpdateEmployee(Employee employee)
    {
        try
        {
            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task InsertEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeById(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}