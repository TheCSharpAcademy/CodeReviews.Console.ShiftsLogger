namespace ConsoleFrontEnd.Models.FilterOptions;

public class ShiftFilterOptions
{
	// This class defines the filter options for retrieving shifts, allowing filtering by worker ID, location ID, start time, and end time.
	public int? ShiftId { get; set; }
	public int? WorkerId { get; set; }
	public int? LocationId { get; set; }
	public DateTimeOffset? StartTime { get; set; }
	public DateTimeOffset? EndTime { get; set; }
}
