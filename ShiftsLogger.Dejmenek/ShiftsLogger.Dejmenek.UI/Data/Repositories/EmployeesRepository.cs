using Microsoft.Extensions.Configuration;
using ShiftsLogger.Dejmenek.UI.Data.Interfaces;
using ShiftsLogger.Dejmenek.UI.Models;
using Spectre.Console;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ShiftsLogger.Dejmenek.UI.Data.Repositories;
internal class EmployeesRepository : IEmployeesRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _baseApiConnection;

    public EmployeesRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _baseApiConnection = $"{configuration.GetSection("BaseUrl").Value}/employees";
    }

    public async Task AddEmployeeAsync(EmployeeDTO employeeDto)
    {
        try
        {
            var payload = JsonSerializer.Serialize(employeeDto);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApiConnection, content);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                AnsiConsole.MarkupLine("Employee successfully created.");
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

    public async Task DeleteEmployeeAsync(int employeeId)
    {
        string url = $"{_baseApiConnection}/{employeeId}";
        try
        {
            var response = await _httpClient.DeleteAsync(url);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                AnsiConsole.MarkupLine("Employee successfully deleted.");
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

    public async Task<List<Employee>?> GetAllEmployeesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_baseApiConnection);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var employees = JsonSerializer.Deserialize<List<Employee>>(content);

                return employees;
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

    public async Task<List<Shift>?> GetEmployeeShiftsAsync(int employeeId)
    {
        string url = $"{_baseApiConnection}/{employeeId}/shifts";

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
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

    public async Task UpdateEmployeeAsync(int employeeId, EmployeeDTO employeeDto)
    {
        string url = $"{_baseApiConnection}/{employeeId}";

        try
        {
            var payload = JsonSerializer.Serialize(employeeDto);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                AnsiConsole.MarkupLine("Employee successfully updated");
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
