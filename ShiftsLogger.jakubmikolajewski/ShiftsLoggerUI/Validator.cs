using Spectre.Console;

namespace ShiftsLoggerUI;
public class Validator
{
    public static DateOnly ValidateDate()
    {
        return AnsiConsole.Prompt(new TextPrompt<DateOnly>($"Enter a date (yyyy-MM-dd):")
            .ValidationErrorMessage("Please enter a date (yyyy-MM-dd)\n"));
    }

    public static TimeOnly ValidateTime(string type)
    {
        return AnsiConsole.Prompt(new TextPrompt<TimeOnly>($"Enter the {type} time:")
            .ValidationErrorMessage("Please enter a valid time (HH:mm:ss)\n"));
    }
}
