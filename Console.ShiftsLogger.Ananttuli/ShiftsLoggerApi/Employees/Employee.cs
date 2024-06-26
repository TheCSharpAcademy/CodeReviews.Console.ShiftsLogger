using ShiftsLoggerApi.Shifts;

namespace ShiftsLoggerApi.Employees;

public class Employee
{
    public int EmployeeId { get; set; }
    public required string Name { get; set; }

    public List<Shift> Shifts = [];

}