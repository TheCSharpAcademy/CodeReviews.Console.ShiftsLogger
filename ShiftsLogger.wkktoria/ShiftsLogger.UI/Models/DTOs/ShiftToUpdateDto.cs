namespace ShiftsLogger.UI.Models.DTOs;

public class ShiftToUpdateDto
{
    public long Id { get; set; }
    public string WorkerName { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
}