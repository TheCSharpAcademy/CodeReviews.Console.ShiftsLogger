using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Interfaces;

public interface IEmployeeRepository
{
    Task<ICollection<Employee>> GetEmployeesAsync();
    Task<Employee> FindEmployeeAsync(int id);
    Task<Employee> FindEmployeeAsync(string name);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task<string> DeleteEmployeeAsync(int id);
    Task<Employee> UpdateEmployeeAsync(int id, Employee updatedEmployee);
}
