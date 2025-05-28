using System.Net.Http.Json;
using System.Web.Http;

using ConsoleFrontEnd.ApiShiftService;
using ConsoleFrontEnd.Models;

namespace ConsoleFrontEnd.Services;

public class ShiftService : IShiftService
{
	private readonly HttpClient httpClient;
	HttpClient base

	[HttpGet("api/shifts/{id}")] // Fixed the attribute syntax
	public async Task<List<Shifts>> GetShiftById(int id)
	{
		HttpResponseMessage response;
		try
		{
			response = await httpClient.GetAsync($"api/shifts/{id}");

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
				return new List<Shifts>();
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
			{
				Console.WriteLine("No shifts found.");
				return new List<Shifts>();
			}
			else
			{
				Console.WriteLine("Shift retrieved successfully.");
				var result = await response.Content.ReadFromJsonAsync<Shifts>();
				return result != null ? new List<Shifts> { result } : new List<Shifts>();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Try catch failed for GetShiftById: {ex}");
			throw;
		}
	}

	// Existing code...
}
