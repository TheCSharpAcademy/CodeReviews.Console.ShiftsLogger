namespace ShiftsLoggerApi.Model;

public class Shift
{
    public int Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public TimeOnly Duration { get; set; }

    public DateOnly Date { get; set; }
}