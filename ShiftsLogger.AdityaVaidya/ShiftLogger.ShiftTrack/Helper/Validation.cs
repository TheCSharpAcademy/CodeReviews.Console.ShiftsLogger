using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ShiftLogger.ShiftTrack.Views;

namespace ShiftLogger.ShiftTrack.Helper;

internal class Validation
{
    internal static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("The email address is empty or contains only whitespace.");
            return false;
        }
        if (email.Length > 256)
        {
            Console.WriteLine("The email address is too long.");
            return false;
        }
        if (!Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
        {
            Console.WriteLine("The email address format is invalid.");
            return false;
        }
        if (!new EmailAddressAttribute().IsValid(email))
        {
            Console.WriteLine("The email address failed the EmailAddressAttribute validation.");
            return false;
        }
        return true;
    }

    internal static bool IsGivenInputInteger(string? input)
    {
        if (int.TryParse(input, out _))
            return true;
        else
            return false;
    }

    internal static bool TryParseDate(string input, out DateOnly date)
    {
        date = default;

        if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            if (parsedDate <= DateTime.Now.Date) // Check if parsed date is not greater than current date
            {
                date = DateOnly.FromDateTime(parsedDate);
                return true;
            }
            else
                Console.WriteLine("Dates from futute are not allowed");
        }
        return false;
    }

    internal static bool ValidateInputDate(string? date)
    {
        if (TryParseDate(date, out DateOnly Date))
            return true;
        return false;
    }

    internal static bool TryParseTime(string? input, out TimeOnly time)
    {
        time = default;

        if (DateTime.TryParseExact(input, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime))
        {
            time = TimeOnly.FromDateTime(parsedTime);
            return true;
        }
        return false;
    }

    internal static bool ValidateStartSessionTime(string? time)
    {
        if (TryParseTime(time, out TimeOnly Time))
        {
            return true;
        }
        return false;
    }

    internal static bool ValidateEndSessionTime(string? startTime, string? endTime)
    {
        if (TryParseTime(endTime, out TimeOnly Time))
        {
            if(TimeOnly.Parse(startTime) < Time)
                return true;
        }
        return false;
    }
    
    internal static string CalculateShiftDuration(string? startTime, string? endTime)
    {
        TimeOnly start = TimeOnly.Parse(startTime);
        TimeOnly end = TimeOnly.Parse(endTime);
        TimeSpan timeSpan = end - start;
        return timeSpan.ToString();
    }


}
