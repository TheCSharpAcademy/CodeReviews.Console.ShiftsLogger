using ShiftsLogger.API.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace ShiftsLogger.Console.Services;

public class ShiftConsoleService
{
    public HttpClient _httpClient = new HttpClient();

    public bool IsShiftInProgress()
    {
        var allShifts = GetShiftHistory().Result;

        if (allShifts.Count == 0)
        {
            return false;
        }

        return (allShifts.LastOrDefault().ShiftInProgress) ? true : false;
    }

    public async Task<Shift> StartShift()
    {
        Shift shift = new();
        shift.ShiftInProgress = true;
        shift.StartTime = DateTime.Now;

        try
        {
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync("https://localhost:7298/Shifts", shift);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("\nException: ");
            System.Console.WriteLine(ex.Message);
            System.Console.ReadLine();
        }

        return shift;
    }

    public async Task<Shift> EndShift()
    {
        List<Shift> allShifts = GetShiftHistory().Result;
        Shift lastShift = allShifts[allShifts.Count - 1];

        lastShift.EndTime = DateTime.Now;

        TimeSpan difference = lastShift.EndTime - lastShift.StartTime; //Calculating the difference
        lastShift.ShiftLength = difference;
        lastShift.ShiftInProgress = false;

        try
        {
            HttpResponseMessage responseMessage = await _httpClient.PatchAsJsonAsync("https://localhost:7298/Shifts", lastShift);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("\nException: ");
            System.Console.WriteLine(ex.Message);
            System.Console.ReadLine();
        }

        return lastShift;
    }

    public async Task<List<Shift>> GetShiftHistory()
    {
        string shiftJson = "";
        try
        {
            shiftJson = await _httpClient.GetStringAsync($"https://localhost:7298/Shifts");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("\nException: ");
            System.Console.WriteLine(ex.Message);
            System.Console.ReadLine();
        }

        return JsonSerializer.Deserialize<List<Shift>>(shiftJson);
    }
}


