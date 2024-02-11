using ShiftsLogger.frockett.API.DTOs;
using ShiftsLogger.frockett.API.Models;
using ShiftsLogger.frockett.API.Repositories;

namespace ShiftsLogger.frockett.API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateDto employeeToCreate)
    {
        Employee employee = new Employee
        {
            Name = employeeToCreate.Name
        };

        await employeeRepository.AddEmployeeAsync(employee);

        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name
        };
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(int employeeId)
    {
        Employee employee = await employeeRepository.GetEmployeeByIdAsync(employeeId);

        EmployeeDto employeeDto = new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Shifts = GetShiftDtoList(employee.Shifts)
        };

        return employeeDto;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        IEnumerable<Employee> employees = await employeeRepository.GetAllEmployeesAsync();
        List<EmployeeDto> result = new List<EmployeeDto>();
        // TODO convert employees to employeeDto
        foreach (var employee in employees)
        {
            result.Add(new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Shifts = GetShiftDtoList(employee.Shifts)
            });
        }
        return result;
    }

    public async Task UpdateEmployeeAsync(EmployeeDto employeeToUpdate)
    {
        Employee employee = new Employee
        {
            Id = employeeToUpdate.Id,
            Name = employeeToUpdate.Name,
            // Currently no need to update the list of shift; it will be unchanged
        };
        await employeeRepository.UpdateEmployeeAsync(employee);
    }

    public async Task DeleteEmployeeAsync(EmployeeDto employeeToDelete)
    {
        await employeeRepository.DeleteEmployeeAsync(employeeToDelete.Id);
    }

    private List<ShiftDto> GetShiftDtoList(List<Shift> shifts)
    {
        List<ShiftDto> shiftDtos = new List<ShiftDto>();

        foreach (Shift shift in shifts)
        {
            shiftDtos.Add(new ShiftDto
            {
                Id = shift.Id,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                Duration = shift.Duration,
                EmployeeId = shift.EmployeeId,
                EmployeeName = shift.Employee.Name
            });
        }
        return shiftDtos;
    }
}
