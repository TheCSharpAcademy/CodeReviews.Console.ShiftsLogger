namespace ShiftLoggerApi.Dtos;

public class ShiftReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Duration { get; set; }
}