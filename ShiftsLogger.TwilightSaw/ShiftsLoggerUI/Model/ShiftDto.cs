namespace ShiftsLoggerUI.Model;

public class ShiftDto(string startTime, string? endTime, string? duration, string date)
{
    public int id { get; set; }
    public string startTime { get; set; } = startTime;
    public string? endTime { get; set; } = endTime;
    public string? duration { get; set; } = duration;
    public string date { get; set; } = date;

    public Shift ToShift()
    {

        TimeOnly.TryParse(startTime, out var StartTime);
        TimeOnly.TryParse(endTime, out var EndTime);
        TimeOnly.TryParse(duration, out var Duration);
        DateOnly.TryParse(date, out var Date);
        var shift = new Shift(StartTime, EndTime, Duration, Date)
        {
            Id = id
        };
        return shift;
    }

    public override string ToString()
    {
        return $"{startTime} {endTime} {duration} {date}";
    }
}