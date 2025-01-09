namespace ShiftsLoggerUI;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Threading.Tasks;

internal static class UIController
{
	private static readonly HttpClient _httpClient;

	static UIController()
	{
		var handler = new HttpClientHandler();
		handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
		_httpClient = new HttpClient(handler)
		{
			BaseAddress = new Uri("https://localhost:7217/api/Shifts") // Endre til riktig base URL for API-en din
		};
	}

	// Add a new shift
	internal static async Task<bool> AddAsync(ShiftModel shift)
	{
		try
		{
			var response = await _httpClient.PostAsJsonAsync("Shifts", shift); // POST to /shifts
			return response.IsSuccessStatusCode;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error adding shift: {ex.Message}");
			return false;
		}
	}

	// Get all shifts
	internal static async Task<IEnumerable<ShiftModel>> GetAsync()
	{
		try
		{
			var response = await _httpClient.GetAsync("Shifts"); // GET from /shifts
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<IEnumerable<ShiftModel>>();
			}
			else
			{
				Console.WriteLine($"Failed to fetch shifts: {response.ReasonPhrase}");
				return new List<ShiftModel>();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error fetching shifts: {ex.Message}");
			return new List<ShiftModel>();
		}
	}

	// Remove a shift
	internal static async Task RemoveAsync(ShiftModel shift)
	{
		try
		{
			var response = await _httpClient.DeleteAsync($"Shifts/{shift.Id}"); // DELETE from /shifts/{id}
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Failed to remove shift: {response.ReasonPhrase}");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error removing shift: {ex.Message}");
		}
	}

	// Update a shift
	internal static async Task UpdateAsync(ShiftModel shift)
	{
		try
		{
			var response = await _httpClient.PutAsJsonAsync($"Shifts/{shift.Id}", shift); // PUT to /shifts/{id}
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Failed to update shift: {response.ReasonPhrase}");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error updating shift: {ex.Message}");
		}
	}
}
