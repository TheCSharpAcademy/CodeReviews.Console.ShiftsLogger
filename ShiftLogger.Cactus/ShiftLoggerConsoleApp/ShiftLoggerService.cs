using ShiftLoggerConsoleApp.UI;
using Spectre.Console;
using System.Text;
using System.Text.Json;

namespace ShiftLoggerConsoleApp;

public class ShiftLoggerService
{
    public async static Task<List<Shift>> GetShifts()
    {
        using (HttpClient client = new HttpClient())
        {
            List<Shift> shifts = new List<Shift>();
            try
            {
                string url = "https://localhost:7256/shiftlogger";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return shifts;
        }
    }

    public async static Task<List<Shift>> GetShiftByName()
    {
        using (HttpClient client = new HttpClient())
        {
            List<Shift> selectedShifts = new List<Shift>();
            try
            {
                var shifts = await GetShifts();

                if (shifts is null || shifts.Count <= 0)
                {
                    return selectedShifts;
                }

                List<string> uniqueNames = shifts.Select(shift => shift.EmployeeName).Distinct().ToList();

                var selectedName = AnsiConsole.Prompt(
                    new SelectionPrompt<String>()
                        .Title("What would you like to view?")
                        .AddChoices(uniqueNames));

                selectedShifts = shifts.Where(shift => shift.EmployeeName.Equals(selectedName)).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return selectedShifts;
        }
    }

    public async static Task<Shift> AddNewShift()
    {
        Shift addedShift = null;
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var shift = InputShift();
                string jsonContent = await GetJson(shift);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                string url = "https://localhost:7256/shiftlogger";
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("ADD successfully.");
                    var stream = await response.Content.ReadAsStreamAsync();
                    addedShift = await JsonSerializer.DeserializeAsync<Shift>(stream);
                }
                else
                {
                    Console.WriteLine($"Request failed with status code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Print any exceptions that occurred
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        return addedShift;
    }

    private static async Task<string> GetJson(object shift)
    {
        using (var stream = new MemoryStream())
        {
            await JsonSerializer.SerializeAsync(stream, shift, shift.GetType());
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }

    private static object InputShift()
    {
        var name = AnsiConsole.Ask<string>("Empolyee's name:");
        Console.WriteLine("Plase type date");
        DateTime date = GetValidDate();
        Console.WriteLine("Plase type start time");
        TimeSpan startTime = GetValidTime();
        Console.WriteLine("Plase type end time");
        TimeSpan endTime = GetValidEndTime(startTime);
        var shift = new { EmployeeName = name, ShiftDate = date, ShiftStartTime = startTime, ShiftEndTime = endTime };
        return shift;
    }

    static bool IsValidDate(string dateStr, out DateTime date)
    {

        if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out date))
        {
            return true;
        }
        return false;
    }

    static bool IsValidTime(string timeString, out TimeSpan timeSpan)
    {
        if (TimeSpan.TryParseExact(timeString, "hh\\:mm\\:ss", null, out timeSpan))
        {
            return true;
        }
        return false;
    }

    public async static Task<List<Shift>> DeleteShift()
    {
        var deletedShifts = new List<Shift>();
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var shifts = await GetShifts();

                if (shifts is null || shifts.Count <= 0)
                {
                    return deletedShifts;
                }

                List<string> uniqueNames = shifts.Select(shift => shift.EmployeeName).Distinct().ToList();

                var selectedName = AnsiConsole.Prompt(
                    new SelectionPrompt<String>()
                        .Title("What would you like to delete?")
                        .AddChoices(uniqueNames));

                string url = $"https://localhost:7256/shiftlogger/{selectedName}";
                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("DELETE successfully.");
                    var stream = await response.Content.ReadAsStreamAsync();
                    deletedShifts = await JsonSerializer.DeserializeAsync<List<Shift>>(stream);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        return deletedShifts;
    }

    public async static Task<Shift> UpdateShift()
    {
        Shift updatedShfit = null;
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var shifts = await GetShifts();

                if (shifts is null || shifts.Count <= 0)
                {
                    return updatedShfit;
                }

                List<string> uniqueNames = shifts.Select(shift => shift.EmployeeName).Distinct().ToList();

                var selectedName = AnsiConsole.Prompt(
                    new SelectionPrompt<String>()
                        .Title("Please choose the empolyee you like to update?")
                        .AddChoices(uniqueNames));

                var shiftsByName = shifts
                    .GroupBy(s => s.EmployeeName)
                    .ToDictionary(g => g.Key, g => g.ToList());

                UserInterface.ShowShifts(shiftsByName[selectedName]);

                var ids = shiftsByName[selectedName].Select(shift => shift.Id.ToString()).ToList();

                var selectedId = AnsiConsole.Prompt(
                    new SelectionPrompt<String>()
                        .Title("Please choose the shift id you like to update?")
                        .AddChoices(ids));

                var selectedShift = shiftsByName[selectedName].Where(shift => shift.Id == int.Parse(selectedId)).ToList()[0];

                var shiftDate = AnsiConsole.Confirm("Update Shift Date?") ? GetValidDate() : selectedShift.ShiftDate;
                var shiftStartTime = AnsiConsole.Confirm("Update Shift START Time?") ? GetValidTime() : selectedShift.ShiftStartTime;
                var shiftEndTime = AnsiConsole.Confirm("Update Shift END Time?") ? GetValidEndTime(shiftStartTime) : selectedShift.ShiftEndTime;

                var shift = new { Id = selectedId, EmployeeName = selectedName, ShiftDate = shiftDate, ShiftStartTime = shiftStartTime, ShiftEndTime = shiftEndTime };
                string jsonContent = await GetJson(shift);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                string url = $"https://localhost:7256/shiftlogger/{selectedId}";
                HttpResponseMessage response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("UPDATE successfully");
                    var stream = await response.Content.ReadAsStreamAsync();
                    updatedShfit = await JsonSerializer.DeserializeAsync<Shift>(stream);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        return updatedShfit;
    }

    private static TimeSpan GetValidEndTime(TimeSpan startTime)
    {
        TimeSpan endTime = GetValidTime();
        while (endTime < startTime)
        {
            Console.WriteLine($"End time should late than start time {startTime}.");
            var endTimeStr = AnsiConsole.Ask<string>("End time (format: hh:mm:ss): ");
            while (!IsValidTime(endTimeStr, out endTime))
            {
                endTimeStr = AnsiConsole.Ask<string>("End time (format: hh:mm:ss): ");
            }
        }
        return endTime;
    }

    private static TimeSpan GetValidTime()
    {
        var timeStr = AnsiConsole.Ask<string>("Shift time (format: hh:mm:ss): ");
        TimeSpan time;
        while (!IsValidTime(timeStr, out time))
        {
            timeStr = AnsiConsole.Ask<string>("Shift time (format: hh:mm:ss): ");
        }
        return time;
    }

    private static DateTime GetValidDate()
    {
        var dateStr = AnsiConsole.Ask<string>("Shift date (format: yyyy-MM-dd):");
        DateTime date;
        while (!IsValidDate(dateStr, out date))
        {
            dateStr = AnsiConsole.Ask<string>("Shift date (format: yyyy-MM-dd):");
        }

        return date;
    }
}

