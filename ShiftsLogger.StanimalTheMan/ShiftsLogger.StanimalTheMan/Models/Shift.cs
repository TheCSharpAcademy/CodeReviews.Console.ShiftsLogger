namespace ShiftsLoggerWebAPI.Models;

public class Shift
{
	public long Id { get; set; }
	public string? Type { get; set; } // e.g. night shift
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public TimeSpan Duration { get; set; }
}
