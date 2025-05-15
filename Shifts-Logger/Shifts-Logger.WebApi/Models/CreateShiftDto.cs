namespace ShiftsLogger.WebApi.Models;

public class CreateShiftDto
{
    public int WorkerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
