using System.Net.Http.Json;
using ShiftsLoggerUI.Models;

namespace ShiftsLogger.KamilKolanowski.Services;

internal class DataFetcher
{
    private static readonly HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("http://localhost:5000/"),
    };

    internal async Task<string> GetAsync(string endpoint)
    {
        using HttpResponseMessage response = await sharedClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    internal async Task PutAsync(WorkerDto worker)
    {
        var response = await sharedClient.PutAsJsonAsync("workers", worker);
        response.EnsureSuccessStatusCode();
    }

    internal async Task PostAsync(WorkerDto worker)
    {
        var response = await sharedClient.PostAsJsonAsync("workers", worker);
        response.EnsureSuccessStatusCode();
    }

    internal async Task DeleteAsync(string endpoint)
    {
        var response = await sharedClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
    }
}

