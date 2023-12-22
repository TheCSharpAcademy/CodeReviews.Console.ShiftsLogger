using System.Globalization;
using Spectre.Console;

namespace ShiftsLoggerUI.Util;

public static class UserInput
{
    public static string GetName()
    {
        var name = AnsiConsole.Ask<string>("Please enter the name for the shift:");
        return name;
    }

    public static DateTime GetStartDate(DateTime? endDate = null)
    {
        var prompt = new TextPrompt<DateTime>("Please enter the start date of the shift: ").ValidationErrorMessage("Please enter using correct date and time format");
        if (endDate != null)
        {
            prompt.Validate(startDate =>
            {
                if (endDate > startDate)
                {
                    return ValidationResult.Success();
                }
                else
                {
                    return ValidationResult.Error($"The start date should be lower than the end date, end date is {endDate.ToString()}");
                }
            });
        }
        var dateTime = AnsiConsole.Prompt(prompt);
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
            .ValidationErrorMessage("Please enter using correct date and time format")
            .Validate(endDate =>
            {
                if (endDate > startDate)
                {
                    return ValidationResult.Success();
                }
                else
                {
                    return ValidationResult.Error($"The end date should be higher than start date, start date is {startDate.ToString(CultureInfo.CurrentCulture)}");
                }
            }));
        return dateTime;
    }

    public static void GetKeyToContinue()
    {
        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
    }
}