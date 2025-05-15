using System.Net.Http.Json;
using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI.Services;

internal class ApiDataService
{
    private readonly HttpClient _sharedClient = new()
    {
        BaseAddress = new Uri("http://localhost:5000/"),
    };

    internal async Task<string> GetAsync(string endpoint)
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

    internal async Task PostWorkerAsync(WorkerDto worker)
    {
        var response = await _sharedClient.PostAsJsonAsync("workers", worker);
        response.EnsureSuccessStatusCode();
    }

    internal async Task DeleteWorkerAsync(int workerId)
    {
        var response = await _sharedClient.DeleteAsync($"workers/{workerId}");
        response.EnsureSuccessStatusCode();
    }
}
