namespace Shifts_Logger.DTOs;

public class CreateShiftDto
{
    public int WorkerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
