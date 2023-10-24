namespace ShiftsLogger.UI.Models.DTOs;

public class ShiftDto
{
    public long Id { get; set; }
    public string WorkerName { get; set; } = null!;
    public DateTime StartAt { get; set; }
    public DateTime FinishAt { get; set; }
}