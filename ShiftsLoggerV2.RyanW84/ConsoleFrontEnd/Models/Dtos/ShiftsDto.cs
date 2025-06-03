namespace ConsoleFrontEnd.Models.Dtos;

public class ShiftsDto
{
    public int WorkerId { get; set; }
    public int LocationId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}
