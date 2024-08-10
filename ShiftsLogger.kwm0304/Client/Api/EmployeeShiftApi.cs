using System.Net.Http.Json;
using Server.Models.Dtos;
using Shared;
using Spectre.Console;

namespace Client.Api;

public class EmployeeShiftApi(HttpClient http) : IBaseApi<EmployeeShift>(http, "/employee-shift")
{
    private readonly HttpClient _http = http;
    internal async Task<List<EmployeeShift>> GetEmployeeShiftByIds(int id)
    {
        try
        {
            return await _http.GetFromJsonAsync<List<EmployeeShift>>($"http://localhost:5062/api/employee-shift/employee/{id}") ?? [];
        }
        catch (Exception e)
        {
            AnsiConsole.WriteLine(e.Message);
            return [];
        }
    }

    internal async Task<List<EmployeeShift>> GetLateEmployees(int id)
    {
        try
        {
            List<EmployeeShift> lateEmployees = await _http.GetFromJsonAsync<List<EmployeeShift>>($"http://localhost:5062/api/employee-shift/late/{id}") ?? [];
            if (lateEmployees == null)
            {
                AnsiConsole.WriteLine("No late employees were found");
                return default!;
            }
            return lateEmployees;
        }
        catch (Exception e)
        {
            AnsiConsole.WriteLine($"Error retrieving late employees: " + e.Message);
            return default!;
        }
    }

    internal async Task<List<EmployeeShift>> GetAllEmployeesOnShift(int shiftId)
    {
        try
        {
            return await _http.GetFromJsonAsync<List<EmployeeShift>>($"http://localhost:5062/api/employee-shift/shift/{shiftId}") ?? [];
        }
        catch (Exception e)
        {
            AnsiConsole.WriteLine(e.Message);
            return [];
        }
    }
    internal async Task CreateEmployeeShift(EmployeeShiftDto dto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("http://localhost:5062/api/employee-shift/create", dto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            AnsiConsole.WriteLine(e.Message);
            return;
        }
    }
}