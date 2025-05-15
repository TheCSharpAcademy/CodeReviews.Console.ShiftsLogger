namespace ShiftsLogger.WebApi.Models;

public class UpdateShiftDto
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
