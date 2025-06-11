using Microsoft.AspNetCore.Mvc;

namespace ShiftsLoggerV2.RyanW84.Models.FilterOptions;

public class ShiftFilterOptions
{
    [FromQuery(Name = "shiftId")]
    public int ShiftId { get; set; } =0; // Use int for shift ID filtering

	[FromQuery(Name = "startTime")]
    public DateTimeOffset? StartTime { get; set; }

    [FromQuery(Name = "endTime")]
    public DateTimeOffset? EndTime { get; set; }

    [FromQuery(Name = "startDate")]
    public DateTime? StartDate { get; set; } // Use DateTime for date-only filtering

    [FromQuery(Name = "endDate")]
    public DateTime? EndDate { get; set; } // Use DateTime for date-only filtering

    [FromQuery(Name = "workerId")]
    public int? WorkerId { get; set; } = 0;

    [FromQuery(Name = "locationId")]
    public int? LocationId { get; set; } = 0;

    [FromQuery(Name = "locationName")]
    public string LocationName { get; set; } = string.Empty; // Use string for location name filtering

    [FromQuery(Name = "sortBy")]
    public string SortBy { get; set; } = string.Empty;

    [FromQuery(Name = "sortOrder")]
    public string SortOrder { get; set; } = string.Empty;

    [FromQuery(Name = "search")]
    public string Search { get; set; } = string.Empty; // Use string for search options
}
