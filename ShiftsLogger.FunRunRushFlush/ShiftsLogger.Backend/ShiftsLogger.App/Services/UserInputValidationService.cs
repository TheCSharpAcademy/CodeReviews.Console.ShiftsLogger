using ShiftsLogger.Domain;
using ShiftsLogger.App.Services.Interfaces;
using Spectre.Console;
using Microsoft.Extensions.Logging;

namespace ShiftsLogger.App.Services;

public class UserInputValidationService : IUserInputValidationService
{
    private readonly ILogger<UserInputValidationService> _log;

    public UserInputValidationService(ILogger<UserInputValidationService> log)
    {
        _log = log;
    }

    public Shift ValidateUserInput(Shift? existingEntry = null)
    {

        int? id = existingEntry?.Id;
        DateTime shiftStart = default;
        DateTime shiftEnd = default;

        AnsiConsole.MarkupLine("[yellow]Please provide the Name, ShiftDescription and the ShiftTimes.[/]");

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Enter your [green]Full Name[/] (max 100 characters):[/]")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input) || input.Length > 100)
                    {
                        return ValidationResult.Error("[red]Please enter a valid name (up to 100 characters).[/]");
                    }
                    return ValidationResult.Success();
                }));


        var description = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Enter the [green]Shift-Description[/] (max 250 characters):[/]")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input) ||
                        input.Length > 250)
                    {
                        return ValidationResult.Error("[red]Please enter a valid Shift-Description(up to 250 characters).[/]");
                    }
                    return ValidationResult.Success();
                }));



        AnsiConsole.Prompt(
         new TextPrompt<string>("[yellow]Enter the [green]Start Time[/] (format: yyyy-MM-dd HH:mm):[/]")
             .Validate(input =>
             {
                 if (!DateTime.TryParse(input, out var startDate))
                     return ValidationResult.Error("[red]Please enter a valid date and time in the format 'yyyy-MM-dd HH:mm'.[/]");

                 shiftStart = startDate;
                 return ValidationResult.Success();
             }));


        AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Enter the [green]End Time[/] (format: yyyy-MM-dd HH:mm):[/]")
                .Validate(input =>
                {
                    if (!DateTime.TryParse(input, out var endDate))
                        return ValidationResult.Error("[red]Please enter a valid date and time in the format 'yyyy-MM-dd HH:mm'.[/]");

                    if (endDate <= shiftStart)
                        return ValidationResult.Error("[red]End Time must be after the Start Time![/]");

                    shiftEnd = endDate;
                    return ValidationResult.Success();
                }));


        var shiftEntry = new Shift(name,description, shiftStart, shiftEnd);
 
        if (id.HasValue)
        {
            shiftEntry.Id = id.Value;
        }

        _log.LogInformation("Validated user input: {shiftEntry}", shiftEntry);

        return shiftEntry;
    }
}
