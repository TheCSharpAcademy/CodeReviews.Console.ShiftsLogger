using System.Net.Http.Json;
using Shared;

namespace Client.Api;

public class EmployeeShiftApi(HttpClient http) : IBaseApi<EmployeeShift>(http, "/employee-shift")
{
    private readonly HttpClient _http = http;
    internal async Task<List<EmployeeShift>> GetEmployeeShiftByIds(int id)
    {
        return await _http.GetFromJsonAsync<List<EmployeeShift>>($"https://localhost:7066/api/employee-shift/employee/{id}") ?? [];
    }

    internal async Task<List<EmployeeShift>> GetLateEmployees(int id)
    {
        return await _http.GetFromJsonAsync<List<EmployeeShift>>($"https://localhost:7066/api/employee-shift/late/{id}") ?? [];
    }

    internal async Task<List<EmployeeShift>> GetAllEmployeesOnShift(int shiftId)
    {
        return await _http.GetFromJsonAsync<List<EmployeeShift>>($"https://localhost:7066/api/employee-shift/shift/{shiftId}") ?? [];
    }
}