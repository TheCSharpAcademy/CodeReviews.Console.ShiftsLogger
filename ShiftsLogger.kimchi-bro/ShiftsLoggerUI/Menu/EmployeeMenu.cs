using Spectre.Console;

internal class EmployeeMenu
{
    private static readonly Dictionary<string, Action> _menuActions = new()
    {
        { DisplayInfoHelpers.Back, Console.Clear },
        { "Show all employees", EmployeeRead.ShowAllEmployees },
        { "Show all shifts for employee", EmployeeShifts.ShowAllShifts },
        { "Add new employee", EmployeeCreate.Create },
        { "Edit employee", EmployeeUpdate.Update },
        { "Delete employee", EmployeeDelete.Delete }
    };

    internal static void ShowEmployeeMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose an action: ")
            .PageSize(10)
            .AddChoices(_menuActions.Keys));

        _menuActions[choice]();
    }
}
