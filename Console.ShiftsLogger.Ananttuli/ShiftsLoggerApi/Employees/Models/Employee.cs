using ShiftsLoggerApi.Shifts.Models;

namespace ShiftsLoggerApi.Employees.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public required string Name { get; set; }
    public List<Shift> Shifts { get; set; } = [];
}