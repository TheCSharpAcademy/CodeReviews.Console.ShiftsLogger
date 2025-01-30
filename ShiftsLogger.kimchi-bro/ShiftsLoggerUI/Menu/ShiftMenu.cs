using Spectre.Console;

internal class ShiftMenu
{
    private static readonly Dictionary<string, Action> _menuActions = new()
    {
        { DisplayInfoHelpers.Back, Console.Clear },
        { "Show all shifts", ShiftRead.ShowAllShifts },
        { "Add new shift", ShiftCreate.Create },
        { "Edit shift", ShiftUpdate.Update },
        { "Delete shift", ShiftDelete.Delete }
    };

    internal static void ShowShiftMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose an action: ")
            .PageSize(10)
            .AddChoices(_menuActions.Keys));

        _menuActions[choice]();
    }
}
