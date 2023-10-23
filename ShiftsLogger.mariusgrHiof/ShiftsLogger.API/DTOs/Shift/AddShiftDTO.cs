namespace ShiftsLogger.API.DTOs.Shift;
public class AddShiftDTO
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int WorkerId { get; set; }
}