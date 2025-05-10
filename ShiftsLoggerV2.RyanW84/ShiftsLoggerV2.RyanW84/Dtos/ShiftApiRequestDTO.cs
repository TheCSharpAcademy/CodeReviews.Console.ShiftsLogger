namespace ShiftsLoggerV2.RyanW84.Dtos;

public class ShiftApiRequestDto
	{
	public int WorkerId { get; set; }
	public DateTimeOffset StartTime { get; set; }
	public DateTimeOffset EndTime { get; set; }
	public int LocationId { get; set; }
	}
