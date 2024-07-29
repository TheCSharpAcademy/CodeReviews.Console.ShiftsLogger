using Microsoft.Extensions.Configuration;
using ShiftsLogger.Dejmenek.UI.Data.Interfaces;
using ShiftsLogger.Dejmenek.UI.Models;
using Spectre.Console;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ShiftsLogger.Dejmenek.UI.Data.Repositories;
public class ShiftsRepository : IShiftsRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _baseApiConnection;

    public ShiftsRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _baseApiConnection = $"{configuration.GetSection("BaseUrl").Value}/shifts";
    }

    public async Task AddShift(ShiftAddDTO shiftDto)
    {
        try
        {
            var payload = JsonSerializer.Serialize(shiftDto);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApiConnection, content);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                AnsiConsole.MarkupLine("Shift successfully created");
            }
            else
            {
                AnsiConsole.MarkupLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"Error: {ex.Message}");
        }
    }

    public async Task DeleteShift(int shiftId)
    {
        string url = $"{_baseApiConnection}/{shiftId}";
        try
        {
            var response = await _httpClient.DeleteAsync(url);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                AnsiConsole.MarkupLine("Shift successfully deleted");
            }
            else
            {
                AnsiConsole.MarkupLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"Error: {ex.Message}");
        }
    }

    public async Task<List<Shift>?> GetAllShifts()
    {
        try
        {
            var response = await _httpClient.GetAsync(_baseApiConnection);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var shifts = JsonSerializer.Deserialize<List<Shift>>(content);

                return shifts;
            }
            else
            {
                AnsiConsole.MarkupLine($"Error: {response.StatusCode}");

                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"Error: {ex.Message}");

            return null;
        }
    }

    public async Task UpdateShift(int shiftId, ShiftUpdateDTO shiftDto)
    {
        string url = $"{_baseApiConnection}/{shiftId}";

        try
        {
            var payload = JsonSerializer.Serialize(shiftDto);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                AnsiConsole.MarkupLine("Shift successfully updated");
            }
            else
            {
                AnsiConsole.MarkupLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"Error: {ex.Message}");
        }
    }
}
