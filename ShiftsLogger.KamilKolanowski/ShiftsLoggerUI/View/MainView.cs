using ShiftsLogger.KamilKolanowski.Controllers;
using ShiftsLogger.KamilKolanowski.Enums;
using Spectre.Console;

namespace ShiftsLogger.KamilKolanowski.View;

internal class MainView
{
    private readonly WorkerController _workerController = new WorkerController();
    private readonly ShiftController _shiftController = new ShiftController();
    
    internal void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            
            var selectOption = AnsiConsole.Prompt(
                new SelectionPrompt<ShiftsLoggerMenu.MainMenu>()
                    .Title("Choose area")
                    .AddChoices(Enum.GetValues<ShiftsLoggerMenu.MainMenu>()));
            
            switch (selectOption)
            {
                case ShiftsLoggerMenu.MainMenu.Worker:
                    _workerController.Operate();
                    Console.ReadKey();
                    // Console.WriteLine("test");
                    break;
                case ShiftsLoggerMenu.MainMenu.Shift:
                    _shiftController.Operate();
                    break;
            }
        }    
    }
} 