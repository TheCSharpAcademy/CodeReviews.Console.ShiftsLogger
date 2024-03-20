using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ShiftApi.Models;
using ShiftUI.Controllers;
using System.ComponentModel;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShiftUI;

internal class ValidationEngine
{
    internal static bool ValidDate(string date)
    {
        if (string.IsNullOrWhiteSpace(date)) { return true; }
        return DateTime.TryParseExact(date, "MM-dd-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _);
    }

    internal static bool ValidTime(string timeString)
    {
        return TimeSpan.TryParse(timeString, out _);
    }

    internal static bool ValidShiftEndTime(TimeSpan shiftStartTime, TimeSpan shiftEndTime)
    {
        var timeDifference = shiftEndTime - shiftStartTime;

        if (timeDifference.TotalHours < 0)
        {
            timeDifference = timeDifference.Add(TimeSpan.FromDays(1));
        }

        if (timeDifference.TotalHours > 16)
        {
            return false;
        }

        return true;
    }
}