namespace ShiftsLoggerAPI.Models;

public class Shift
{
    public long ShiftId { get; set; }
    public long EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Comment { get; set; } = "";
    public TimeSpan Duration { get; set; }
}