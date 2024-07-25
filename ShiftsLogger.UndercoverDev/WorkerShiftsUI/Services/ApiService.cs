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
        var response = await _httpClient!.GetAsync($"{_baseUrl}/api/workers");
        return await ApiResponseHandler.HandleResponse<List<Worker>>(response) ?? [];
    }

    public async Task<Worker?> GetWorkerAsync(int id)
    {
        var response = await _httpClient!.GetAsync($"{_baseUrl}/api/workers/{id}");
        return await ApiResponseHandler.HandleResponse<Worker>(response);
    }

    public async Task<Worker?> CreateWorkerAsync(Worker worker)
    {
        var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/api/workers", worker);
        return await ApiResponseHandler.HandleResponse<Worker>(response);
    }

    public async Task<Worker?> UpdateWorkerAsync(int id, Worker worker)
    {
        var response = await _httpClient!.PutAsJsonAsync($"{_baseUrl}/api/workers/{id}", worker);
        return await ApiResponseHandler.HandleResponse<Worker>(response);
    }

    public async Task DeleteWorkerAsync(int id)
    {
        var response = await _httpClient!.DeleteAsync($"{_baseUrl}/api/workers/{id}");
        await ApiResponseHandler.HandleResponse<object>(response);
    }

    // Shifts Methods
    public async Task<List<Shift>> GetShiftsAsync()
    {
        var response = await _httpClient!.GetAsync($"{_baseUrl}/api/shifts");
        return await ApiResponseHandler.HandleResponse<List<Shift>>(response) ?? [];
    }

    public async Task<Shift?> GetShiftAsync(int id)
    {
        var response = await _httpClient!.GetAsync($"{_baseUrl}/api/shift/{id}");
        return await ApiResponseHandler.HandleResponse<Shift>(response);
    }

    public async Task<Shift?> CreateShiftAsync(Shift shift)
    {
        var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/api/shifts", shift);
        return await ApiResponseHandler.HandleResponse<Shift>(response);
    }

    public async Task<Shift?> UpdateShiftAsync(int id, Shift shift)
    {
        var response = await _httpClient!.PutAsJsonAsync($"{_baseUrl}/api/shifts/{id}", shift);
        return await ApiResponseHandler.HandleResponse<Shift>(response);
    }

    public async Task DeleteShiftAsync(int id)
    {
        var response = await _httpClient!.DeleteAsync($"{_baseUrl}/api/shifts/{id}");
        await ApiResponseHandler.HandleResponse<object>(response);
    }
}
