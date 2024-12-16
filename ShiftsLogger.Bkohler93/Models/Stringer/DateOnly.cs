namespace Models.Stringer;

public static class DateOnlyExtensions 
{
    public static string ToDayMonthYear(this DateOnly date)
    {
        return date.ToString("yyyy-MM-dd");
    }
}