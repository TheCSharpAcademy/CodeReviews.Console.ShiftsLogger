using Newtonsoft.Json;
using ShiftLogger.samggannon.Models;
using shiftloggerconsole.Models;
using shiftloggerconsole.UserInterface;
using Spectre.Console;
using static shiftloggerconsole.Helpers.Helpers;

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

    public async Task InsertShiftAsync()
    {
        ///<summary>
        /// For brevivty, we will generate a random WorkerId,
        /// A standard american work shift is 8 hours,
        /// For simplicity, we will insert an 8 hour shift for a random generic employee and allow for changes later
        ///</summary>

        Console.WriteLine("inserting record. Please wait...");

        var shift = GenerateRandomShift();

        try
        {
            var serializedShift = JsonConvert.SerializeObject(shift);
            var content = new StringContent(serializedShift, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiBaseUrl + _endPointUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var createdShift = JsonConvert.DeserializeObject<WorkShift>(responseData);
                InformUser($"New shift was created: WorkerId: {createdShift.WorkerId}\n" +
                       $"Clock In Time: {createdShift.ClockIn}\n" +
                       $"Clock Out Time: {createdShift.ClockOut}");
            }
        }
        catch (Exception ex)
        {
            InformUser($"An error occurred: {ex.Message}");
        }
    }

    private Shift GenerateRandomShift()
    {
        return new Shift
        {
            WorkerId = GetRandomWorkerId(),
            ClockIn = DateTime.Now,
            ClockOut = DateTime.Now.AddHours(8),
            Duration = TimeSpan.FromHours(8).ToString(@"hh\:mm\:ss")
        };
    }

    private static int GetRandomWorkerId()
    {
        int[] workerId = { 123456, 789012, 345678, 901234, 567890 };

        Random rand = new Random();
        int randomIndex = rand.Next(0, workerId.Length);

        return workerId[randomIndex];
    }

    public async Task GetAllShifts()
    {
        Console.Clear();
        Console.WriteLine("Retreveing records. Please wait...");

        List<Shift> shifts = new List<Shift>();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_apiBaseUrl + _endPointUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                shifts = JsonConvert.DeserializeObject<List<Shift>>(responseData);
                Visualization.ShowTable(shifts);
            }
        }
        catch (Exception ex)
        {
            InformUser($"An error occurred: {ex.Message}");
        }

        Console.WriteLine("Press [enter] to continue");
        ConfirmKey();
    }

    internal async Task EditShift()
    {
        GetAllShifts();

        var shiftId = UserInput.GetShiftIdInput();

        Shift shift = new Shift();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_apiBaseUrl + _endPointUrl + shiftId);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                shift = JsonConvert.DeserializeObject<Shift>(responseData);
            }
            else
            {
                InformUser("Please ensure the Id of the shift is valid and try again");
                return;
            }
        }
        catch (Exception ex)
        {
            InformUser($"An error occurred: {ex.Message}");
        }

        Visualization.ShowRow(shift);
        ConfirmEdit(shift, "edit");
    }

    private void ConfirmEdit(Shift? shift, string? editMethod)
    {
        var isConfirmed = AnsiConsole.Confirm($"Is this the record you wish to {editMethod}?");
        if (isConfirmed)
        {
            if (editMethod == "edit")
            {
                shift.ClockIn = UserInput.GetPunchIn();
                shift.ClockOut = UserInput.GetPunchOut();
                shift.Duration = CalculateTime(shift.ClockIn, shift.ClockOut);

                UpdateShift(shift);
            }

            if (editMethod == "delete")
            {
                DeleteShift(shift);
            }
        }
    }

    internal async Task DeleteShiftById()
    {
        await GetAllShifts();

        var shiftId = UserInput.GetShiftIdInput();

        Shift shift = new Shift();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_apiBaseUrl + _endPointUrl + shiftId);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                shift = JsonConvert.DeserializeObject<Shift>(responseData);
            }
            else
            {
                InformUser("Please ensure the Id of the shift is valid and try again");
            }

        }
        catch (Exception ex)
        {
            InformUser($"An error occurred: {ex.Message}");
        }

        Visualization.ShowRow(shift);
        ConfirmEdit(shift, "delete");
    }

    private async Task DeleteShift(Shift? shift)
    {
        Console.WriteLine($"Deleting shift with Shiftd; {shift.Id}");
        try
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + _endPointUrl + shift.Id);

            if (response.IsSuccessStatusCode)
            {
                InformUser($"Shift was deleted: shift Id: {shift.Id}");
            }
        }
        catch (Exception ex)
        {
            InformUser($"Failed to delete shift:\n {ex.Message}");
        }
    }

    private async Task UpdateShift(Shift shift)
    {
        Console.WriteLine($"Updating shift with ShiftId: {shift.Id} ");
        try
        {
            var serializedShift = JsonConvert.SerializeObject(shift);
            var content = new StringContent(serializedShift, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_apiBaseUrl + _endPointUrl + shift.Id, content);

            if (response.IsSuccessStatusCode)
            {
                InformUser($"Shift was updated: WorkerId: {shift.WorkerId}" + Environment.NewLine + $"Clock In Time: {shift.ClockIn}" + Environment.NewLine + $"Clock Out Time: {shift.ClockOut}");
            }
        }
        catch (Exception ex)
        {
            InformUser($"Failed to create shift:\n {ex.Message}");
        }
    }

    private static string CalculateTime(DateTime clockIn, DateTime clockOut)
    {
        TimeSpan duration = clockOut - clockIn;
        return duration.ToString(@"hh\:mm\:ss");
    }
}
