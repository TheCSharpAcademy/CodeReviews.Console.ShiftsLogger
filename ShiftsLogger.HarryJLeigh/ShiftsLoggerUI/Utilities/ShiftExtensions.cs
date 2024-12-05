using System.Globalization;
using Shared.Models;

namespace ShiftsLoggerUI.Utilities;

public static class ShiftExtensions
{
    internal static Shift GetShift()
    {
        DateTime startTime = GetStartTime();
        DateTime endTime = GetEndTime(startTime);
        double duration = GetShiftDuration(startTime, endTime);

        return new Shift
        {
            StartTime = startTime,
            EndTime = endTime,
            ShiftTime = duration
        };
    }
    
    private static DateTime GetEndTime(DateTime startTime)
    {
       return UserInput.DatePrompt("end", startTime.ToString("yyyy-MM-dd HH:mm"));
    }

    private static DateTime GetStartTime()
    {
        string format = "yyyy-MM-dd HH:mm";
        DateTime now = DateTime.Now;
        return DateTime.ParseExact(now.ToString(format), format, CultureInfo.InvariantCulture);
    }

    internal static double GetShiftDuration(DateTime startTime, DateTime endTime)
    {
        TimeSpan timeSpan = endTime - startTime;
        return Math.Round(timeSpan.TotalHours, 2);
    }
}