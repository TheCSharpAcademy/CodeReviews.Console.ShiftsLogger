using System.Net.Http.Json;
using System.Text.Json;
using ShiftsLoggerCLI.Models;
using Spectre.Console;

namespace ShiftsLoggerCLI;

public static class ResponseManager
{
    private static Uri _baseUri = new Uri("http://localhost:5105");
    private static HttpClient _client = new HttpClient();

    public static void InitResponseManager()
    { 
        _baseUri = new("http://localhost:5105/");
        _client.BaseAddress = _baseUri;
        PositionManager.SetPositions(GetAllWorkers().Result);
    }

    public static async Task<List<ShiftRead>> GetShiftsByWorkerId(int workerId)
    {
        var response = await _client.GetAsync($"api/shifts/{workerId}");
        string json = await response.Content.ReadAsStringAsync();
        var shifts = JsonSerializer.Deserialize<List<ShiftRead>>(json) ?? new();
        return shifts;
    }
    public static async Task<WorkerRead?> GetWorker(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/worker/{id}");
        string json = await response.Content.ReadAsStringAsync();
        
        WorkerRead? worker = JsonSerializer.Deserialize<WorkerRead>(json)??null;

        return worker;
    }

    public static async Task DeleteWorker(int id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/worker/{id}");
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("Deleted worker successfully.");
            MenuBuilder.EnterButtonPause();
            return;
        }
        AnsiConsole.MarkupLine("Failed to delete worker.");
        MenuBuilder.EnterButtonPause();
    }
    public static async Task<List<WorkerRead>> GetAllWorkers()
    {
        HttpResponseMessage response = await _client.GetAsync("api/worker/all");
        string json = await response.Content.ReadAsStringAsync();
       
        List<WorkerRead> workers = JsonSerializer.Deserialize<List<WorkerRead>>(json) ?? [];
        
        return workers;
    }

    public static async Task UpdateWorker(WorkerUpdate worker)
    {
        HttpResponseMessage response = await _client.PutAsJsonAsync($"api/worker", worker);
        
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("Updated worker successfully.");
            MenuBuilder.EnterButtonPause();
            return;
        }
        AnsiConsole.MarkupLine("Failed to update worker.");
        MenuBuilder.EnterButtonPause();
    }
    public static async Task AddWorker(WorkerCreate worker)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("api/Worker", worker);
        Console.WriteLine(response.IsSuccessStatusCode ? "Worker added" : response.Content.ReadAsStringAsync().Result);
        MenuBuilder.EnterButtonPause();
    }
    
}