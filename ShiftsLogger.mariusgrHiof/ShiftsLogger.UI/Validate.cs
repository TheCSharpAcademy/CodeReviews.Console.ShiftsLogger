using ShiftsLogger.API.DTOs.Shift;
using System.Globalization;
using System.Text.Json;

namespace ShiftsLogger.UI;
public static class Validate
{
    public static bool IsValidateDate(string date)
    {
        return DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm", new CultureInfo("nb-NO"), DateTimeStyles.None, out _);
    }

    public static bool IsValidDateRange(DateTime DateStart, DateTime DateEnd)
    {
        TimeSpan timeSpan = DateEnd - DateStart;

        return timeSpan.Ticks > 0;

    }
    public static bool IsValidNumber(string number)
    {
        return int.TryParse(number, out _);
    }

    public static bool IsValidWorkerId(string inputId)
    {
        HttpClient client = new HttpClient();

        var endpoint = new Uri("https://localhost:7184/api/workers");

        if (!IsValidNumber(inputId)) return false;

        int id = int.Parse(inputId);
        var result = client.GetAsync($"{endpoint}/{id}").Result;

        if (result.IsSuccessStatusCode)
        {
            var json = result.Content.ReadAsStringAsync().Result;

            UpdateShiftDto? shift = JsonSerializer.Deserialize<UpdateShiftDto>(json);

            if (shift == null)
            {
                return false;

            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsValidShiftId(string inputId)
    {
        HttpClient client = new HttpClient();

        var endpoint = new Uri("https://localhost:7184/api/shiftsLogger");

        if (!IsValidNumber(inputId)) return false;

        int id = int.Parse(inputId);
        var result = client.GetAsync($"{endpoint}/{id}").Result;

        if (result.IsSuccessStatusCode)
        {
            var json = result.Content.ReadAsStringAsync().Result;

            UpdateShiftDto? shift = JsonSerializer.Deserialize<UpdateShiftDto>(json);

            if (shift == null)
            {
                return false;

            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsValidString(string name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }
}