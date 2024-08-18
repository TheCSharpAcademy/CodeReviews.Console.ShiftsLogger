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

    public async Task<List<Shift>> GetShiftsAsync()
    {
        var response = await _httpClient.GetAsync("api/shifts");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Shift>>(json);
        }
        else
        {
            throw new InvalidOperationException("Failed to get shifts.");
            return new List<Shift>();
        }
    }

    public async Task<HttpResponseMessage> StartShiftAsync<StartShiftDto>(StartShiftDto startShift)
    {
        var json = JsonSerializer.Serialize(startShift);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync("api/shifts", content);
    }

    public async Task<HttpResponseMessage> EndShiftAsync()
    {
        return await _httpClient.PutAsync("api/shifts", null);
    }

}
