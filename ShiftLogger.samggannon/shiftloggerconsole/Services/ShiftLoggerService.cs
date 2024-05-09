using Microsoft.EntityFrameworkCore.Update.Internal;
using Newtonsoft.Json;
using ShiftLogger.samggannon.Models;
using shiftloggerconsole.Models;
using shiftloggerconsole.UserInterface;
using Spectre.Console;

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
        _endPointUrl = "api/WorkShifts/";
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
            Utilities.Utilities.InformUser(true, $"New shift was created: WorkerId: {createdShift.WorkerId}" + Environment.NewLine + $"Clock In Time: {createdShift.ClockIn}" + Environment.NewLine + $"Clock Out Time: {createdShift.ClockOut}");

        }
        else
        {
            Utilities.Utilities.InformUser(false, "Failed to create shift");
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
                    Utilities.Utilities.InformUser(false, $"Failed to retrieve shifts. Status code : {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.Utilities.InformUser(false, $"An error occurred: {ex.Message}");
        }

        Visualization.ShowTable(shifts);

        Console.WriteLine("Press [enter] to continue");
        Utilities.Utilities.ConfirmKey();
    }

    internal static void DeleteShiftById()
    {
        GetAllShifts();

        Console.WriteLine("\nPlease select the id of the shift you wish to delete");
        var shiftId = Console.ReadLine();

        Shift shift = new Shift();

        try
        {

            HttpResponseMessage response = _httpClient.GetAsync(_apiBaseUrl + _endPointUrl + shiftId).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                shift = JsonConvert.DeserializeObject<Shift>(responseData);
            }
            else
            {
                Utilities.Utilities.InformUser(false, "Please ensure the Id of the shift is valid and try again");
            }

        }
        catch (Exception ex)
        {
            Utilities.Utilities.InformUser(false, $"An error occurred: {ex.Message}");
        }

        Visualization.ShowRow(shift);
    }

    #region Edit Shift
    internal static void EditShift()
    {
        GetAllShifts();

        Console.WriteLine("\nPlease select the id of the shift you wish to edit");
        var shiftId = Console.ReadLine();

        Shift shift = new Shift();

        try
        {

            HttpResponseMessage response = _httpClient.GetAsync(_apiBaseUrl + _endPointUrl + shiftId).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                shift = JsonConvert.DeserializeObject<Shift>(responseData);
            }
            else
            {
                Utilities.Utilities.InformUser(false, "Please ensure the Id of the shift is valid and try again");
                MainMenu.ShowMenu();
            }
            
        }
        catch (Exception ex)
        {
            Utilities.Utilities.InformUser(false, $"An error occurred: {ex.Message}");
        }

        Visualization.ShowRow(shift);
        ConfirmEdit(shift, "edit");
    }

    private static void ConfirmEdit(Shift? shift, string? editMethod)
    {
        var isConfirmed = AnsiConsole.Confirm($"Is this the record you wish to {editMethod}?");
        if (isConfirmed)
        {
            if(editMethod == "edit")
            {
                shift.ClockIn = PunchIn();
                shift.ClockOut = PunchOut();
                shift.Duration = CalculateTime(shift.ClockIn, shift.ClockOut);

                UpdateShift(shift);
            }

            if(editMethod == "delete")
            {
                DeleteShift(shift);
            }
        }

    }

    private static void DeleteShift(Shift? shift)
    {
        var serializedShift = JsonConvert.SerializeObject(shift);
        //var content = new StringContent(serializedShift, System.Text.Encoding.UTF8, "application/json");
        var response = _httpClient.DeleteAsync(_apiBaseUrl + _endPointUrl + shift.Id).Result;

        if (response.IsSuccessStatusCode)
        {
            var responseData = response.Content.ReadAsStringAsync().Result;
            var updatedShift = JsonConvert.DeserializeObject<WorkShift>(responseData);
            Utilities.Utilities.InformUser(true, $"New shift was deleted: shift Id: {shift.Id}");

        }
        else
        {
            Utilities.Utilities.InformUser(false, "Failed to delete shift");
        }
    }

    private static void UpdateShift(Shift shift)
    {
        var serializedShift = JsonConvert.SerializeObject(shift);
        var content = new StringContent(serializedShift, System.Text.Encoding.UTF8, "application/json");
        var response = _httpClient.PutAsync(_apiBaseUrl + _endPointUrl + shift.Id, content).Result;

        if (response.IsSuccessStatusCode)
        {
            //var responseData = response.Content.ReadAsStringAsync().Result;
            //var updatedShift = JsonConvert.DeserializeObject<WorkShift>(responseData);
            Utilities.Utilities.InformUser(true, $"Shift was updated: WorkerId: {shift.WorkerId}" + Environment.NewLine + $"Clock In Time: {shift.ClockIn}" + Environment.NewLine + $"Clock Out Time: {shift.ClockOut}");

        }
        else
        {
            Utilities.Utilities.InformUser(false, "Failed to create shift");
        }
    }

    #endregion

    #region Property Helpers
    private static int GetRandomWorkerId()
    {
        int[] workerId = { 123456, 789012, 345678, 901234, 567890 };

        Random rand = new Random();
        int randomIndex = rand.Next(0, workerId.Length);


        return workerId[randomIndex];
    }

    private static DateTime PunchIn()
    {
        Console.WriteLine("Please enter the clock-in date and time (format: yyyy-MM-dd HH:mm:ss):");
        while (true)
        {
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter the date and time in the format yyyy-MM-dd HH:mm:ss");
            }
        }
    }

    private static DateTime PunchOut()
    {
        Console.WriteLine("Please enter the clock-out date and time (format: yyyy-MM-dd HH:mm:ss):");
        while (true)
        {
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter the date and time in the format yyyy-MM-dd HH:mm:ss");
            }
        }
    }

    private static string CalculateTime(DateTime clockIn, DateTime clockOut)
    {
        TimeSpan duration = clockOut - clockIn;
        return duration.ToString(@"hh\:mm\:ss");
    }

    #endregion
}
