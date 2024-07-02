using SharedLibrary.DTOs;

public interface IEmployeeService
{
    List<EmployeeDto> GetAllEmployees();
    EmployeeDto GetEmployee(int id);
    void CreateEmployee(CreateEmployeeDto employee);
    void UpdateEmployee(UpdateEmployeeDto employee, int id);
    void DeleteEmployee(int id);
}