namespace ConsoleFrontEnd.Models.FilterOptions;

public class ShiftFilterOptions
{
    // This class defines the filter options for retrieving shifts, allowing filtering by worker ID, location ID, start time, and end time.
    public int? ShiftId { get; set; }
    public int? WorkerId { get; set; }
    public int? LocationId { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
    public string? Search { get; set; }
    public string? LocationName { get; set; } = string.Empty;
    public string? SortBy { get; set; } = string.Empty;
    public string? SortOrder { get; set; } = string.Empty;

    // Additional properties for date filtering
    public DateTime? StartDate { get; set; } // Use DateTime for date-only filtering
    public DateTime? EndDate { get; set; } // Use DateTime for date-only filtering
}
