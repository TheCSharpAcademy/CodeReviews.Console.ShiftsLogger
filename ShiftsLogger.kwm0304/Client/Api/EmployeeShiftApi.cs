using System.Net.Http.Json;
using Shared;

namespace Client.Api;

public class EmployeeShiftApi(HttpClient http) : IBaseApi<EmployeeShift>(http, "/employee-shift")//check endpoint
{
    private readonly HttpClient _http = http;
    internal async Task<List<EmployeeShift>> GetEmployeeShiftByIds(int id)//empId
    {
        return await _http.GetFromJsonAsync<List<EmployeeShift>>($"/employee-shift/employee/{id}") ?? [];
    }

    internal async Task<List<EmployeeShift>> GetLateEmployees(int id)//shiftId
    {
        return await _http.GetFromJsonAsync<List<EmployeeShift>>($"/employee-shift/late/{id}") ?? [];
    }

    internal async Task<List<EmployeeShift>> GetAllEmployeesOnShift(int shiftId)
    {
        return await _http.GetFromJsonAsync<List<EmployeeShift>>($"/employee-shift/shift/{shiftId}") ?? [];
    }
}