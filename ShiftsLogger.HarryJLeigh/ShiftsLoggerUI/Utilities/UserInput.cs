using Shared.Models;
using Spectre.Console;

namespace ShiftsLoggerUI.Utilities;

public static class UserInput
{
    private const string DateFormat = "yyyy-MM-dd HH:mm";
    internal static DateTime DatePrompt(string text, string? startTime = null, string? endTime = null)
    {
        // Set prompt message
        string promptMessage = $"Enter {text} date with format '{DateFormat}'.";

        // Get validation logic based on prompt type
        var validation = GetDateValidationLogic(text, startTime, endTime);

        // Prompt user input with validation
        string answer = AnsiConsole.Prompt(
            new TextPrompt<string>(promptMessage).Validate(validation)
        );

        // Parse and return DateTime
        return DateTime.ParseExact(answer, DateFormat, System.Globalization.CultureInfo.InvariantCulture);
    }


    internal static Shift IdPromptToGetShift(List<Shift> shifts)
    {
        string promptMessage = $"Select ID from the shifts above:";

        int selectedId = AnsiConsole.Prompt(
            new TextPrompt<int>(promptMessage)
                .Validate(input =>
                    Validator.CheckIfIdExists(input, shifts)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]ID does not exist. Please try again.[/]")
                ));

        foreach (Shift shift in shifts)
        {
            if (shift.Id == selectedId) return shift;
        }

        return new Shift();
    }

    internal static string PromptUserForShiftProperty()
    {
        string promptMessage = "What would you like to update? ([green]start[/]/[yellow]end[/]/[cyan]both[/]) of shift: ";

        var answer = AnsiConsole.Prompt(
            new TextPrompt<string>(promptMessage)
                .Validate(input =>
                    Validator.CheckIfPropertyNameExists(input.ToLower())
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Invalid input. Please input either start/end/both: [/]")));

        return answer;
    }

    private static Func<string, ValidationResult> GetDateValidationLogic(string text, string? startTime,
        string? endTime)
    {
        return text.ToLower() switch
        {
            // Validation for "start"
            "start" => input =>
            {
                if (!Validator.IsDateTimeValid(input))
                {
                    return ValidationResult.Error($"[red]Enter a valid date with format: {DateFormat}[/]");
                }
                if (!Validator.IsStartDateValid(input, endTime))
                {
                    return ValidationResult.Error("[red]Please enter a valid date before the end date.[/]");
                }
                return ValidationResult.Success();
            },
            // Validation for "end"
            "end" when startTime != null => input =>
            {
                if (!Validator.IsDateTimeValid(input))
                {
                    return ValidationResult.Error($"[red]Enter a valid date with format: {DateFormat}[/]");
                }
                if (!Validator.IsEndDateValid(input, startTime))
                {
                    return ValidationResult.Error("[red]Enter a valid end date after the start date.[/]");
                }
                return ValidationResult.Success();
            }
        };
    }
}