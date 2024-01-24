namespace ShiftsLoggerWebApi.Models;

public class EmployeeDto()
{
    public int EmployeeId {get; set;}
    public string Name {get; set;} = "";
    public static EmployeeDto FromEmployee(Employee employee)
    {
        var employeeDto = new EmployeeDto
        {
            EmployeeId = employee.EmployeeId,
            Name = employee.Name
        };
        return employeeDto;
    }
}