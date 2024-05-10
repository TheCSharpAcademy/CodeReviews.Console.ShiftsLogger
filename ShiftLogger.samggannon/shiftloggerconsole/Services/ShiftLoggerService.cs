using Microsoft.EntityFrameworkCore.Update.Internal;
using Newtonsoft.Json;
using ShiftLogger.samggannon.Models;
using shiftloggerconsole.Models;
using shiftloggerconsole.UserInterface;
using Spectre.Console;

namespace shiftloggerconsole.Services;

internal class ShiftLoggerService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;
    private readonly string _endPointUrl;

    public ShiftLoggerService(HttpClient httpClient, string apiBaseUrl, string endPointUrl)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _apiBaseUrl = apiBaseUrl ?? throw new ArgumentNullException(nameof(apiBaseUrl));
        _endPointUrl = endPointUrl ?? throw new ArgumentNullException(nameof(endPointUrl));
    }

    // AddShift
    public async Task InsertShiftAsync()
    {
        ///<summary>
        /// For brevivty, we will generate a random WorkerId,
        /// A standard american work shift is 8 hours,
        /// For simplicity, we will insert an 8 hour shift for a random generic employee and allow for changes later
        ///</summary>

        Console.WriteLine("inserting record. Please wait...");

        var shift = new WorkShift
        {
            WorkerId = GetRandomWorkerId(),
            ClockIn = DateTime.Now,
            ClockOut = DateTime.Now.AddHours(8),
            Duration = "8:00:00" // represent in time format
        };

        var serializedShift = JsonConvert.SerializeObject(shift);
        var content = new StringContent(serializedShift, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = _httpClient.PostAsync(_apiBaseUrl + _endPointUrl, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                var createdShift = JsonConvert.DeserializeObject<WorkShift>(responseData);
                Utilities.Utilities.InformUser(true, $"New shift was created: WorkerId: {createdShift.WorkerId}" + Environment.NewLine + $"Clock In Time: {createdShift.ClockIn}" + Environment.NewLine + $"Clock Out Time: {createdShift.ClockOut}");

            }
            else
            {
                Utilities.Utilities.InformUser(false, "Failed to create shift");
            }
        }
        catch (Exception ex)
        {
            Utilities.Utilities.InformUser(false, $"An error occurred: {ex.Message}");
        }

    }

    public void GetAllShifts()
    {
        Console.Clear();
        List<Shift> shifts = new List<Shift>();

        try
        {

            HttpResponseMessage response = _httpClient.GetAsync(_apiBaseUrl + _endPointUrl).Result;

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
        catch (Exception ex)
        {
            Utilities.Utilities.InformUser(false, $"An error occurred: {ex.Message}");
        }

        Visualization.ShowTable(shifts);

        Console.WriteLine("Press [enter] to continue");
        Utilities.Utilities.ConfirmKey();
    }

    #region Edit Shift
    internal void EditShift()
    {
        GetAllShifts();

        Console.WriteLine("\nPlease select the id of the shift you wish to edit");
        var selectedId = Console.ReadLine();
        var shiftId = 0;

        while (!Int32.TryParse(selectedId, out shiftId))
        {
            Console.WriteLine("please enter a shift id. it must be a number");
            selectedId = Console.ReadLine();
        }

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
                return;
            }
            
        }
        catch (Exception ex)
        {
            Utilities.Utilities.InformUser(false, $"An error occurred: {ex.Message}");
        }

        Visualization.ShowRow(shift);
        ConfirmEdit(shift, "edit");
    }

    private void ConfirmEdit(Shift? shift, string? editMethod)
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

    internal void DeleteShiftById()
    {
        GetAllShifts();

        Console.WriteLine("\nPlease select the id of the shift you wish to delete and then press [enter]");
        var selectedShift = Console.ReadLine();
        var shiftId = 0;

        while (!Int32.TryParse(selectedShift, out shiftId))
        {
            Console.WriteLine("please enter a shift id. it must be a number");
            selectedShift = Console.ReadLine();
        }

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
        ConfirmEdit(shift, "delete");
    }

    private void DeleteShift(Shift? shift)
    {
        // var serializedShift = JsonConvert.SerializeObject(shift);
        // var content = new StringContent(serializedShift, System.Text.Encoding.UTF8, "application/json");
        var response = _httpClient.DeleteAsync(_apiBaseUrl + _endPointUrl + shift.Id).Result;

        if (response.IsSuccessStatusCode)
        {
            //var responseData = response.Content.ReadAsStringAsync().Result;
            //var updatedShift = JsonConvert.DeserializeObject<WorkShift>(responseData);
            Utilities.Utilities.InformUser(true, $"Shift was deleted: shift Id: {shift.Id}");

        }
        else
        {
            Utilities.Utilities.InformUser(false, "Failed to delete shift");
        }
    }

    private void UpdateShift(Shift shift)
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
