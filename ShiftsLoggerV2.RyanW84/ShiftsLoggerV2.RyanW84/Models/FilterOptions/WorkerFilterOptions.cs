using Microsoft.AspNetCore.Mvc;

namespace ShiftsLoggerV2.RyanW84.Models.FilterOptions;

public class WorkerFilterOptions
{
    [FromQuery(Name = "workerId")]
    public int? WorkerId { get; set; } = 0;

    [FromQuery(Name = "name")]
    public string Name { get; set; } = string.Empty; // Use string for name filtering

    [FromQuery(Name = "phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty; // Use string for phone filtering

    [FromQuery(Name = "email")]
    public string Email { get; set; } = string.Empty; // Use string for email filtering

    [FromQuery(Name = "sortBy")]
    public string SortBy { get; set; } = "workerId"; // Use string for sorting options

    [FromQuery(Name = "sortOrder")]
    public string SortOrder { get; set; } = "ASC"; // Use string for sorting options

    [FromQuery(Name = "search")]
    public string Search { get; set; } = string.Empty; // Use string for search options
}
