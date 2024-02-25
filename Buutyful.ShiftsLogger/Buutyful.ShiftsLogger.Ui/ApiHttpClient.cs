﻿using Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;
using System.Text.Json;

namespace Buutyful.ShiftsLogger.Ui;

public class ApiHttpClient
{
    const string baseUrl = "https://localhost:44331/api/";
    private readonly HttpClient httpClient = new();
    public async Task<List<WorkerResponse>> GetWorkersAsync()
    {
        var endPoint = "workers";
        var (status, content) = await SendRequestAsync(endPoint);
        if (status)
        {
            return JsonSerializer.Deserialize<List<WorkerResponse>>(content) ?? [];
        }
        else 
        {
            Console.WriteLine($"{content}");
            return [];
        }
    }
    private async Task<(bool Status, string Content)> SendRequestAsync(string endpoint)
    {
        string url = $"{baseUrl}{endpoint}";

        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return (true, await response.Content.ReadAsStringAsync());
            }
            else
            {
                return (false, $"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in SendRequestAsync: {ex.Message}");
            return (false, $"Exception: {ex.Message}");
        }
    }
}