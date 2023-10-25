namespace ShiftsLogger.API.DTOs.Shift;
public class AddShiftDto
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int WorkerId { get; set; }
}