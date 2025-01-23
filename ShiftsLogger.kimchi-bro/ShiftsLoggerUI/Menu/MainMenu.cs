using ShiftsLoggerUI.MockArea;
using ShiftsLoggerUI.ShiftCrud;
using Spectre.Console;

namespace ShiftsLoggerUI.Menu;

internal class MainMenu
{
    private static readonly Dictionary<string, Action> _menuActions = new()
    {
        { "Show all shifts", ShiftRead.ShowAllShifts },
        { "Add new shift", ShiftCreate.Create },
        { "Update shift", ShiftUpdate.Update },
        { "Delete shift", ShiftDelete.Delete },
        { "[yellow]Create random shifts[/]", ShiftMock.Generate },
        { "[red]Delete all shifts[/]", ShiftMock.DeleteAll },
        { "Exit the app", () =>
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[yellow]Goodbye![/]");
                Environment.Exit(0);
            }
        }
    };

    internal static void ShowMainMenu()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose an action: ")
                .PageSize(10)
                .AddChoices(_menuActions.Keys));

            _menuActions[choice]();
        }
    }
}
