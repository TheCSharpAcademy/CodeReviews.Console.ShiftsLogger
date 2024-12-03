using ShiftsLogger.Console.UI;
using Spectre.Console;

namespace ShiftsLogger.Console.UI7;

internal class MainMenuPrompt
{
    public void MainMenuSelection()
    {
        DisplayShiftHistory displayShiftHistory = new();
        StartShiftPrompt startShiftPrompt = new();
        EndShiftPrompt endShiftPrompt = new();

        var mainMenuSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Please select from the options below: [/]")
                .PageSize(20)
                .AddChoices(new[] {
                    "Start Shift", "End Shift", "Shift History", "Exit Application"
                }));

        switch (mainMenuSelection)
        {
            case "Start Shift": startShiftPrompt.StartShift(); break;
            case "End Shift": endShiftPrompt.EndShift(); break;
            case "Shift History": displayShiftHistory.ShowShiftHistory(); break;
            case "Exit Application": Environment.Exit(0); break;
        }
    }
}