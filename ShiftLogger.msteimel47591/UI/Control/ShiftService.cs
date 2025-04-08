using UI.Models;
using System.Net.Http.Json;

namespace UI.Service;

internal class ShiftService
{
    private readonly HttpClient _client;

    public ShiftService()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5262")
        };
    }

    public async Task<List<Shift>> GetShifts()
    {
        var shifts = await _client.GetFromJsonAsync<List<Shift>>("shifts");
        return shifts;
    }

    public async Task<List<Shift>> GetShiftByEmployee(string name)
    {
        var shifts = await _client.GetFromJsonAsync<List<Shift>>($"shifts/name/{name}");
        return shifts;
    }

    public async Task AddShift(Shift shift)
    {
        var response = await _client.PostAsJsonAsync("shifts", shift);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> DeleteShift(int id)
    {
        var response = await _client.DeleteAsync($"shifts/id/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return true;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        else
        {
            response.EnsureSuccessStatusCode();
            return false;
        }
    }

    public async Task<Shift> GetShiftById(int id)
    {
        var shift = await _client.GetFromJsonAsync<Shift>($"shifts/id/{id}");
        return shift;
    }

    public async Task UpdateShift(Shift shift)
    {
        var response = await _client.PutAsJsonAsync($"shifts/id/{shift.Id}", shift);
        response.EnsureSuccessStatusCode();
    }
}


