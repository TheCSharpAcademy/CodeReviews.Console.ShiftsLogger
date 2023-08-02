namespace ShiftLoggerConsole.Models;

public class ShiftModel
{
    public long Id { get; set; }
    public int Day { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration { get; set; }
    public int EmployeeId { get; set; }
}
