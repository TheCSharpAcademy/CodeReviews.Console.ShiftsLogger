using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using WorkerShiftsUI.Models;

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
        return await _httpClient!.GetFromJsonAsync<List<Worker>>($"{_baseUrl}/api/workers") ?? [];
    }

    public async Task<Worker?> GetWorkerAsync(int id)
    {
        return await _httpClient!.GetFromJsonAsync<Worker>($"{_baseUrl}/api/workers/{id}");
    }

    public async Task<Worker?> CreateWorkerAsync(Worker worker)
    {
        var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/api/workers", worker);
        return await response.Content.ReadFromJsonAsync<Worker>();
    }

    public async Task UpdateWorkerAsync(int id, Worker worker)
    {
        await _httpClient!.PutAsJsonAsync($"{_baseUrl}/api/workers/{id}", worker);
    }

    public async Task DeleteWorkerAsync(int id)
    {
        await _httpClient!.DeleteAsync($"{_baseUrl}/api/workers/{id}");
    }

    // Shifts Methods
    public async Task<List<Shift>> GetShiftsAsync()
    {
        return await _httpClient!.GetFromJsonAsync<List<Shift>>($"{_baseUrl}/api/shifts");
    }

    public async Task<Shift> GetShiftAsync(int id)
    {
        return await _httpClient!.GetFromJsonAsync<Shift>($"{_baseUrl}/api/shifts/{id}");
    }

    public async Task<Shift> CreateShiftAsync(Shift shift)
    {
        var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/api/shifts", shift);
        return await response.Content.ReadFromJsonAsync<Shift>();
    }

    public async Task UpdateShiftAsync(int id, Shift shift)
    {
        await _httpClient!.PutAsJsonAsync($"{_baseUrl}/api/shifts/{id}", shift);
    }

    public async Task DeleteShiftAsync(int id)
    {
        await _httpClient!.DeleteAsync($"{_baseUrl}/api/shifts/{id}");
    }
}
