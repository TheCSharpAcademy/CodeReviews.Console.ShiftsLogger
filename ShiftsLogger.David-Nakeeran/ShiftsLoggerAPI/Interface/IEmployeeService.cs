using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Interface;

public interface IEmployeeService
{
    public Task<ServiceResponse<List<Employee>>> GetAllEmployeesAsync();

    public Task<ServiceResponse<Employee>> GetEmployeeByIdAsync(long id);

    public Task<ServiceResponse<Employee>> CreateEmployee(EmployeeDTO employeeDTO);

    public Task<ServiceResponse<Employee>> UpdateEmployee(long id, EmployeeDTO employeeDTO);

    public Task<ServiceResponse<bool>> DeleteEmployee(long id);
}