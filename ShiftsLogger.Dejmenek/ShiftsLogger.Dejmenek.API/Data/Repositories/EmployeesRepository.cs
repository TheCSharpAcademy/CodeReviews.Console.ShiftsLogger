using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Dejmenek.API.Data.Interfaces;
using ShiftsLogger.Dejmenek.API.Helpers;
using ShiftsLogger.Dejmenek.API.Models;

namespace ShiftsLogger.Dejmenek.API.Data.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly ShiftsContext _shiftsContext;

    public EmployeesRepository(ShiftsContext shiftsContext)
    {
        _shiftsContext = shiftsContext;
    }

    public async Task AddEmployeeAsync(EmployeeCreateDTO employeeDto)
    {
        var employee = Mapper.FromEmployeeCreateDto(employeeDto);

        await _shiftsContext.Employees.AddAsync(employee);
        await _shiftsContext.SaveChangesAsync();
    }

    public async Task<int> DeleteEmployeeAsync(int employeeId)
    {
        var employee = await _shiftsContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee is null)
        {
            return -1;
        }

        _shiftsContext.Employees.Remove(employee);

        return await _shiftsContext.SaveChangesAsync();
    }

    public async Task<List<EmployeeReadDTO>> GetEmployeesAsync()
    {
        var employees = await _shiftsContext.Employees.Include(e => e.Shifts).ToListAsync();

        return Mapper.ToEmployeeReadDtoList(employees);
    }

    public async Task<List<ShiftReadDTO>?> GetEmployeeShiftsAsync(int employeeId)
    {
        var employee = await _shiftsContext.Employees.Include(e => e.Shifts).FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee?.Shifts is null)
        {
            return null;
        }

        var employeeReadDto = Mapper.ToEmployeeReadDto(employee);

        return employeeReadDto.Shifts;
    }

    public async Task<int> UpdateEmployeeAsync(int employeeId, EmployeeUpdateDTO employeeDto)
    {
        var oldEmployee = await _shiftsContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

        if (oldEmployee is null)
        {
            return -1;
        }

        oldEmployee.FirstName = employeeDto.FirstName;
        oldEmployee.LastName = employeeDto.LastName;

        return await _shiftsContext.SaveChangesAsync();
    }

    public async Task<bool> EmployeeExists(int employeeId)
    {
        return await _shiftsContext.Employees.AnyAsync(e => e.Id == employeeId);
    }
}
