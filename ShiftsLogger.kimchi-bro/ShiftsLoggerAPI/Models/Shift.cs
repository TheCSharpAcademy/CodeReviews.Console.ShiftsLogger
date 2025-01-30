namespace ShiftsLoggerAPI.Models;

public class Shift
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }

    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
