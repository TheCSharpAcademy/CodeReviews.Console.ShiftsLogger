using System.ComponentModel.DataAnnotations.Schema;
using ShiftsLoggerApi.Employees;

namespace ShiftsLoggerApi.Shifts;

public class Shift
{
    private Employee? _employee;
    public int ShiftId { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public int EmployeeId { get; set; }

    public Employee Employee
    {
        get => _employee ??
            throw new InvalidOperationException("Employee not initialized");
        set => _employee = value;
    }

    [NotMapped]
    public TimeSpan Duration
    {
        get
        {
            return EndTime.Subtract(StartTime);
        }
    }
}