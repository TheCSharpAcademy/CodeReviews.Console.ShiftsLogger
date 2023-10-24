namespace ShiftsLogger.API.Models;

public class Shift
{
    public long Id { get; set; }
    public string WorkerName { get; set; } = null!;
    public DateTime StartAt { get; set; }
    public DateTime FinishAt { get; set; }
    public TimeSpan Duration { get; set; }
}