using ShiftsLoggerApi.Shifts.Models;

namespace ShiftsLoggerApi.Employees.Models;

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public List<ShiftDto>? Shifts { get; set; }

    public EmployeeDto(int employeeId, string name, List<ShiftDto>? shifts = null)
    {
        EmployeeId = employeeId;
        Name = name;
        Shifts = shifts;
    }
}
