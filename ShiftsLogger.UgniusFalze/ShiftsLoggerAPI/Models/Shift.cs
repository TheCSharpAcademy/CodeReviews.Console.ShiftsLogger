namespace ShiftsLoggerAPI.Models;

public class Shift
{
    public int ShiftId { get; set; }
    public string Name { get; set; }
    public DateTime ShiftStart { get; set; }
    public DateTime ShiftEnd { get; set; }
    public string? Comment { get; set;}
}