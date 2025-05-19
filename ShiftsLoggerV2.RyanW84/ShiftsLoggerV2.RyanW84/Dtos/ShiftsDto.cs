namespace ShiftsLoggerV2.RyanW84.Dtos;

public class ShiftsDto
{
	public int ShiftId { get; set; }
	public int WorkerId { get; set; }
	public int LocationId { get; set; }
	public DateTimeOffset StartTime { get; set; }
	public DateTimeOffset EndTime { get; set; }
}
