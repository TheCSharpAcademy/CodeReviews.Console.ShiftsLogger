namespace ShiftsLoggerApi.Model;

public class Shift
{
    public int Id { get; set; }
    public string StartTime { get; set; }
    public string? EndTime { get; set; }

    public string? Duration { get; set; }

    public string Date { get; set; }
}