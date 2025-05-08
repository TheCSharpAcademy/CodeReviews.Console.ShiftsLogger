using System.Net.Http;

using Newtonsoft.Json;

using ShiftsLogger.Ryanw84.Models;

using Spectre.Console;

namespace FrontEnd.Services;

public class UiShiftService
	{
	private readonly HttpClient _httpClient;

	// Constructor to initialize the HttpClient
	public UiShiftService(HttpClient httpClient)
		{
		
		_httpClient = httpClient;
		httpClient.BaseAddress = new Uri("https://localhost:7009");
		}

	public async Task<List<Shift>> GetAllShifts( )
		{
		try
			{
			var response = await _httpClient.GetAsync("shift");

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var shifts = JsonConvert.DeserializeObject<List<Shift>>(jsonResponse);

			return shifts ?? new List<Shift>();
			}
		catch(Exception ex)
			{
			AnsiConsole.MarkupLine($"[red]Error fetching workers: {ex.Message}[/]");
			return new List<Shift>();
			}
		}

	public async Task<List<Shift>> GetShiftById(int id)
		{
		try
			{
			var response = await _httpClient.GetAsync($"shift/{id}");
			var shifts = JsonConvert.DeserializeObject<List<Shift>>(await response.Content.ReadAsStringAsync());

			return shifts ?? new List<Shift>();
			}
		catch(Exception ex)
			{
			AnsiConsole.MarkupLine($"[red]Error fetching shift by Id: {ex.Message}[/]");
			return new List<Shift>();
			}
		}

	public async Task<List<Worker>> GetAllWorkers( )
		{
		try
			{
			var response = await _httpClient.GetAsync("worker");

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var workers = JsonConvert.DeserializeObject<List<Worker>>(jsonResponse);

			return workers ?? new List<Worker>();
			}
		catch(Exception ex)
			{
			AnsiConsole.MarkupLine($"[red]Error fetching workers: {ex.Message}[/]");
			return new List<Worker>();
			}
		}
	}
