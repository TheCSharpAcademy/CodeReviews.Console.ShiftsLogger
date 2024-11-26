namespace ShiftsLoggerUI.Model;

public class ShiftDto(string StartTime, string? EndTime, string? Duration, string Date)
{
    public int id { get; set; }
    public string startTime { get; set; } = StartTime;
    public string? endTime { get; set; } = EndTime;
    public string? duration { get; set; } = Duration;
    public string date { get; set; } = Date;

    public Shift ToShift()
    {
        TimeOnly.TryParse(startTime, out var StartTime);
        TimeOnly.TryParse(endTime, out var EndTime);
        TimeOnly.TryParse(duration, out var Duration);
        DateOnly.TryParse(date, out var Date);
        return new Shift(StartTime, EndTime, Duration, Date);
    }
}