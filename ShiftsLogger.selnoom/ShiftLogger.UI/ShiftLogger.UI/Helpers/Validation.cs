using Spectre.Console;

namespace ShiftsLogger.UI.Helpers;

public static class Validation
{
    public static DateTime? ValidateTimeInput()
    {
        string timeInput = AnsiConsole.Ask<string>("Enter date (yyyy-MM-dd HH:mm) or 0 to return to menu:");
        DateTime parsedDate;
        while (!DateTime.TryParseExact(timeInput, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out parsedDate))
        {
            if (timeInput == "0")
            {
                return null;
            }

            AnsiConsole.MarkupLine("[bold red]Invalid input. Please try again.[/]\n");
            timeInput = AnsiConsole.Ask<string>("Enter date (yyyy-MM-dd HH:mm) or 0 to return to menu:");
        }
        return parsedDate;
    }

    internal static DateTime? ValidateEndTimeInput(DateTime? startTime)
    {
        DateTime? endTime;

        while (true)
        {
            endTime = ValidateTimeInput();

            if (endTime == null)
            {
                return null;
            }

            if (endTime < startTime)
            {
                AnsiConsole.MarkupLine("[bold red]The end time cannot be before the start time. Please try again.[/]\n");
            }
            else
            {
                return endTime;
            }
        }
    }

}
