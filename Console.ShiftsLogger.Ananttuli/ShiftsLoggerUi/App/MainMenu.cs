using Spectre.Console;

namespace ShiftsLoggerUi.App;

public class MainMenu
{
    public const string ManageShifts = "Search & modify all shifts";
    public const string ManageEmployees = "Manage Employees & Associated Shifts";
    public const string Exit = "[red]Exit[/]";

    public static string Prompt()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nM E N U")
                .AddChoices([Exit, ManageShifts, ManageEmployees])
        );
    }
}