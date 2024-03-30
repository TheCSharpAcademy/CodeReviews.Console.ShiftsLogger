using System.Net.Http.Headers;
using System.Text.Json;
using ShiftsLoggerLibrary.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using ShiftsLoggerLibrary.DTOs;

namespace ShiftsLoggerUI;

public class DataAccess
{
    public string BasePath { get; set; }
    private readonly HttpClient _client;

    public DataAccess()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        BasePath = GetConfigurationValue(configuration, "ShiftLoggerAPI", "BasePath");

        _client = new();
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private static string GetConfigurationValue(IConfiguration configuration, string section, string key)
    {
        string? value = configuration.GetSection(section)[key];
        if (value == null)
        {
            return $"{key} not found";
        }
        return value;
    }

    public async Task<IEnumerable<Shift>?> GetShiftsAsync(int id)
    {
        try
        {
            var json = await _client.GetStringAsync($"{BasePath}/employee/{id}");
            return JsonSerializer.Deserialize<IEnumerable<Shift>>(json);
        }
        catch (JsonException)
        {
            return [];
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while making the HTTP request: {ex.Message}");
        }

        return [];
    }

    public async Task<Shift?> GetRunningShiftAsync(int id)
    {
        try
        {
            var json = await _client.GetStringAsync($"{BasePath}/employee/{id}/running");
            return JsonSerializer.Deserialize<Shift>(json);
        }
        catch (JsonException)
        {
            return null;
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"An error occurred while making the HTTP request: {ex.Message}");
            }
        }

        return null;
    }

    public async Task<Shift?> StartShiftAsync(StartShift startShift)
    {
        try
        {
            var jsonShift = JsonSerializer.Serialize(startShift);
            HttpContent content = new StringContent(jsonShift, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{BasePath}/start", content);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Shift>(json);
        }
        catch (JsonException)
        {
            return null;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while making the HTTP request: {ex.Message}");
        }

        return null;
    }

    public async Task<Shift?> EndShiftAsync(EndShift endShift)
    {
        try
        {
            var jsonShift = JsonSerializer.Serialize(endShift);
            HttpContent content = new StringContent(jsonShift, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{BasePath}/end", content);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Shift>(json);
        }
        catch (JsonException)
        {
            return null;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while making the HTTP request: {ex.Message}");
        }

        return null;
    }

    public async Task<Shift?> UpdateShiftAsync(Shift shift)
    {
        try
        {
            var jsonShift = JsonSerializer.Serialize(shift);
            HttpContent content = new StringContent(jsonShift, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{BasePath}/update", content);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Shift>(json);
        }
        catch (JsonException)
        {
            return null;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while making the HTTP request: {ex.Message}");
        }

        return null;
    }

    public async Task<bool> DeleteShiftAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{BasePath}/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while making the HTTP request: {ex.Message}");
        }

        return false;
    }
}