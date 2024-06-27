using ShiftsLoggerApi.Employees.Models;

namespace ShiftsLoggerApi.Shifts.Models;

public class ShiftDto
{
    public int ShiftId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public EmployeeDto? Employee { get; set; }

    public ShiftDto(
        int shiftId,
        DateTime startTime,
        DateTime endTime,
        EmployeeDto? employee = null
    )
    {
        ShiftId = shiftId;
        StartTime = startTime;
        EndTime = endTime;
        Employee = employee;
    }
}