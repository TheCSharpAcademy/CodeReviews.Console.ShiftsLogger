namespace ShiftsLoggerUi.App.Utils;

public class Time
{
    public static string Duration(DateTime startTime, DateTime endTime)
    {
        var duration = endTime.Subtract(startTime);

        return string.Format(
            $"{(int)duration.TotalHours,-3} h, {duration.Minutes,-3} m"
        );
    }
}