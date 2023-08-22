using System.Net;
using System.Net.Http.Json;
using UI.Models;

namespace UI.DAL;

public class ShiftDataAccess
{
    private readonly HttpClient _client;

    public ShiftDataAccess(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Shift>> GetShifts()
    {
        return await _client.GetFromJsonAsync<List<Shift>>("api/Shift");
    }

    public async Task<Shift> GetShift(int id)
    {
        return await _client.GetFromJsonAsync<Shift>($"api/Shift/{id}");
    }

    public async Task<bool> UpdateShift(int id, Shift shift)
    {
        var response = await _client.PutAsJsonAsync($"api/Shift/{id}", shift);

        return response.StatusCode == HttpStatusCode.NoContent;
    }

    public async Task<bool> AddShift(Shift shift)
    {
        var response = await _client.PostAsJsonAsync("api/Shift", shift);

        return response.StatusCode == HttpStatusCode.Created;
    }

    public async Task<bool> DeleteShift(int id)
    {
        var response = await _client.DeleteAsync($"api/Shift/{id}");

        return response.StatusCode == HttpStatusCode.NoContent;
    }
}
