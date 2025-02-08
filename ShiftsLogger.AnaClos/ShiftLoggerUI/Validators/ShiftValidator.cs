using ShiftsLoggerWebAPI.DTOs;
using System.Globalization;

namespace ShiftLoggerUI.Validators;

public class ShiftValidator
{
    public bool IntervalValidator(DateTime start, DateTime end)
    {
        TimeSpan timeSpan = end - start;
        double eightHours = 3600 * 8;
        return timeSpan.TotalSeconds > 0 && timeSpan.TotalSeconds <= eightHours ? true : false;
    }

    public DateTime DateTimeValidator(string date)
    {
        DateTime dateTime = DateTime.MinValue;
        DateTime.TryParseExact(date, "yy/MM/dd HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None, out dateTime);
        return dateTime;
    }

    public bool OrderValidator(ShiftDto lastShift, DateTime newStartTime)
    {
        return newStartTime - lastShift.EndTime >= TimeSpan.Zero ? true : false;
    }
}