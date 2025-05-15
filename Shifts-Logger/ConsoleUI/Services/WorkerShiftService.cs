using ConsoleUI.Models;
using Spectre.Console;
using System.Net.Http.Json;

namespace ConsoleUI.Services;

internal class WorkerShiftService
{
    private readonly HttpClient _httpClient;

    public WorkerShiftService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(@"https://localhost:7042/");
    }

    internal async Task<bool> CreateNewShift(Shift shift)
    {
        try
        {
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + $"api/Shifts/{shift.WorkerId}", shift);
            return response.IsSuccessStatusCode;
        }catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[maroon]Error Creating Shift: {ex.Message}[/]");
            return false;
        }
    }

    internal async Task<bool> CreateNewWorker(Worker worker)
    {
        try
        {
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "api/Shifts/", worker);
            return response.IsSuccessStatusCode;
        }catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[maroon]Error Creating Worker: {ex.Message}[/]");
            return false;
        }
    }

    internal async Task<List<Shift>> GetAllShifts()
    {
        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/Shifts/");
            response.EnsureSuccessStatusCode();

            List<Shift> responseBody = await response.Content.ReadFromJsonAsync<List<Shift>>() ?? new List<Shift>();

            return responseBody;

        }catch (Exception ex)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[maroon]An unexpected error occurred trying to connect to the API. \nMessage: {ex.Message}[/]\n");
            return new List<Shift>();
        }
    }

    internal async Task<List<Worker>> GetAllWorkers()
    {
        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/Shifts/Workers/");
            response.EnsureSuccessStatusCode();

            List<Worker> responseBody = await response.Content.ReadFromJsonAsync<List<Worker>>() ?? new List<Worker>();
            return responseBody;
        }catch(Exception ex)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[maroon]An unexpected error occurred trying to connect to the API. \nMessage: {ex.Message}[/]\n");
            return new List<Worker>();
        }
    }

    internal async Task<bool> UpdateShift(Shift shift, string managerCode)
    {
        try
        {
            using HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(_httpClient.BaseAddress + $"api/Shifts/{shift.WorkerId}/{shift.Id}"),
                Content = JsonContent.Create(shift)
            };
            request.Headers.Add("X-Manager-Code", managerCode);

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;

        }catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[maroon]Error Updating Shift: {ex.Message}[/]");
            return false;
        }
    }

    internal async Task<bool> UpdateWorker(Worker worker, string managerCode)
    {
        try
        {
            using HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(_httpClient.BaseAddress + $"api/Shifts/{worker.Id}"),
                Content = JsonContent.Create(worker)
            };
            request.Headers.Add("X-Manager-Code", managerCode);

            using var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
            
        }catch(Exception ex)
        {
            AnsiConsole.MarkupLine($"[maroon]Error Updating Worker: {ex.Message}[/]");
            return false;
        }
    }

    internal async Task<bool> DeleteShift(Shift shift, string managerCode)
    {
        try
        {
            using HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + $"api/Shifts/{shift.WorkerId}/{shift.Id}"),
            };
            request.Headers.Add("X-Manager-Code", managerCode);
            using HttpResponseMessage reponse = await _httpClient.SendAsync(request);
            
            return reponse.IsSuccessStatusCode;
        }catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[maroon]Error Deleting Shift: {ex.Message}[/]");
            return false;
        }
    }

    internal async Task<bool> DeleteWorker(Worker worker, string managerCode)
    {
        try
        {
            using HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + $"api/Shifts/{worker.Id}/"),
            };
            request.Headers.Add("X-Manager-Code", managerCode );

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }catch(Exception ex)
        {
            AnsiConsole.MarkupLine($"[maroon]Error Deleting Worker: {ex.Message}[/]");
            return false;
        }
    }
}
