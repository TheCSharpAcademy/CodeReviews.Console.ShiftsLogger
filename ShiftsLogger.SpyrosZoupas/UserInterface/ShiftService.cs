using ShiftsLogger.SpyrosZoupas.DAL.Model;
using Spectre.Console;
using System.Net.Http.Json;

namespace UserInterface.SpyrosZoupas;

public class ShiftService
{
    private readonly HttpClient _httpClient;

    public ShiftService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task InsertShift()
    {
        // add datetime validation later
        DateTime startDate = AnsiConsole.Ask<DateTime>("Start Date:");
        DateTime endDate = AnsiConsole.Ask<DateTime>("End Date:");
        Shift shift = new Shift { StartDateTime = startDate, EndDateTime = endDate };

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Shift", shift);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.WriteLine($"[black on red]Something went wrong: {ex.Message}[/]");
        }
    }

    public async Task<Shift?> GetShift()
    {
        Shift shift = GetShiftOptionInput();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Shift/{shift.ShiftId}");
            response.EnsureSuccessStatusCode();
            var test = response.Content;
            return await response.Content.ReadFromJsonAsync<Shift>();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.WriteLine($"[black on red]Something went wrong: {ex.Message}[/]");
        }

        return null;
    }

    public async Task<List<Shift>?> GetAllShifts()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Shift");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Shift>>();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.WriteLine($"[black on red]Something went wrong: {ex.Message}[/]");
        }

        return null;
    }

    public async Task DeleteShift()
    {
        Shift? shift = GetShiftOptionInput();
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Shift/{shift.ShiftId}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.WriteLine($"[black on red]Something went wrong: {ex.Message}[/]");
        }
    }

    public async Task UpdateShift()
    {
        Shift? shift = GetShiftOptionInput();
        if (AnsiConsole.Confirm("Update start date?"))
            shift.StartDateTime = AnsiConsole.Ask<DateTime>("Updated start date:");
        if (AnsiConsole.Confirm("Update contact email?"))
            shift.EndDateTime = AnsiConsole.Ask<DateTime>("Updated end date:");

        try
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Shift/{shift.ShiftId}", shift);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.WriteLine($"[black on red]Something went wrong: {ex.Message}[/]");
        }
    }

    public Shift? GetShiftOptionInput()
    {
        var shifts = GetAllShifts().Result;
        if (shifts.Count == 0) return null;

        var option = AnsiConsole.Prompt(new SelectionPrompt<int>()
            .Title("Choose Shift")
            .AddChoices(shifts.Select(s => s.ShiftId)));

        return shifts.First(s => s.ShiftId == option);
    }
}