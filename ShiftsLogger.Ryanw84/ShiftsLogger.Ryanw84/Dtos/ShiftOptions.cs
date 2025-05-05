using Microsoft.AspNetCore.Mvc;

namespace ShiftsLogger.Ryanw84.Dtos;

public class ShiftOptions
    {
    // Filter options
    [FromQuery(Name = "worker_name")]
    public string WorkerName { get; set; } = string.Empty;
    [FromQuery(Name = "location_name")]
    public string LocationName { get; set; } = string.Empty;
    [FromQuery(Name = "shift_start_time")]
    public DateTime? ShiftStartTime { get; set; }
    [FromQuery(Name = "shift_end_time")]
    public DateTime? ShiftEndTime { get; set; }

    // Sorting options
    [FromQuery(Name = "sort_by")]
    public string SortBy { get; set; } = "id";
    [FromQuery(Name = "sort_order")]
    public string SortOrder { get; set; } = "ASC"; // Asc or Desc

    // Search options
    [FromQuery(Name = "search")]
    public string Search { get; set; } = string.Empty;

    // Pagination options
    [FromQuery(Name = "page_size")]
    public int PageSize { get; set; } = 10; // Default page size
    [FromQuery(Name = "page_number")]
    public int PageNumber { get; set; } = 1; // Default page number
    }
