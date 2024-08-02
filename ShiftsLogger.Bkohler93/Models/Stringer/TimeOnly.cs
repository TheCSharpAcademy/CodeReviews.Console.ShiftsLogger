namespace Models.Stringer;

public static class TimeOnlyExtensions
{
    public static string ToHourMinutes(this TimeOnly time)
    {
        return time.ToString("HH:mm");
    }
}