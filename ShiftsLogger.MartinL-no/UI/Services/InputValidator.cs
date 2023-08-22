using System.Globalization;

namespace UI.Services;

internal static class InputValidator
{
    private static string _format = "yyyy-MM-dd HH:mm";
    private static CultureInfo _cultureInfo = new CultureInfo("en-US");
    private static DateTimeStyles _style = DateTimeStyles.None;

    public static bool IsValidDate(string dateTimeString, out DateTime dateValue)
    {

        if (DateTime.TryParseExact(dateTimeString, _format, _cultureInfo, _style, out dateValue))
        {
            return true;
        }
        return false;
    }

    public static DateTime ParseDateTime(string dateTimeString)
    {
        return DateTime.ParseExact(dateTimeString, _format, _cultureInfo, _style);
    }
}

