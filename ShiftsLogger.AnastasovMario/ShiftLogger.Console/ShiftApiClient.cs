using Newtonsoft.Json;
using ShiftsLogger.API.Models.Shifts;
using System.Net.Http.Headers;

namespace ShiftLoggerConsoleUI
{
  public class ShiftApiClient
  {
    private readonly HttpClient _httpClient;

    public ShiftApiClient(string baseUrl)
    {
      _httpClient = new HttpClient()
      {
        BaseAddress = new Uri(baseUrl)
      };

      _httpClient.DefaultRequestHeaders.Accept.Clear();
      _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task GetAllShiftsAsync()
    {

      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync("api/shifts");

        if (response.IsSuccessStatusCode)
        {
          var jsonResponse = await response.Content.ReadAsStringAsync();
          var shifts = JsonConvert.DeserializeObject<IEnumerable<ShiftDto>>(jsonResponse)!;
          UserInterface.ShowShifts(shifts);

          Helper.ContinueMessage();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
