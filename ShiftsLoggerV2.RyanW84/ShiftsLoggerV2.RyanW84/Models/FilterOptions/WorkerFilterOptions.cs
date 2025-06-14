using Microsoft.AspNetCore.Mvc;

namespace ShiftsLoggerV2.RyanW84.Models.FilterOptions;

public class WorkerFilterOptions
{
    [FromQuery(Name = "WorkerId")]
    public int? WorkerId { get; set; } = 0;

    [FromQuery(Name = "Name")]
    public string Name { get; set; } = string.Empty; // Use string for name filtering

    [FromQuery(Name = "PhoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty; // Use string for phone filtering

    [FromQuery(Name = "Email")]
    public string Email { get; set; } = string.Empty; // Use string for email filtering

    [FromQuery(Name = "SortBy")]
    public string SortBy { get; set; } = "workerId"; // Use string for sorting options

    [FromQuery(Name = "SortOrder")]
    public string SortOrder { get; set; } = "ASC"; // Use string for sorting options

    [FromQuery(Name = "Search")]
    public string Search { get; set; } = string.Empty; // Use string for search options
}
