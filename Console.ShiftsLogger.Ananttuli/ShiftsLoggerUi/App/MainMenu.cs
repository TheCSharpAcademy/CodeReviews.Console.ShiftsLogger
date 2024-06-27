using Spectre.Console;

namespace ShiftsLoggerUi.App;

public class MainMenu
{
    public const string ManageEmployees = "Manage Employees & Associated Shifts";
    public const string ManageShifts = "View and manage all shifts";
    public const string AddEmployee = "Add New Employee";

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