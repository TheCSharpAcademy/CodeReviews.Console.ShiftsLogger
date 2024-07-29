using ShiftsLogger.Dejmenek.UI.Data.Interfaces;
using ShiftsLogger.Dejmenek.UI.Helpers;
using ShiftsLogger.Dejmenek.UI.Models;
using ShiftsLogger.Dejmenek.UI.Services.Interfaces;
using Spectre.Console;

namespace ShiftsLogger.Dejmenek.UI.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IUserInteractionService _userInteractionService;

    public EmployeeService(IEmployeesRepository employeesRepository, IUserInteractionService userInteractionService)
    {
        _employeesRepository = employeesRepository;
        _userInteractionService = userInteractionService;
    }

    public async Task AddEmployeeAsync()
    {
        string firstName = _userInteractionService.GetFirstName();
        string lastName = _userInteractionService.GetLastName();

        var employeeDto = new EmployeeDTO { FirstName = firstName, LastName = lastName };

        await _employeesRepository.AddEmployeeAsync(employeeDto);
    }

    public async Task DeleteEmployeeAsync()
    {
        List<Employee>? employees = await _employeesRepository.GetAllEmployeesAsync();

        if (employees is null)
        {
            return;
        }

        if (employees.Count == 0)
        {
            AnsiConsole.MarkupLine("There are currently no employees. Please add one before deleting an employee");
            return;
        }

        var employee = _userInteractionService.GetEmployee(employees);

        await _employeesRepository.DeleteEmployeeAsync(employee.Id);
    }

    public async Task<List<EmployeeReadDTO>> GetAllEmployeesAsync()
    {
        var employees = await _employeesRepository.GetAllEmployeesAsync();

        if (employees is null)
        {
            return [];
        }

        return Mapper.ToEmployeeReadDtoList(employees);
    }

    public async Task<List<ShiftReadDTO>?> GetEmployeeShiftsAsync()
    {
        var employees = await _employeesRepository.GetAllEmployeesAsync();

        if (employees is null)
        {
            return null;
        }

        if (employees.Count == 0)
        {
            AnsiConsole.MarkupLine("There are no employees.");
            return null;
        }

        int employeeId = _userInteractionService.GetEmployee(employees).Id;
        List<Shift>? shifts = await _employeesRepository.GetEmployeeShiftsAsync(employeeId);

        if (shifts is null)
        {
            return null;
        }

        return Mapper.ToShiftReadDtoList(shifts);
    }

    public async Task UpdateEmployeeAsync()
    {
        List<Employee>? employees = await _employeesRepository.GetAllEmployeesAsync();

        if (employees is null)
        {
            return;
        }

        if (employees.Count == 0)
        {
            AnsiConsole.MarkupLine("There are currently no employees. Please add one before updating an employee");
            return;
        }

        var employee = _userInteractionService.GetEmployee(employees);
        EmployeeDTO updatedEmployee = Mapper.ToEmployeeDto(employee);

        if (_userInteractionService.GetConfirmation("Do you want to change first name?"))
        {
            updatedEmployee.FirstName = _userInteractionService.GetFirstName();
        }

        if (_userInteractionService.GetConfirmation("Do you want to change last name?"))
        {
            updatedEmployee.LastName = _userInteractionService.GetLastName();
        }

        await _employeesRepository.UpdateEmployeeAsync(employee.Id, updatedEmployee);
    }
}
