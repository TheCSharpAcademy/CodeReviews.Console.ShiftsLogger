namespace ShiftsLogger.UI.Models.DTOs;

public class ShiftViewDto
{
    public string WorkerName { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
}