
using Spectre.Console;
using WorkerShiftsUI.Models;
using WorkerShiftsUI.Services;

namespace WorkerShiftsUI.Views;
public class WorkersView : IWorkersView
{
    private readonly IWorkerService? _workerService;

    public WorkersView(IWorkerService workerService)
    {
        _workerService = workerService;
    }

    public async Task WorkersMenu()
    {
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Shift Logger Workers Menu")
            .AddChoices(
                "View Workers",
                "Add Worker",
                "Update Worker",
                "Delete Worker",
                "Go Back")
            .UseConverter(x => x.ToString())
        );

        switch (choice)
        {
            case "View Workers":
                await _workerService!.ViewWorkers();
                break;
            // case "Add Worker":
            //     await _workerService!.AddWorker(1);
            //     break;
            // case "Update Worker":
            //     await _workerService!.UpdateWorker();
            //     break;
            // case "Delete Worker":
            //     await _workerService!.DeleteWorker(1);
                break;
            case "Go Back":
                return;
        }
    }

    public void ShowWorker(Worker worker)
    {
        throw new NotImplementedException();
    }

    public void ShowWorkers(List<Worker> workers)
    {
        throw new NotImplementedException();
    }
}