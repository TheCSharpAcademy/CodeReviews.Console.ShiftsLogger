namespace ShiftsLogger.API.Models;

public class ShiftsEntry
{
    public long Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}