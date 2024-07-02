using ShiftsLogger.frockett.API.Models;

namespace ShiftsLogger.frockett.API.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> AddEmployeeAsync(Employee employee);
    Task<Employee> UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(int employeeId);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int employeeId);
}
