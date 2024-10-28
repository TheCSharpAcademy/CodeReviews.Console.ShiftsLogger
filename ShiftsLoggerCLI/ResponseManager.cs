using System.Net.Http.Json;
using System.Text.Json;
using ShiftsLoggerCLI.Models;
using Spectre.Console;

namespace ShiftsLoggerCLI;

public static class ResponseManager
{
    private static readonly HttpClient Client ;

    static ResponseManager()
    {
        Uri baseUri = new("http://localhost:5105/");
        Client = new HttpClient();
        Client.BaseAddress = baseUri;
        PositionManager.SetPositions(GetAllWorkers().Result);
    }

    public static async Task<List<ShiftRead>> GetShiftsByWorkerId(int workerId)
    {
        var response = await Client.GetAsync($"api/shifts/{workerId}");
        string json = await response.Content.ReadAsStringAsync();
        var shifts = JsonSerializer.Deserialize<List<ShiftRead>>(json) ?? new();
        return shifts;
    }
    public static async Task<WorkerRead?> GetWorker(int id)
    {
        HttpResponseMessage response = await Client.GetAsync($"api/worker/{id}");
        string json = await response.Content.ReadAsStringAsync();
        
        WorkerRead? worker = JsonSerializer.Deserialize<WorkerRead>(json)??null;

        return worker;
    }

    public static async Task DeleteWorker(int id)
    {
        HttpResponseMessage response = await Client.DeleteAsync($"api/worker/{id}");
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("[green]Deleted worker successfully.[/]");
            MenuBuilder.EnterButtonPause();
            return;
        }
        AnsiConsole.MarkupLine("[red]Failed to delete worker.[/]");
        MenuBuilder.EnterButtonPause();
    }
    public static async Task<List<WorkerRead>> GetAllWorkers()
    {
        HttpResponseMessage response = await Client.GetAsync("api/worker/all");
        string json = await response.Content.ReadAsStringAsync();
       
        List<WorkerRead> workers = JsonSerializer.Deserialize<List<WorkerRead>>(json) ?? [];
        
        return workers;
    }

    public static async Task UpdateWorker(WorkerUpdate worker)
    {
        HttpResponseMessage response = await Client.PutAsJsonAsync($"api/worker", worker);
        
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("[green]Updated worker successfully.[/]");
            MenuBuilder.EnterButtonPause();
            return;
        }
        AnsiConsole.MarkupLine("[red]Failed to update worker.[/]");
        MenuBuilder.EnterButtonPause();
    }
    public static async Task AddWorker(WorkerCreate worker)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync("api/Worker", worker);
        Console.WriteLine(response.IsSuccessStatusCode ? "Worker added" : response.Content.ReadAsStringAsync().Result);
        MenuBuilder.EnterButtonPause();
    }

    public static async Task AddShift(ShiftCreate shift)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync("api/shifts", shift);
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("[green]Added Shift Successfully.[/]");
            return;
        }   
        AnsiConsole.MarkupLine("[red]Failed to add Shift.[/]");
    }

    public static async Task DeleteShift(int shiftId)
    {
        HttpResponseMessage response = await Client.DeleteAsync($"api/shifts/{shiftId}");
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("[green]Deleted Shift successfully.[/]");
            return;
        }
        AnsiConsole.MarkupLine("[red]Failed to delete Shift.[/]");
    }

    public static async Task UpdateShift(ShiftUpdate shift)
    {
        HttpResponseMessage response = await Client.PutAsJsonAsync($"api/shifts", shift);
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("[green]Updated Shift Successfully.[/]");
            return;
        }
        AnsiConsole.MarkupLine("[red]Failed to update Shift.[/]");
    }

    public static async Task<List<ShiftRead>> GetShifts(int workerId)
    {
        HttpResponseMessage response = await Client.GetAsync($"api/shifts/{workerId}");
        var shifts = JsonSerializer.Deserialize<List<ShiftRead>>(await response.Content.ReadAsStringAsync())??new();
        return shifts;
    }

    public static async Task<List<ShiftRead>> GetAllShifts()
    {
        HttpResponseMessage response = await Client.GetAsync("api/shifts/all");
        var shifts = JsonSerializer.Deserialize<List<ShiftRead>>(await response.Content.ReadAsStringAsync())??new();
        return shifts;
    }
}