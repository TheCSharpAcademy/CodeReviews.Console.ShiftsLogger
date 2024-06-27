using Spectre.Console;

namespace ShiftsLoggerUi.App;

public class MainMenu
{
    public const string ViewShifts = "View Shifts";
    public const string ViewEmployees = "View Employees";
    public const string Exit = "[red]Exit[/]";

    public static string Prompt()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nM E N U")
                .AddChoices([Exit, ViewShifts, ViewEmployees])
        );

    }
}