using System.Net.Http.Json;
using ShiftsLogger.KamilKolanowski.Models;
using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI.Services;

internal class ApiDataService
{
    private readonly HttpClient _sharedClient = new()
    {
        BaseAddress = new Uri("http://localhost:5000/"),
    };

    internal async Task<string> GetWorkerAsync(string endpoint)
    {
        using HttpResponseMessage response = await _sharedClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    internal async Task PutWorkerAsync(WorkerDto worker)
    {
        var response = await _sharedClient.PutAsJsonAsync("workers", worker);
        response.EnsureSuccessStatusCode();
    }

    internal async Task<WorkerDto> PostWorkerAsync(WorkerDto worker)
    {
        var response = await _sharedClient.PostAsJsonAsync("workers", worker);
        response.EnsureSuccessStatusCode();
        
        var createdWorker = await response.Content.ReadFromJsonAsync<WorkerDto>();
        return createdWorker!;
    }

    internal async Task DeleteWorkerAsync(int workerId)
    {
        var response = await _sharedClient.DeleteAsync($"workers/{workerId}");
        response.EnsureSuccessStatusCode();
    }

    internal async Task<string> GetShiftAsync(string endpoint)
    {
        using HttpResponseMessage response = await _sharedClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    internal async Task PutShiftAsync(ShiftDto shift)
    {
        var response = await _sharedClient.PutAsJsonAsync("shifts", shift);
        response.EnsureSuccessStatusCode();
    }

    internal async Task PostShiftAsync(ShiftDto shift)
    {
        var response = await _sharedClient.PostAsJsonAsync("shifts", shift);
        response.EnsureSuccessStatusCode();
    }

    internal async Task DeleteShiftAsync(int shiftId)
    {
        var response = await _sharedClient.DeleteAsync($"shifts/{shiftId}");
        response.EnsureSuccessStatusCode();
    }
}
