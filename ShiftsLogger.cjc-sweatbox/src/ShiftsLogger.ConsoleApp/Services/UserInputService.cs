using System.Globalization;
using ShiftsLogger.ConsoleApp.Enums;
using ShiftsLogger.ConsoleApp.Models;
using ShiftsLogger.Extensions;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.Services;

/// <summary>
/// Helper service for getting valid user inputs of different types.
/// </summary>
internal static partial class UserInputService
{
    #region Constants

    // TODO: Validation Messages.

    #endregion
    #region Methods - Internal

    internal static DateTime? GetShiftStartDateTime(string prompt, string format)
    {
        var dateTimeString = AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            .ValidationErrorMessage($"[red]Invalid start time![/] Enter in the required format: [blue]{format}[/]")
            .Validate(input =>
            {
                return (input == "0" || DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error();
            }));

        return dateTimeString == "0" ? null : DateTime.ParseExact(dateTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
    }

    internal static DateTime? GetShiftEndDateTime(string prompt, string format, DateTime startDateTime)
    {
        var dateTimeString = AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            .ValidationErrorMessage($"[red]Invalid end time![/] Enter in the required format: [blue]{format}[/]. End time must be after the start time.")
            .Validate(input =>
            {
                return (input == "0" || IsValidShiftEndDateTime(input, format, startDateTime))
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error();
            }));

        return dateTimeString == "0" ? null : DateTime.ParseExact(dateTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
    }

    internal static string GetString(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            );
    }

    internal static string GetString(string prompt, string defaultValue)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            .DefaultValue(defaultValue)
            );
    }

    //internal static Category GetCategory(string prompt, IReadOnlyList<Category> categories)
    //{
    //    return AnsiConsole.Prompt(
    //            new SelectionPrompt<Category>()
    //            .Title(prompt)
    //            .AddChoices(categories)
    //            .UseConverter(c => c.Name)
    //            );
    //}

    internal static SelectionChoice GetPageChoice(string prompt, IEnumerable<SelectionChoice> choices)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<SelectionChoice>()
                .Title(prompt)
                .AddChoices(choices)
                .UseConverter(c => c.Name)
                );
    }

    internal static MenuChoice GetMenuChoice(string prompt, IEnumerable<MenuChoice> choices)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<MenuChoice>()
                .Title(prompt)
                .AddChoices(choices)
                .UseConverter(c => c.GetDescription())
                );
    }

    #endregion
    #region Methods - Private

    private static bool IsValidShiftEndDateTime(string input, string format, DateTime startDateTime)
    {
        bool isCorrectDateTimeFormat = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDateTime);
        if (!isCorrectDateTimeFormat)
        {
            return false;
        }
        else if (endDateTime <= startDateTime)
        {
            return false;
        }

        return true;
    }

    #endregion
}

