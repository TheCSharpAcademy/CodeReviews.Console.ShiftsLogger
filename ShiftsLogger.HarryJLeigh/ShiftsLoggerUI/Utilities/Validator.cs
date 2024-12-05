using Shared.Models;

namespace ShiftsLoggerUI.Utilities;

public static class Validator
{
    private static readonly string Format = "yyyy-MM-dd HH:mm";
    internal static bool IsDateTimeValid(string date)
    {
        DateTime parseDate = new();
        return DateTime.TryParseExact(date,
            Format,
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out parseDate);
    }

    internal static bool IsEndDateValid(string start, string end)
    {
        DateTime parsedStart = DateTime.ParseExact(start, Format, System.Globalization.CultureInfo.InvariantCulture);
        DateTime parsedEnd = DateTime.ParseExact(end, Format, System.Globalization.CultureInfo.InvariantCulture);
        
        TimeSpan difference = parsedStart - parsedEnd;
        if (difference.TotalHours < 0) return false;
        return true;
    }

    internal static bool IsStartDateValid(string start, string end)
    {
        DateTime parsedStart = DateTime.ParseExact(start, Format, System.Globalization.CultureInfo.InvariantCulture);
        DateTime parsedEnd = DateTime.ParseExact(end, Format, System.Globalization.CultureInfo.InvariantCulture);
        
        if (parsedStart > parsedEnd) return false;
        return true;
    }

    internal static bool CheckIfIdExists(int input, List<Shift> shifts)
    {
        foreach (Shift shift in shifts)
        {
            if (input == shift.Id) return true;
        }
        return false; 
    }

    internal static bool CheckIfPropertyNameExists(string input)
    {
        if (input == "start" || input == "end" || input == "both") return true;
        return false;
    }
}
