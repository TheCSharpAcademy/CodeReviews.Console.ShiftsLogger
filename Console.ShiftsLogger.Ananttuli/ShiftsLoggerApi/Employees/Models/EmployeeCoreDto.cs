namespace ShiftsLoggerApi.Employees.Models;

public class EmployeeCoreDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }

    public EmployeeCoreDto(int employeeId, string name)
    {
        EmployeeId = employeeId;
        Name = name;
    }
}
