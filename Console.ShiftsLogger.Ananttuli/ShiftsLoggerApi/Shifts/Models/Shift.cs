using System.ComponentModel.DataAnnotations.Schema;
using ShiftsLoggerApi.Employees.Models;

namespace ShiftsLoggerApi.Shifts.Models;

public class Shift
{
    public int ShiftId { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    public static Func<Shift, bool> IsShiftTimeOverlapping(DateTime start, DateTime end)
    {
        return (Shift s) =>
            !(
                ((start < s.StartTime) && (end <= s.StartTime)) ||
                ((start >= s.EndTime) && (end > s.EndTime))
            );
    }
}