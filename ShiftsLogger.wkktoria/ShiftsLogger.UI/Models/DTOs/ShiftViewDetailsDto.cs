namespace ShiftsLogger.UI.Models.DTOs;

public class ShiftViewDetailsDto
{
    public string WorkerName { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public double Duration { get; set; }

    public override string ToString()
    {
        return $"Worker: {WorkerName}; Started: {StartedAt}";
    }
}