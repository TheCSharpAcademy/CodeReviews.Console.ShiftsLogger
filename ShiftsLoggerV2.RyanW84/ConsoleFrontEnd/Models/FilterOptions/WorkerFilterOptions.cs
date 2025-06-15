namespace ConsoleFrontEnd.Models.FilterOptions;

public class WorkerFilterOptions
{
	// This class defines the optional filter options for retrieving workers
	public int? WorkerId { get; set; }
	public string? Name { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Email { get; set; }
	public string? Search { get; set; }

	// Sorting options
	public string? SortBy { get; set; }
	public string? SortOrder { get; set; }
}
