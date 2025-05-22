using Microsoft.AspNetCore.Mvc;

namespace ShiftsLoggerV2.RyanW84.Models.FilterOptions;

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

	[FromQuery(Name = "locationName")]
    public string LocationName { get; set; } = string.Empty; // Use string for location name filtering

	[FromQuery(Name = "sort_by")]
    public string SortBy { get; set; } = "shift_id"; // Use string for sorting options

    [FromQuery(Name = "sort_order")]
    public string SortOrder { get; set; } = "ASC"; // Use string for sorting options

    [FromQuery(Name = "search")]
	public string Search { get; set; } = string.Empty; // Use string for search options

}
