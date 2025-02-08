using ShiftsLoggerWebAPI.DTOs;

namespace ShiftsLoggerWebAPI.Services;

public interface IEmployeeService
{
    public List<EmployeeDto>? GetAllEmployees();
    public EmployeeDto? GetEmployeeById(int id);
    public string? CreateEmployee(EmployeeDto employee);
    public string? UpdateEmployee(EmployeeDto updatedEmployee);
    public string? DeleteEmployee(int id);
}