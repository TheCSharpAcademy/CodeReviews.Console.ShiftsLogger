using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using WorkerShiftsUI.Models;
using WorkerShiftsUI.Utilities;

namespace WorkerShiftsUI.Services;
public class ApiService
{
    private readonly HttpClient? _httpClient;
    private readonly string? _baseUrl;

    public ApiService()
    {
        _httpClient = new HttpClient();
        _baseUrl = "https://localhost:7267";
    }

    // Workers Methods
    public async Task<List<Worker>> GetWorkersAsync()
    {
        try
        {
            var response = await _httpClient!.GetAsync($"{_baseUrl}/api/workers");
            return await ApiResponseHandler.HandleResponse<List<Worker>>(response) ?? [];
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
            return [];
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return [];
        }
    }

    public async Task<Worker?> GetWorkerAsync(int id)
    {
        try
        {
            var response = await _httpClient!.GetAsync($"{_baseUrl}/api/workers/{id}");
            return await ApiResponseHandler.HandleResponse<Worker>(response);
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
            return null;
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task<Worker?> CreateWorkerAsync(Worker worker)
    {
        try
        {
            var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/api/workers", worker);
            return await ApiResponseHandler.HandleResponse<Worker>(response);
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
            return null;
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task<Worker?> UpdateWorkerAsync(int id, Worker worker)
    {
        try
        {
            var response = await _httpClient!.PutAsJsonAsync($"{_baseUrl}/api/workers/{id}", worker);
            return await ApiResponseHandler.HandleResponse<Worker>(response);
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
            return null;
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task DeleteWorkerAsync(int id)
    {
        try
        {
            var response = await _httpClient!.DeleteAsync($"{_baseUrl}/api/workers/{id}");
            await ApiResponseHandler.HandleResponse<object>(response);
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    // Shifts Methods
    public async Task<List<Shift>> GetShiftsAsync()
    {
        try
        {
            var response = await _httpClient!.GetAsync($"{_baseUrl}/api/shifts");
            return await ApiResponseHandler.HandleResponse<List<Shift>>(response) ?? [];
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
            return [];
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return [];
        }
    }

    public async Task<Shift?> GetShiftAsync(int id)
    {
        try
        {
            var response = await _httpClient!.GetAsync($"{_baseUrl}/api/shift/{id}");
            return await ApiResponseHandler.HandleResponse<Shift>(response);
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
            return null;
        }  
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task<Shift?> CreateShiftAsync(Shift shift)
    {
        try
        {
            var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/api/shifts", shift);
            return await ApiResponseHandler.HandleResponse<Shift>(response);
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
            return null;
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task<Shift?> UpdateShiftAsync(int id, Shift shift)
    {
        try
        {
            var response = await _httpClient!.PutAsJsonAsync($"{_baseUrl}/api/shifts/{id}", shift);
            return await ApiResponseHandler.HandleResponse<Shift>(response);
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
            return null;
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task DeleteShiftAsync(int id)
    {
        try
        {
            var response = await _httpClient!.DeleteAsync($"{_baseUrl}/api/shifts/{id}");
            await ApiResponseHandler.HandleResponse<object>(response);
        }
        catch (HttpRequestException ex)
        {
            HandleException(ex);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private static void HandleException(Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}