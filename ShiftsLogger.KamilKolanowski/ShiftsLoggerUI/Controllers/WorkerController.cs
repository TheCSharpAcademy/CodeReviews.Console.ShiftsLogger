using ShiftsLogger.KamilKolanowski.Enums;
using ShiftsLoggerUI.Services;
using Spectre.Console;

namespace ShiftsLoggerUI.Controllers;

internal class WorkerController
{
    private readonly WorkerService _workerService = new();

    internal async Task Operate()
    {
        var selectOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose an option:")
                .AddChoices(ShiftsLoggerMenu.WorkerMenuType.Values)
        );

        var selectedOption = ShiftsLoggerMenu
            .WorkerMenuType.FirstOrDefault(o => o.Value == selectOption)
            .Key;

        switch (selectedOption)
        {
            case ShiftsLoggerMenu.WorkerMenu.AddWorker:
                await AddWorker();
                break;
            case ShiftsLoggerMenu.WorkerMenu.EditWorker:
                await UpdateWorker();
                break;
            case ShiftsLoggerMenu.WorkerMenu.DeleteWorker:
                await DeleteWorker();
                break;
            case ShiftsLoggerMenu.WorkerMenu.ViewWorkers:
                await ViewWorkers();
                break;
            case ShiftsLoggerMenu.WorkerMenu.GoBack:
                return;
        }
    }

    private async Task AddWorker()
    {
        await _workerService.CreateWorker();
        
        AnsiConsole.MarkupLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task UpdateWorker()
    {
        await _workerService.EditWorker();

        Console.ReadKey();
    }

    private async Task DeleteWorker()
    {
        await _workerService.DeleteWorker();

        Console.ReadKey();
    }

    private async Task ViewWorkers()
    {
        await _workerService.CreateWorkersTable();

        AnsiConsole.MarkupLine("Press any key to continue...");
        Console.ReadKey();
    }
}
