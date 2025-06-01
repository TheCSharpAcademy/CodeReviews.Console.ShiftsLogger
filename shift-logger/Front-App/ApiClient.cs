using API.Models;
using Microsoft.AspNetCore.Http;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Front_App;

public class ShiftApiClient : IShiftClient
{
    private readonly HttpClient _httpClient;

    public ShiftApiClient()
    {
        var handler = new HttpClientHandler
        {
            // AKCEPTUJ każdy certyfikat SSL — tylko na potrzeby testów!
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://localhost:7279")
        };
    }

    public async Task<List<Shift>> GetShiftsAsync()
    {
        HttpResponseMessage response = new();
        try
        {
            response = await _httpClient.GetAsync("/Shift");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red] {ex}[/]");
        }
        return await response.Content.ReadFromJsonAsync<List<Shift>>();
    }

    public async Task CreateShiftAsync(Shift shift)
    {
        HttpResponseMessage response = new();
        try
        {
            response = await _httpClient.PostAsJsonAsync("/Shift", shift);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red] {ex}[/]");
        }
    }

    public async Task UpdateShiftAsync(Shift shift)
    {
        HttpResponseMessage response = new();
        try
        {
            response = await _httpClient.PutAsJsonAsync("/Shift", shift);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red] {ex}[/]");
        }
    }

    public async Task DeleteShiftAsync(int id)
    {
        HttpResponseMessage response = new();
        try
        {
            response = await _httpClient.DeleteAsync($"/shift?id={id}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red] {ex}[/]");
        }
    }
}