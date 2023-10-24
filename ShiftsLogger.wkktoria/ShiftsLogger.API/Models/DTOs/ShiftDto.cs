namespace ShiftsLogger.API.Models.DTOs;

public class ShiftDto
{
    public long Id { get; set; }
    public string WorkerName { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
}