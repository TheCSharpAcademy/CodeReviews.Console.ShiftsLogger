using System.Globalization;
using Spectre.Console;

namespace ShiftsLoggerCLI;

public static class Validators
{
    public static ValidationResult DateTimeFormatValidator(string prompt)
    {
        if (DateTime.TryParseExact(
                prompt,
                "MM/dd/yyyy HH:mm",
                new CultureInfo("en-US"),
                DateTimeStyles.None,
                out _))
        {
            return ValidationResult.Success();
        };
        
        return ValidationResult.Error("[red]Time must be in format MM/dd/yyyy HH:mm (e.g., \"03/20/2025 15:30\".[/]");
    }

    public static ValidationResult EndTimeValidator(string prompt, DateTime start)
    {
        // Can't chain validators with Spectre, so call the format validator from here.
        var formatResult = DateTimeFormatValidator(prompt);
        if (!formatResult.Successful)
        {
            return formatResult;
        }
        
        var endTime = DateTime.ParseExact(prompt, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
        return endTime < start 
            ? ValidationResult.Error("[red]End date must be after start time.[/]") 
            : ValidationResult.Success();
    }

    public static ValidationResult EmpIdValidator(int arg)
    {
        return arg switch
        {
            < 0 => ValidationResult.Error("[red]Employee ID cannot be negative.[/]"),
            _ => ValidationResult.Success(),
        };
    }
}