using System.Globalization;
using Spectre.Console;

namespace ShiftsLoggerClient.Utilities;

class DateTimeParser
{
    internal DateTime Parser(string time)
    {
        DateTime userParsedTime;
        while (!DateTime.TryParseExact(time, "dd-MM-yyyy HH:mm", new CultureInfo("en-GB"), DateTimeStyles.None, out userParsedTime))
        {
            time = AnsiConsole.Ask<string>("Invalid format. Please enter the date and time in 'dd-mm-yyyy hh:mm' format for example '24-02-2025 14:30'");
        }
        return userParsedTime;
    }
}