using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FrontEnd.Controllers;
using Newtonsoft.Json;
using ShiftsLogger.Ryanw84.Models;
using Spectre.Console;

namespace FrontEnd.Services;

internal class UiWorkerService
{
    private readonly HttpClient _httpClient;

    // Constructor to initialize the HttpClient
    public UiWorkerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Worker>> GetAllWorkersAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("worker");
            response.EnsureSuccessStatusCode();

            // Deserialize the response content into a list of workers
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var workers = JsonConvert.DeserializeObject<List<Worker>>(jsonResponse);

            return workers ?? new List<Worker>();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error fetching workers: {ex.Message}[/]");
            return new List<Worker>();
        }
    }
}
