using Microsoft.AspNetCore.Mvc;

namespace ShiftsLoggerV2.RyanW84.Models.FilterOptions;

public class WorkerFilterOptions
{
    [FromQuery(Name = "workerId")]
    public int? WorkerId { get; set; }

    [FromQuery(Name = "name")]
    public string Name { get; set; } = string.Empty; // Use string for name filtering

    [FromQuery(Name = "phone")]
    public string Phone { get; set; } = string.Empty; // Use string for phone filtering

    [FromQuery(Name = "email")]
    public string Email { get; set; } = string.Empty; // Use string for email filtering

    [FromQuery(Name = "sort_by")]
    public string SortBy { get; set; } = "worker_id"; // Use string for sorting options

    [FromQuery(Name = "sort_order")]
    public string SortOrder { get; set; } = "ASC"; // Use string for sorting options

    [FromQuery(Name = "search")]
    public string Search { get; set; } = string.Empty; // Use string for search options
}
