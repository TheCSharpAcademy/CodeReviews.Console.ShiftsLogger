using ShiftsLogger.Console.Services;
using ShiftsLogger.Console.UI7;
using Spectre.Console;

namespace ShiftsLogger.Console.UI;

public class StartShiftPrompt
{
    ShiftConsoleService shiftConsoleService = new();
    MainMenuPrompt mainMenuPrompt = new();

    public void StartShift()
    {
        if (shiftConsoleService.IsShiftInProgress())
        {
            AnsiConsole.Markup("\n\n\nShift already in progress.\nPlease clock out before clocking in.\n\n\n");
            mainMenuPrompt.MainMenuSelection();
        }
        else
        {
            shiftConsoleService.StartShift();
            AnsiConsole.Markup("Shift started successfully.\n\n\n");
            mainMenuPrompt.MainMenuSelection();
        }
    }
}
