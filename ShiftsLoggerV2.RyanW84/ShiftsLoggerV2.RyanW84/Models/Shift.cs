namespace ShiftsLoggerV2.RyanW84.Models;

public class Shift
	{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTimeOffset StartTime { get; set; }
	public DateTimeOffset EndTime { get; set; }
	public string Location { get; set; }
	public string Status { get; set; }
	public string AssignedTo { get; set; }
	}