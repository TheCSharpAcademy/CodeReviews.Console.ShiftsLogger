namespace ShiftsLoggerUI.Models;

public class Shift
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public TimeSpan ShiftDuration
    {
        get => EndTime - StartTime;
    }
}
