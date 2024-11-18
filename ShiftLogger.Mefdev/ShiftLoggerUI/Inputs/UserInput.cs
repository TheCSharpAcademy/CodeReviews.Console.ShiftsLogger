using System.Globalization;
using Spectre.Console;

namespace ShiftLogger.Mefdev.ShiftLoggerUI.Inputs;

public class UserInput
{
    public UserInput()
    {

    }

    public string GetId()
    {
        return AnsiConsole
        .Prompt(new TextPrompt<string>("Enter a id: ")
        .Validate(id =>
        {
            if (string.IsNullOrWhiteSpace(id))
                return ValidationResult.Error("[red]ID cannot be empty![/]");

            if (id.All(char.IsLetter))
                return ValidationResult.Error("[red]ID can only contain numbers![/]");

            return ValidationResult.Success();
        }));
    }
    public  string GetName(string oldName="")
    {
        return AnsiConsole
        .Prompt(new TextPrompt<string>($"Enter a name ({oldName}): ")
        .Validate(name =>
        {
            if (string.IsNullOrWhiteSpace(name))
                return ValidationResult.Error("[red]Name cannot be empty![/]");

            if (name.Any(char.IsDigit))
                return ValidationResult.Error("[red]Name cannot contain numbers![/]");

            if (name.Length < 2)
                return ValidationResult.Error("[red]Name must be at least 2 characters long![/]");

            return ValidationResult.Success();
        }));
    }

    public DateTime GetDate(string oldDate="")
    {
        while (true)
        {
            string dateInput = AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter a start or end date and time in the format [yellow] DD/MM/YYYY HH:mm:ss[/] ({oldDate}): ")
                .Validate(input =>
                {
                    return DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Invalid format! Please try again.[/]");
                }));

            if (DateTime.TryParseExact(dateInput, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }
            AnsiConsole.MarkupLine("[red]Invalid date entered. Please try again.[/]");
        }
    }
}