using System.Net;
using System.Net.Http.Json;

using UI.Models;

namespace UI.DAL;

public class ShiftDataAccess
{
    private static HttpClient _sharedClient = new()
    {
        BaseAddress = new Uri("https://localhost:7012")
    };

    public static async Task<IEnumerable<Shift>> GetShifts()
    {
        return await _sharedClient.GetFromJsonAsync<List<Shift>>("api/Shift");
    }

    public static async Task<Shift> GetShift(int id)
    {
        return await _sharedClient.GetFromJsonAsync<Shift>($"api/Shift/{id}");
    }

    public static async Task<bool> UpdateShift(int id, Shift shift)
    {
        var response = await _sharedClient.PutAsJsonAsync($"api/Shift/{id}", shift);

        return response.StatusCode == HttpStatusCode.NoContent;
    }

    public static async Task<bool> InsertShift(Shift shift)
    {
        var response = await _sharedClient.PostAsJsonAsync("api/Shift", shift);

        return response.StatusCode == HttpStatusCode.Created;
    }

    public static async Task<bool> DeleteShift(int id)
    {
        var response = await _sharedClient.DeleteAsync($"api/Shift/{id}");

        return response.StatusCode == HttpStatusCode.NoContent;
    }
}
