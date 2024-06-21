using SharedLibrary.Models;

public interface IEmployeeService
{
    List<Employee> GetEmployees();
    Employee GetEmployee(int id);
    void AddEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(int id);
}