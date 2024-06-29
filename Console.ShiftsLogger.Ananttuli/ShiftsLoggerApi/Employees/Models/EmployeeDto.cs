using ShiftsLoggerApi.Shifts.Models;

namespace ShiftsLoggerApi.Employees.Models;

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public List<ShiftCoreDto> Shifts { get; set; }

    public EmployeeDto(int employeeId, string name, List<ShiftCoreDto> shifts)
    {
        EmployeeId = employeeId;
        Name = name;
        Shifts = shifts;
    }
}
