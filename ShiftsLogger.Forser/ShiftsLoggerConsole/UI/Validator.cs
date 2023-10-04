using System.Globalization;

namespace ShiftsLoggerConsole.UI
{
    internal class Validator
    {
        internal static bool ValidateDateTime(string shiftDate)
        {
            bool dateValid = false;

            if (DateTime.TryParseExact(shiftDate, "dd-MM-yy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                dateValid = true;
            }

            return dateValid;
        }

        internal static bool AreDatesValid(DateTime startDate, DateTime endDate)
        {
            if (startDate < endDate)
            {
                return true;
            }
            else
            {
                AnsiConsole.WriteLine("End of shift can't be before start of shift");
                return false;
            }
        }
    }
}