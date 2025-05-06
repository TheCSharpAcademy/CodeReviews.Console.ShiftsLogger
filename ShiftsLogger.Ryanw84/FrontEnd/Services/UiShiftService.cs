using System.Reflection.Metadata;

using FrontEnd.Controllers;

using Microsoft.Identity.Client;

using Newtonsoft.Json;
using ShiftsLogger.Ryanw84.Models;
using Spectre.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FrontEnd.Services;

public class UIShiftService
    {
    private readonly HttpClient _httpClient;

    // Constructor to initialize the HttpClient
    public UIShiftService(HttpClient httpClient)
        {
        _httpClient = httpClient;
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
            AnsiConsole.MarkupLine($"[red]Error fetching shifts: {ex.Message}[/]");
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

	
    }
