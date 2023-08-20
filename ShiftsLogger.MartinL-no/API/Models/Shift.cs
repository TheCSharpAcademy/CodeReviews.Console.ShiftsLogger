namespace API.Models;

internal class Shift
{
    public int ShiftId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Employee Employee { get; set; }
}