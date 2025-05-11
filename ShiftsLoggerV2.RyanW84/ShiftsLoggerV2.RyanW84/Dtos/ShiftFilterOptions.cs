using Microsoft.AspNetCore.Mvc;

namespace ShiftsLoggerV2.RyanW84.Dtos;

public class ShiftFilterOptions
{
	[FromQuery(Name = "startTime")]
	public DateTimeOffset? StartTime { get; set; }

	[FromQuery(Name = "endTime")]
	public DateTimeOffset? EndTime { get; set; }

	[FromQuery(Name = "startDate")]
	public DateTime? StartDate { get; set; } // Use DateTime for date-only filtering

	[FromQuery(Name = "endDate")]
	public DateTime? EndDate { get; set; } // Use DateTime for date-only filtering

	[FromQuery(Name = "workerId")]
	public int? WorkerId { get; set; }

	[FromQuery(Name = "locationId")]
	public int? LocationId { get; set; }
}
