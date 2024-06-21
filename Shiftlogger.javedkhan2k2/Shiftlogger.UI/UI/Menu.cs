using Shiftlogger.UI.DTOs;
using Spectre.Console;

namespace Shiftlogger.UI;

public class Menu
{
    public static string CancelOperation = $"[maroon]Go Back[/]";

    public string[] MainMenu = ["Workers", "Shifts", "Exit"];
    public string[] WorkerMenu = ["View All Workers", "Add Worker", "Update Worker", "Delete Worker", $"[maroon]Go Back[/]"];
    public string[] ShiftMenu = ["View All Shifts", "Add Shift", "Update Shift", "Delete Shift", $"[maroon]Go Back[/]"];
    
    public string Title = "[yellow]Please Select An [blue]Action[/] From The Options Below[/]";
    internal string GetMainMenu()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(Title)
                    .PageSize(10)
                    .AddChoices(MainMenu)
        );
    }

    internal string GetWorkerMenu()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(Title)
                    .PageSize(10)
                    .AddChoices(WorkerMenu)
        );
    }

    internal string GetShiftMenu()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(Title)
                    .PageSize(10)
                    .AddChoices(ShiftMenu)
        );
    }

}