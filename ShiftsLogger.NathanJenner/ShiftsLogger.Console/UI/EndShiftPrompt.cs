using ShiftsLogger.Console.Services;
using ShiftsLogger.Console.UI7;
using Spectre.Console;

namespace ShiftsLogger.Console.UI;

public class EndShiftPrompt
{
    ShiftConsoleService shiftConsoleService = new();
    MainMenuPrompt mainMenuPrompt = new();

    public void EndShift()
    {
        if (!shiftConsoleService.IsShiftInProgress())
        {
            AnsiConsole.Markup("No Shift in progress.\nPlease clock in before clocking out\n\n\n");
            mainMenuPrompt.MainMenuSelection();
        }
        else
        {
            shiftConsoleService.EndShift();
            System.Console.WriteLine("You have successfully clocked out. Goodbye.\n\n\n\n\n\n");
            mainMenuPrompt.MainMenuSelection();
        }
    }
}
