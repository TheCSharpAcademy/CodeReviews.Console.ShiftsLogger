
using Spectre.Console;
using WorkerShiftsUI.Models;
using WorkerShiftsUI.Services;
using WorkerShiftsUI.UserInteractions;

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
        var isWorkerViewRunning = true;

        while (isWorkerViewRunning)
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
                case "Add Worker":
                    Worker worker = UserInteraction.GetWorkerDetails();
                    await _workerService!.AddWorker(worker);
                    break;
                // case "Update Worker":
                //     await _workerService!.UpdateWorker();
                //     break;
                case "Delete Worker":
                    await _workerService!.DeleteWorker();
                    break;
                case "Go Back":
                    isWorkerViewRunning = false;
                    return;
            }
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