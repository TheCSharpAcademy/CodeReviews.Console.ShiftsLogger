namespace sadklouds.ShiftLogger.Models;
public class Shift
{
    public int Id { get; set; }
    public DateTime ShiftStart { get; set; }
    public DateTime ShiftEnd { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdate { get; set; }
}
