namespace ShiftLogger.samggannon.Models;

public class WorkShift
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime ClockIn { get; set; }
    public DateTime ClockOut { get; set; }
    public string Duration { get; set; }
}
