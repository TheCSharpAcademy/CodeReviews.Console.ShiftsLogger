namespace ShiftsLogger.weakiepedia.Models;

public class Shift
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public long DurationInSeconds { get; set; } = 0;
}