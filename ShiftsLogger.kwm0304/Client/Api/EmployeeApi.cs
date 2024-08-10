using System.Net.Http.Json;
using Shared;
using Shared.Enums;
using Spectre.Console;

namespace Client.Api;

public class EmployeeApi(HttpClient http) : IBaseApi<Employee>(http, "/employees")
{
  private readonly HttpClient _http = http;
  internal async Task<List<Employee>> GetEmployeeByShiftClassification(ShiftClassification classification)
    {
      try
      {
        return await _http.GetFromJsonAsync<List<Employee>>($"http://localhost:5062/api/employees/classification/{classification}") ?? [];
      }
      catch (Exception e)
      {
        AnsiConsole.WriteLine(e.Message);
        return [];
      }
    }
}
