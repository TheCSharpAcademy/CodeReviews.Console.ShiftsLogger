using System.Text;
using System.Text.Json;
using ShiftLoggerUI.Models;

namespace ShiftLoggerUI.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Shift> GetShiftAsync(int shiftId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/shifts/{shiftId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var shift = JsonSerializer.Deserialize<Shift>(json);

            if (shift == null)
            {
                throw new InvalidOperationException("Failed to deserialize Shift object.");
            }

            return shift;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Critical error occurred: {ex.Message}. Shutting down application.");
            Environment.Exit(-1);
            return null; // Dette vil aldrig blive nået, men er nødvendigt for at tilfredsstille compiler
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Critical error occurred: {ex.Message}. Shutting down application.");
            Environment.Exit(-1);
            return null; // Dette vil aldrig blive nået, men er nødvendigt for at tilfredsstille compiler
        }
    }

    public async Task<List<Shift>> GetShiftsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/shifts");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Shift>>(json);
            }
            else
            {
                throw new InvalidOperationException($"Failed to get shifts. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Critical error occurred: {ex.Message}. Shutting down application.");
            Environment.Exit(-1);
            return null; // Dette vil aldrig blive nået, men er nødvendigt for at tilfredsstille compiler
        }
    }

    public async Task<HttpResponseMessage> StartShiftAsync<StartShiftDto>(StartShiftDto startShift)
    {
        try
        {
            var json = JsonSerializer.Serialize(startShift);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/shifts", content);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Critical error occurred: {ex.Message}. Shutting down application.");
            Environment.Exit(-1);
            return null; // Dette vil aldrig blive nået, men er nødvendigt for at tilfredsstille compiler
        }
    }

    public async Task<HttpResponseMessage> EndShiftAsync()
    {
        try
        {
            return await _httpClient.PutAsync("api/shifts", null);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Critical error occurred: {ex.Message}. Shutting down application.");
            Environment.Exit(-1);
            return null; // Dette vil aldrig blive nået, men er nødvendigt for at tilfredsstille compiler
        }
    }
}
