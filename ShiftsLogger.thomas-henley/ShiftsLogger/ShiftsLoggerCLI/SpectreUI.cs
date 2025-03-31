using System.Globalization;
using ShiftsLoggerModels;
using Spectre.Console;

namespace ShiftsLoggerCLI;

public static class SpectreUi
{
    public static void WelcomeMessage()
    {
        AnsiConsole.MarkupLine("[green]Welcome to the Shift Logger.[/]");
    }

    public static void LoggedInMessage(int id)
    {
        AnsiConsole.MarkupLine($"\n[green]LOGGED IN: Employee #{id}.[/]");
    }

    public static void GoodbyeMessage()
    {
        AnsiConsole.MarkupLine("[green]\nGoodbye![/]");
    }

    public static int EmployeeIdPrompt()
    {
        AnsiConsole.MarkupLine("\nPlease enter your [green]Employee ID#[/] or create a new one.");

        var prompt = new TextPrompt<int>("Employee ID: ")
            .ValidationErrorMessage("[red]Employee ID must be an integer.[/]")
            .Validate(Validators.EmpIdValidator);
        
        var empId = AnsiConsole.Prompt(prompt);
        return empId;
    }

    public static string EmpMainMenu()
    {
        var prompt = new SelectionPrompt<string>()
            .Title("\nWhat would you like to do?")
            .AddChoices("Add Shift", "Review Shifts", "Exit");
        
        return AnsiConsole.Prompt(prompt);
    }

    public static DateTime GetStartTime()
    {
        var prompt = new TextPrompt<string>("Please enter the start time (MM/dd/yyyy HH:mm):")
            .Validate(Validators.DateTimeFormatValidator);
        
        return DateTime.ParseExact(AnsiConsole.Prompt(prompt), "MM/dd/yyyy HH:mm", new CultureInfo("en-US"));
    }

    public static DateTime GetEndTime(DateTime start)
    {
        var prompt = new TextPrompt<string>("Please enter the end time (MM/dd/yyyy HH:mm):")
            .Validate(s => Validators.EndTimeValidator(s, start));
        
        return DateTime.ParseExact(AnsiConsole.Prompt(prompt), "MM/dd/yyyy HH:mm", new CultureInfo("en-US"));
    }

    public static Shift DisplayShiftLog(List<Shift> shifts)
    {
        var prompt = new SelectionPrompt<Shift>()
            .Title("Shifts:")
            .AddChoices(shifts)
            .UseConverter(shift => shift.TimeDisplay);
        
        return AnsiConsole.Prompt(prompt);
    }

    public static string ShiftMenu(Shift shift)
    {
        AnsiConsole.MarkupLine($"Shift: [green]{shift.TimeDisplay}[/]");
        AnsiConsole.MarkupLine($"Duration: [green]{shift.Duration}[/]");
        AnsiConsole.MarkupLine("");

        var prompt = new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .AddChoices("Edit Start Time", "Edit End Time", "Delete Shift", "Go Back");
        
        return AnsiConsole.Prompt(prompt);
    }

    public static bool Confirm(string prompt)
    {
        return AnsiConsole.Confirm(prompt);
    }

    public static string Error(string message) => $"[red]Error: {message}[/]";

    public static string Success(string message) => $"[green]{message}[/]";
    
    public static void DisplayNotification(string message) => AnsiConsole.MarkupLine("* " + message);
}