using System.Globalization;
using Spectre.Console;
namespace ShiftsLoggerUI;
public class Validation
{
    internal static bool ValidateId(string userInput)
    {
        List<ShiftModel> shifts = Service.SendGetRequest(1);
        if (string.IsNullOrEmpty(userInput)) return false;
        var parsedId = int.TryParse(userInput, out int id);
        if (parsedId == false) return parsedId;
        int lastId = 0;
        for (int i = 0; i <= shifts.Count; i++)
        {
            if (i == shifts.Count) lastId = (int)shifts[i - 1].id;
        }
        if (id > lastId) return false;
        if (id < 1) return false;
        else return true;
    }
    internal static DateTime ValidateDate(string date)
    {
        var parsed = DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate);
        while (!parsed)
        {
            date = AnsiConsole.Ask<string>("Enter the date(format dd-MM-yyyy):");
            parsed = DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
        }
        return parsedDate;
    }

    internal static DateTime ValidateStartTime(string startTime)
    {
        string format = "HH:mm";
        var parsed = DateTime.TryParseExact(startTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedStartTime);
        while (parsed == false)
        {
            Console.WriteLine("Invalid starting time.");
            startTime = AnsiConsole.Ask<string>("Enter the time you started your shift(format: 3:40 PM/AM): ");
            parsed = DateTime.TryParseExact(startTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedStartTime);
        }
        return parsedStartTime;
    }

    internal static DateTime ValidateEndTime(string endTime, DateTime startTime)
    {
        string format = "HH:mm";
        var parsed = DateTime.TryParseExact(endTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedEndTime);
        while (parsed == false || parsedEndTime < startTime)
        {
            Console.WriteLine("Invalid starting time.");
            endTime = AnsiConsole.Ask<string>("Enter the time you ended your shift(format: 3:40 PM/AM): ");
            parsed = DateTime.TryParseExact(endTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedEndTime);
            
        }
        return parsedEndTime;
    }
}