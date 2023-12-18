using Spectre.Console;

namespace ShiftsLoogerUI.Util;

public class UserInput
{
    public static string GetName()
    {
        var name = AnsiConsole.Ask<string>("Please enter the name for the shift:");
        return name;
    }

    public static DateTime GetStartDate()
    {
        var dateTime = AnsiConsole.Prompt(new TextPrompt<DateTime>("Please enter the start date of the shift:").ValidationErrorMessage("Invalid Date Time"));
        return dateTime;
    }

    public static string GetComment()
    {
        var comment = AnsiConsole.Ask<string>("Please enter the comment for the shift");
        return comment;
    }

    public static DateTime GetEndDate(DateTime startDate)
    {
        var dateTime = AnsiConsole.Prompt(new TextPrompt<DateTime>("Please enter the end date of the shift:")
            .Validate(endDate =>
            {
                if (endDate > startDate)
                {
                    return ValidationResult.Success();
                }
                else
                {
                    return ValidationResult.Error("The end date should be higher than start date");
                }
            }));
        return dateTime;
    }
}