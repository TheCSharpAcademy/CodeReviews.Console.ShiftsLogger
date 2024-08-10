using System.Net.Http.Json;
using Shared;
using Spectre.Console;

namespace Client.Api;

public class ShiftApi(HttpClient http) : IBaseApi<Shift>(http, "/shifts")
{
  private readonly HttpClient _http = http;

  internal async Task<Shift?> GetNewestShift()
  {
    try
    {
      return await _http.GetFromJsonAsync<Shift>("http://localhost:5062/api/shifts/new");
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      return null;
    }
  }
}
