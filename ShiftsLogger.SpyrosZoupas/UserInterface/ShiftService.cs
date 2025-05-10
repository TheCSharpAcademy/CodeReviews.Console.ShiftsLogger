using Microsoft.IdentityModel.Tokens;
using ShiftsLogger.SpyrosZoupas.DAL.Model;
using Spectre.Console;
using System.Net.Http.Json;
using UserInterface.SpyrosZoupas.Util;

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
        DateTime startDate = Validation.GetDateTimeValue("Start Date:");
        DateTime endDate = Validation.GetDateTimeValue("End Date:");
        Shift shift = new Shift { StartDateTime = startDate, EndDateTime = endDate };

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Shift", shift);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.Markup($"[black on red]Something went wrong: {ex.Message}. Enter any key to go back to Main Menu[/]");
            Console.ReadLine();
            Console.Clear();
        }
    }

    public async Task<Shift?> GetShift()
    {
        Shift? shift = GetShiftOptionInput();
        if (shift == null) return null;

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Shift/{shift?.ShiftId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Shift>();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.Markup($"[black on red]Something went wrong: {ex.Message}. Enter any key to go back to Main Menu[/]");
            Console.ReadLine();
            Console.Clear();
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
            AnsiConsole.Markup($"[black on red]Something went wrong: {ex.Message}. Enter any key to continue[/]");
            Console.ReadLine();
            Console.Clear();
        }

        return null;
    }

    public async Task DeleteShift()
    {
        Shift? shift = GetShiftOptionInput();

        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Shift/{shift?.ShiftId}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.Markup($"[black on red]Something went wrong: {ex.Message}. Enter any key to go back to Main Menu[/]");
            Console.ReadLine();
            Console.Clear();
        }
    }

    public async Task UpdateShift()
    {
        Shift? shift = GetShiftOptionInput();
        if (shift == null) return;

        if (AnsiConsole.Confirm("Update start date?"))
            shift.StartDateTime = Validation.GetDateTimeValue("Updated start date:");
        if (AnsiConsole.Confirm("Update end date?"))
            shift.EndDateTime = Validation.GetDateTimeValue("Updated end date:");

        try
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Shift/{shift?.ShiftId}", shift);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.Markup($"[black on red]Something went wrong: {ex.Message}. Enter any key to go back to Main Menu[/]");
            Console.ReadLine();
            Console.Clear();
        }
    }

    public Shift? GetShiftOptionInput()
    {
        var shifts = GetAllShifts().Result;
        if (shifts.IsNullOrEmpty()) 
        {
            AnsiConsole.Markup($"[red]No Shifts found![/]");
            Console.ReadLine();
            Console.Clear();
            return null;
        }

        var option = AnsiConsole.Prompt(new SelectionPrompt<int>()
            .Title("Choose Shift")
            .AddChoices(shifts.Select(s => s.ShiftId)));

        return shifts.First(s => s.ShiftId == option);
    }
}