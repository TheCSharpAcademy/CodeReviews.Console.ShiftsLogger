using SharedLibrary.Models;

public interface IEmployeeService
{
    List<Employee> GetAllEmployees();
    Employee GetEmployee(int id);
    void CreateEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(int id);
}