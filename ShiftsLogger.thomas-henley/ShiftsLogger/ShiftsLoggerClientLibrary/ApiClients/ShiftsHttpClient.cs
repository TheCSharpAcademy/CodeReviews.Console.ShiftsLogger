using System.Text;
using System.Text.Json;
using ShiftsLoggerModels;

namespace ShiftsLoggerClientLibrary.ApiClients;

public class ShiftsHttpClient : IShiftApiClient
{
    private readonly JsonSerializerOptions _jsonOpts;
    private readonly HttpClient _client;
    
    public ShiftsHttpClient(int port)
    {
        _jsonOpts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:" + port + "/api/");
    }

    public async Task<IEnumerable<Shift>> GetShifts()
    {
        var response = await _client.GetAsync("Shifts");
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        var shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(stream, _jsonOpts);
        return shifts ?? [];
    }

    public async Task<IEnumerable<Shift>> GetShiftsByEmployeeId(int empId)
    {
        var response = await _client.GetAsync($"Shifts/Employee/{empId}");
        response.EnsureSuccessStatusCode();
        
        await using var stream = await response.Content.ReadAsStreamAsync();
        var shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(stream, _jsonOpts);
        return shifts ?? [];
    }

    public async Task<Shift?> GetShiftById(int id)
    {
        var response = await _client.GetAsync($"Shifts/{id}");
        response.EnsureSuccessStatusCode();
        
        await using var stream = await response.Content.ReadAsStreamAsync();
        var shift = await JsonSerializer.DeserializeAsync<Shift>(stream, _jsonOpts);
        return shift ?? null;
    }

    public async Task<Shift> AddShift(Shift shift)
    {
        var json = JsonSerializer.Serialize(shift, _jsonOpts);
        var content = new StringContent(json,
            Encoding.UTF8,
            "application/json");
        
        var response = await _client.PostAsync($"Shifts", content);
        response.EnsureSuccessStatusCode();

        return shift;
    }

    public async Task<bool> DeleteShift(int id)
    {
        var response = await _client.DeleteAsync($"Shifts/{id}");
        response.EnsureSuccessStatusCode();
        
        return true;
    }

    public async Task<Shift> UpdateShift(Shift shift)
    {
        var json = JsonSerializer.Serialize(shift, _jsonOpts);
        var content = new StringContent(json,
            Encoding.UTF8,
            "application/json");
        
        var response = await _client.PutAsync($"Shifts", content);
        response.EnsureSuccessStatusCode();

        return shift;
    }
}