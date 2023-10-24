namespace ShiftsLogger.UI.Models.DTOs;

public class ShiftViewDto
{
    public string WorkerName { get; set; } = null!;
    public DateTime StartAt { get; set; }
    public DateTime FinishAt { get; set; }
    public TimeSpan Duration { get; set; }
}