using System.Globalization;

namespace ShiftLoggerCarDioLogicsUI;

internal class Validator
{
    public static bool IsValidDateInput(string input, out DateTime startDate)
    {
        return DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
    }

}
