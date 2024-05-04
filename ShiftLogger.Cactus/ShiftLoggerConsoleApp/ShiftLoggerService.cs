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
            List<Shift> shifts = new List<Shift>();
            try
            {
                var name = AnsiConsole.Ask<string>("Empolyee's name:");

                string url = $"https://localhost:7256/shiftlogger/name/{name}";
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
        var dateStr = AnsiConsole.Ask<string>("Shift date (format: yyyy-MM-dd):");
        DateTime date;
        while (!IsValidDate(dateStr, out date))
        {
            dateStr = AnsiConsole.Ask<string>("Shift date (format: yyyy-MM-dd):");
        }

        var startTimeStr = AnsiConsole.Ask<string>("Start time (format: hh:mm:ss): ");
        TimeSpan startTime;
        while (!IsValidTime(startTimeStr, out startTime))
        {
            startTimeStr = AnsiConsole.Ask<string>("Start time (format: hh:mm:ss): ");
        }

        var endTimeStr = AnsiConsole.Ask<string>("End time (format: hh:mm:ss): ");
        TimeSpan endTime;
        while (!IsValidTime(endTimeStr, out endTime))
        {
            endTimeStr = AnsiConsole.Ask<string>("End time (format: hh:mm:ss): ");
        }

        while (endTime < startTime)
        {
            Console.WriteLine($"End time should late than start time {startTime}.");
            endTimeStr = AnsiConsole.Ask<string>("End time (format: hh:mm:ss): ");
            while (!IsValidTime(endTimeStr, out endTime))
            {
                endTimeStr = AnsiConsole.Ask<string>("End time (format: hh:mm:ss): ");
            }
        }

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
}

