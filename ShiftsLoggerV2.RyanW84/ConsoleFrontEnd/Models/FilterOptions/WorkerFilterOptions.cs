namespace ConsoleFrontEnd.Models.FilterOptions;

public class WorkerFilterOptions
{
	// This class defines the filter options for retrieving workers, allowing filtering by worker ID, location ID, and name.
	public int? WorkerId { get; set; } // Optional filter by worker ID
	public string? Name { get; set; } // Optional filter by worker name
	public string? PhoneNumber { get; set; } // Optional filter by worker phone number
	public string? Email { get; set; } // Optional filter by worker email address
}
