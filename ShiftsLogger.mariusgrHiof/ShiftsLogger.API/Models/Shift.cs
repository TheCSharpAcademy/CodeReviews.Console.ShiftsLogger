namespace ShiftsLogger.API.Models;
public class Shift
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan Duration => End - Start;
    public int WorkerId { get; set; }
    public Worker Worker { get; set; }
}

