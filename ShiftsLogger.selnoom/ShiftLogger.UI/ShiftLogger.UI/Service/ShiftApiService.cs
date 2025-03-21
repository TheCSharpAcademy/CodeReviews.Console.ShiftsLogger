using ShiftsLogger.API.Model;
using System.Net.Http.Json;

namespace ShiftsLogger.UI.Service;

class ShiftApiService
{
    private readonly HttpClient _httpClient;

    public ShiftApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Shift>?> GetAllShiftsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Shift>>("api/shift");
    }

    public async Task<Shift?> GetShiftByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Shift>($"api/shift/{id}");
    }

    public async Task<Shift?> CreateShift(Shift shift)
    {
        var response = await _httpClient.PostAsJsonAsync<Shift>($"api/shift/", shift);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Shift>();
    }

    public async Task<Shift?> UpdateShift(Shift shift)
    {
        var response = await _httpClient.PutAsJsonAsync<Shift>($"api/shift/", shift);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Shift>();
    }

    public async Task<String> DeleteShift(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/shift/{id}");
        response.EnsureSuccessStatusCode();
        return $"Shift of Id {id} deleted successfully!";
    }
}
