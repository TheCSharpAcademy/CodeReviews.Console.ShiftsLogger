namespace ShiftsLogger.UI.Models.DTOs;

public class ShiftViewDto
{
    public string WorkerName { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public TimeSpan Duration { get; set; }

    public override string ToString()
    {
        return $"Worker name: {WorkerName}; Started at: {StartedAt}; Duration: {Duration}";
    }
}