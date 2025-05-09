namespace ShiftsLoggerV2.RyanW84.Models;

public class Shift
{
    public int ShiftId { get; set; }
    public int workerId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int LocationId { get; set; }
}
