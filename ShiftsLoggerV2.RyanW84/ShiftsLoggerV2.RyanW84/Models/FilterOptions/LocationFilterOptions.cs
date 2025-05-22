namespace ShiftsLoggerV2.RyanW84.Models.FilterOptions;

public class LocationFilterOptions
{
    public int? LocationId { get; set; } = 1;
    public string? SortBy { get; set; } = "locationID";
    public string? SortOrder { get; set; } = "ASC";
    public string? Search { get; set; } = string.Empty;
}
