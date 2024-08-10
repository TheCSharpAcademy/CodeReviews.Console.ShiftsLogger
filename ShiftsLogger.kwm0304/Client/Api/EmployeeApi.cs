using System.Net.Http.Json;
using Shared;
using Shared.Enums;

namespace Client.Api;

public class EmployeeApi(HttpClient http) : IBaseApi<Employee>(http, "/employees")
{
  private readonly HttpClient _http = http;
  internal async Task<List<Employee>> GetEmployeeByShiftClassification(ShiftClassification classification)
    {
        return await _http.GetFromJsonAsync<List<Employee>>($"http://localhost:5062/api/employees/classification/{classification}") ?? [];
    }
}
