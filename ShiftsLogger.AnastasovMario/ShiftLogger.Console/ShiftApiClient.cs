using Newtonsoft.Json;
using ShiftLogger.Console.Handlers;
using ShiftsLogger.API.Models.Shifts;
using Spectre.Console;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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

    public async Task AddShiftAsync()
    {
      try
      {
        var startShift = Validator.ValidateDate(AnsiConsole.Ask<string>("Enter the START of the shift (MM/dd/yyyy HH:mm): \n"));
        var endShift = Validator.ValidateDate(AnsiConsole.Ask<string>("Enter the END of the shift (MM/dd/yyyy HH:mm): \n"));

        if (!Validator.ValidateDuration(startShift, endShift))
        {
          await AddShiftAsync();
        }

        var shift = new Shift()
        {
          WorkerId = 1,
          Start = startShift,
          End = endShift
        };

        var response = await _httpClient.PostAsJsonAsync("api/shifts/add", shift);

        if (response.IsSuccessStatusCode)
        {
          var jsonResponse = await response.Content.ReadAsStringAsync();
          Console.WriteLine($"Scuccessfully created shift:\n {jsonResponse}\n");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"{ex.Message}");
      }
    }

    public async Task DeleteShiftAsync()
    {
      try
      {
        var shiftId = AnsiConsole.Ask<int>("Enter the Id of the shift you want to DELETE \n");
        var response = await _httpClient.DeleteAsync($"api/shifts/{shiftId}");

        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine($"Successfully deleted a shift");
        }
        else
        {
          Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public async Task UpdateShiftAsync()
    {
      try
      {
        var shiftId = AnsiConsole.Ask<int>("Enter the Id of the shift you want to Edit \n");

        var startShift = Validator.ValidateDate(AnsiConsole.Ask<string>("Enter the START of the shift (MM/dd/yyyy HH:mm): \n"));
        var endShift = Validator.ValidateDate(AnsiConsole.Ask<string>("Enter the END of the shift (MM/dd/yyyy HH:mm): \n"));

        if (!Validator.ValidateDuration(startShift, endShift))
        {
          await UpdateShiftAsync();
        }

        var shift = new ShiftEditDto()
        {
          StartShift = startShift,
          EndShift = endShift
        };

        var response = await _httpClient.PostAsJsonAsync($"api/shifts/edit/{shiftId}", shift);

        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine("The shift was updated successfully");
        }
        else
        {
          var jsonResponse = await response.Content.ReadAsStringAsync();
          Console.WriteLine(jsonResponse);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public async Task GetShiftAsync()
    {
      try
      {
        int shiftId = AnsiConsole.Ask<int>("Enter the Id of the shift you want to GET \n");

        var response = await _httpClient.GetAsync($"api/shifts/{shiftId}");

        var jsonResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
          var shift = JsonConvert.DeserializeObject<ShiftDto>(jsonResponse)!;

          UserInterface.ShowShift(shift);

          Helper.ContinueMessage();
        }
        else
        {
          Console.WriteLine(jsonResponse);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
