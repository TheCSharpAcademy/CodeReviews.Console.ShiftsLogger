using System.Globalization;
using Spectre.Console;

namespace ShiftsLoggerUI.UI;

public class UserInput
{
    public static int GetEmployeeId(int currentId = 0)
    {
        return AnsiConsole.Prompt(new TextPrompt<int>("Employee ID:")
            .DefaultValue(currentId));
    }

    public static string GetDate(DateTime? currentDate = null)
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Shift Date (dd/mm/yy):")
            .DefaultValue(currentDate != null ? currentDate.Value.ToShortDateString() : DateTime.Now.ToString("dd/MM/yy"))
            .Validate(
                x => DateTime.TryParseExact(x, "dd/MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _),
                "Please enter a valid shift date in the format dd/mm/yy"));
    }

    public static string GetTime(bool isStartTime, DateTime? startTime = null)
    {
        if (isStartTime)
        {
            return AnsiConsole.Prompt(new TextPrompt<string>("Start Shift Time (hh:mm):")
                .DefaultValue(startTime != null ? startTime.Value.ToShortTimeString() : DateTime.Now.ToString("HH:mm"))
                .Validate(
                    x => DateTime.TryParseExact(x, @"H\:m", CultureInfo.InvariantCulture, DateTimeStyles.None, out _),
                    "Please enter a valid shift time in the format hh:mm")
                );
        }
        return AnsiConsole.Prompt(new TextPrompt<string>("End Shift Time (hh:mm):")
            .DefaultValue(startTime != null ? startTime.Value.ToShortTimeString() : DateTime.Now.ToString("HH:mm"))
            .Validate(
                x => DateTime.TryParseExact(x, @"H\:m", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTime) && TimeOnly.FromDateTime(parsedTime) > TimeOnly.FromDateTime(startTime.Value),
                "Please enter a valid shift time in the format hh:mm, that is also later that your shift start time")
        );
    }

    public static string GetComment(string currentComment = "")
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Shift comment (optional):")
            .DefaultValue(currentComment));
    }
}