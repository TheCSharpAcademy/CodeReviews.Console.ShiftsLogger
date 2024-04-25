using Spectre.Console;

namespace STUDY.ASP.ShiftLoggerTryThreeUserInterface;
internal class Validation{
    public static DateTime ValidateClockInDateTime(string prompt)
    {
        DateTime dateTime;
        while (true)
        {
            dateTime = AnsiConsole.Ask<DateTime>(prompt);
            if (dateTime < DateTime.MinValue)
            {
                AnsiConsole.WriteLine("Date and time cannot be earlier than minimum date.");
            }
            else if (dateTime.Year < 1995)
            {
                AnsiConsole.WriteLine("Our company was established in 1995. Can't clock in prior to 1995");
            }
            else if (dateTime > DateTime.Now)
            {
                AnsiConsole.WriteLine("Clock In Date cannot be in the future");
            }
            else
            {
                break;
            }
        }
        return dateTime;
    }

    public static DateTime ValidateClockOutDateTime(string prompt, DateTime clockIn)
    {
        DateTime clockOut;
        while (true)
        {
            clockOut = AnsiConsole.Ask<DateTime>(prompt);
            if (clockOut < DateTime.MinValue)
            {
                AnsiConsole.WriteLine("Clock out time cannot be earlier than minimum date.");
            }
            else if (clockOut < clockIn)
            {
                AnsiConsole.WriteLine("Clock out time cannot be earlier than clock in time.");
            }
            else if ((clockOut - clockIn).TotalHours > 24)
            {
                AnsiConsole.WriteLine("Did you really work more than 24 hours in 1 shift? Contact IT department if this is not a mistake");
            }
            else
            {
                break;
            }
        }
        return clockOut;
    }
}
