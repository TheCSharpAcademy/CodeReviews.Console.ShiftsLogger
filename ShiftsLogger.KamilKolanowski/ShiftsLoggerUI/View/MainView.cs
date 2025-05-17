using ShiftsLogger.KamilKolanowski.Enums;
using ShiftsLoggerUI.Controllers;
using Spectre.Console;

namespace ShiftsLoggerUI.View;

internal class MainView
{
    private readonly ShiftController _shiftController = new();
    private readonly WorkerController _workerController = new();

    internal async Task Start()
    {
        while (true)
        {
            Console.Clear();

            var selectOption = AnsiConsole.Prompt(
                new SelectionPrompt<ShiftsLoggerMenu.MainMenu>()
                    .Title("Manage Areas")
                    .AddChoices(Enum.GetValues<ShiftsLoggerMenu.MainMenu>())
            );

            switch (selectOption)
            {
                case ShiftsLoggerMenu.MainMenu.Worker:
                    await _workerController.Operate();
                    break;
                case ShiftsLoggerMenu.MainMenu.Shift:
                    await _shiftController.Operate();
                    break;
                case ShiftsLoggerMenu.MainMenu.Exit:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
