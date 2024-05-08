using shiftloggerconsole.Models;
using ShiftLogger.samggannon.Controllers;
using Spectre.Console;
using Newtonsoft.Json;
using ShiftLogger.samggannon.Models;

namespace shiftloggerconsole.Services;

internal class ShiftLoggerService
{
    private static readonly HttpClient _httpClient;
    private static readonly string _apiBaseUrl;

    static ShiftLoggerService()
    {
        _httpClient = new HttpClient();
        _apiBaseUrl = "base url here";
    }

    // AddShift
    public static async Task InsertShiftAsync()
    {
        ///<summary>
        /// For brevivty, we will generate a random WorkerId,
        /// WorkerId would be a FK to the Employees table,
        /// Employees table would have fields such as WorkerId, FirstName, LastName, etc..
        /// A standard american work shift is 8 hours,
        /// For simplicity, we will insert an 8 hour shift for a random generic employee and allow for changes later
        ///</summary>
        var shift = new WorkShift
        {
            WorkerId = GetRandomWorkerId(),
            ClockIn = DateTime.Now,
            ClockOut = DateTime.Now.AddHours(8),
            Duration = "8 hours" // represent in time format
        };

        var serializedShift = JsonConvert.SerializeObject(shift);
        var content = new StringContent(serializedShift, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_apiBaseUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var createdShift = JsonConvert.DeserializeObject<WorkShift>(responseData);
            Console.WriteLine($"New shift created with ID: {createdShift.Id}");
        }
        else
        {
            Console.WriteLine($"Failed to create shift. Status code: {response.StatusCode}");
        }
    }

    private static int GetRandomWorkerId()
    {
        int[] workerId = { 123456, 789012, 345678, 901234, 567890 };
        
        Random rand = new Random();
        int randomIndex = rand.Next(0, workerId.Length);

       
        return workerId[randomIndex];
    }

    private string CalculateTime(DateTime clockIn, DateTime clockOut)
    {
        throw new NotImplementedException();
    }

    private DateTime PunchOut()
    {
        throw new NotImplementedException();
    }

    private DateTime PunchIn()
    {
        throw new NotImplementedException();
    }

    //ShowAllShifts,
    //ShowShiftById,
    //EditShiftById,
    //DeleteShiftById,
}
