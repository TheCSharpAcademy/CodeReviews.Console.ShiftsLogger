namespace ShiftsLogger.UI.Models;

public class Shift
{
    public long Id { get; set; }
    public string WorkerName { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public TimeSpan Duration { get; set; }
}