using ShiftsLoggerUI.Models;
using Spectre.Console;
using System.Net.Http.Json;

namespace ShiftsLoggerUI;

public class ShiftsLoggerHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly Uri _baseAddress = new Uri("https://localhost:7179/api/Shifts/");

    public ShiftsLoggerHttpClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = _baseAddress;
    }

    public async Task<List<Shift>> GetShifts()
    {
        List<Shift>? shifts = [];
        try
        {
            shifts = await _httpClient.GetFromJsonAsync<List<Shift>>(_baseAddress);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        if (shifts is null)
        {
            AnsiConsole.Markup("[red]No results found.[/]");
            return new();
        }
        return shifts;
    }

    public async Task<Shift> GetSingleShift(string shiftId)
    {
        Shift? shift = new();
        try
        {    
            shift = await _httpClient.GetFromJsonAsync<Shift>($"{shiftId}");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        if (shift is null)
        {
            AnsiConsole.Markup("[red]No results found.[/]");
            return new();
        }
        return shift;
    }

    public async Task<bool> PostShift(Shift shift)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<Shift>(_baseAddress, shift);
            if (!response.IsSuccessStatusCode)
            {
                AnsiConsole.Markup($"[red]Something went wrong. Details: {response.Content.ReadAsStringAsync().Result}[/]");
            }     
            AnsiConsole.Markup($"[green]Operation successful.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        Console.ReadLine();
        return false;
    }

    public async Task<bool> DeleteShift(string shiftId)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{shiftId}");
            if (!response.IsSuccessStatusCode)
            {
                AnsiConsole.Markup($"[red]Something went wrong. Details: {response.Content.ReadAsStringAsync().Result}[/]");
            }
            AnsiConsole.Markup($"[green]Operation successful.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        Console.ReadLine();
        return false;
    }

    public async Task<bool> EditShift(string shiftId)
    {
        Shift shift = await GetSingleShift(shiftId);
        try
        {        
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<Shift>($"{shiftId}", UserInput.EnterShiftProperties(shift));
            if (!response.IsSuccessStatusCode)
            {
                AnsiConsole.Markup($"[red]Something went wrong. Details: {response.Content.ReadAsStringAsync().Result}[/]");
            }
            AnsiConsole.Markup($"[green]Operation successful.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        Console.ReadLine();
        return false;
    }
}
