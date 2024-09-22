using ShiftsLogger.Client.Models;
using ShiftsLogger.UI.Utils;
using System.Net.Http.Json;
using System.Text.Json;

namespace ShiftsLogger.Client.Repositories;

public static class ShiftsRepository
{
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public static async Task<List<ShiftsEntry>> GetShiftsAsync()
    {
        using var client = new HttpClient();
        var url = ApiRoutes.GetShiftsUrl();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var jsonStream = await response.Content.ReadAsStreamAsync();
        var shiftsList = await JsonSerializer.DeserializeAsync<List<ShiftsEntry>>(jsonStream, options);

        return shiftsList ?? [];
    }

    public static async Task CreateShiftAsync(ShiftsEntry entry)
    {
        using var client = new HttpClient();
        var url = ApiRoutes.CreateShiftUrl();
        var response = await client.PostAsJsonAsync(url, entry);
        response.EnsureSuccessStatusCode();
    }

    public static async Task UpdateShiftAsync(ShiftsEntry entry)
    {
        using var client = new HttpClient();
        var url = ApiRoutes.UpdateShiftUrl(entry.Id);
        var response = await client.PutAsJsonAsync(url, entry);
        response.EnsureSuccessStatusCode();
    }

    public static async Task DeleteShiftAsync(long id)
    {
        using var client = new HttpClient();
        var url = ApiRoutes.DeleteShiftUrl(id);
        var response = await client.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
    }
}