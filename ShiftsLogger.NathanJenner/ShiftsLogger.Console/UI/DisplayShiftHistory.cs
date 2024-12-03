using ShiftsLogger.API.Models;
using ShiftsLogger.Console.Services;
using ShiftsLogger.Console.UI7;
using Spectre.Console;

namespace ShiftsLogger.Console.UI;

public class DisplayShiftHistory
{
    ShiftConsoleService shiftConsoleService = new();
    MainMenuPrompt mainMenuPrompt = new();

    public void ShowShiftHistory()
    {
        Task<List<Shift>> allShiftsTask = shiftConsoleService.GetShiftHistory();

        List<Shift> allShifts = allShiftsTask.Result;

        AnsiConsole.Clear();
        AnsiConsole.Markup("[bold yellow]Shift History[/]");
        var table = new Table();
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Shift Length");

        foreach (Shift shift in allShifts)
        {
            table.AddRow(shift.StartTime.ToString(), shift.EndTime.ToString(), shift.ShiftLength.ToString());
        }
        AnsiConsole.Render(table);
        mainMenuPrompt.MainMenuSelection();
    }
}
