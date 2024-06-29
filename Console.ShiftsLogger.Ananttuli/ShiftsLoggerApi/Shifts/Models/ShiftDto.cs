using ShiftsLoggerApi.Employees.Models;

namespace ShiftsLoggerApi.Shifts.Models;

public class ShiftDto
{
    public int ShiftId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public EmployeeCoreDto Employee { get; set; }

    public ShiftDto(
        int shiftId,
        DateTime startTime,
        DateTime endTime,
        EmployeeCoreDto employee
    )
    {
        ShiftId = shiftId;
        StartTime = startTime;
        EndTime = endTime;
        Employee = employee;
    }
}