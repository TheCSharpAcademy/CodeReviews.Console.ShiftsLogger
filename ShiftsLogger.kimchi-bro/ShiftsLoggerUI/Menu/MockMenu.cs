using ShiftsLoggerUI.MockArea;
using Spectre.Console;

internal class MockMenu
{
    private static readonly Dictionary<string, Action> _menuActions = new()
    {
        { DisplayInfoHelpers.Back, Console.Clear },
        { "[green]Generate random shifts[/]", MockShift.Generate },
        { "[green]Generate random employees[/]", MockEmployee.Generate },
        { "[yellow]Delete all shifts[/]", MockShift.DeleteAllShifts },
        { "[red]Delete all employees[/]", MockEmployee.DeleteAllEmployees }
    };

    internal static void ShowMockMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose an action: ")
            .PageSize(10)
            .AddChoices(_menuActions.Keys));

        _menuActions[choice]();
    }
}
