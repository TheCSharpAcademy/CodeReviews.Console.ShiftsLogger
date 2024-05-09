using Newtonsoft.Json;
using ShiftLogger.samggannon.Models;
using shiftloggerconsole.Models;
using shiftloggerconsole.UserInterface;

namespace shiftloggerconsole.Services;

internal class ShiftLoggerService
{
    private static readonly HttpClient _httpClient;
    private static readonly string _apiBaseUrl;
    private static readonly string _endPointUrl;

    static ShiftLoggerService()
    {
        _httpClient = new HttpClient();
        _apiBaseUrl = "https://localhost:7205/";
        _endPointUrl = "api/WorkShifts";
    }

    // AddShift
    public static async Task InsertShiftAsync()
    {
        ///<summary>
        /// For brevivty, we will generate a random WorkerId,
        /// A standard american work shift is 8 hours,
        /// For simplicity, we will insert an 8 hour shift for a random generic employee and allow for changes later
        ///</summary>
        var shift = new WorkShift
        {
            WorkerId = GetRandomWorkerId(),
            ClockIn = DateTime.Now,
            ClockOut = DateTime.Now.AddHours(8),
            Duration = "8:00:00" // represent in time format
        };

        var serializedShift = JsonConvert.SerializeObject(shift);
        var content = new StringContent(serializedShift, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_apiBaseUrl + _endPointUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var createdShift = JsonConvert.DeserializeObject<WorkShift>(responseData);
            InformUser(true, $"New shift was created: WorkerId: {createdShift.WorkerId}" + Environment.NewLine + $"Clock In Time: {createdShift.ClockIn}" + Environment.NewLine + $"Clock Out Time: {createdShift.ClockOut}");

        }
        else
        {
            InformUser(false, "Failed to create shift");
        }
    }

    public static void GetAllShifts()
    {
        Console.Clear();
        List<Shift> shifts = new List<Shift>();

        try
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.GetAsync(_apiBaseUrl + _endPointUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    shifts = JsonConvert.DeserializeObject<List<Shift>>(responseData);
                }
                else
                {
                    InformUser(false, $"Failed to retrieve shifts. Status code : {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            InformUser(false, $"An error occurred: {ex.Message}");
        }

        Visualization.ShowTable(shifts);
    }



    private static void InformUser(bool isSuccessStatusCode, string infoMessage)
    {
        Console.Clear();
        if (isSuccessStatusCode)
        {
            Console.WriteLine(infoMessage);
        }
        else
        {
            Console.WriteLine(infoMessage);
        }

        Console.WriteLine("Press [enter] to go back");

        Utilities.Utilities.ConfirmKey();
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
