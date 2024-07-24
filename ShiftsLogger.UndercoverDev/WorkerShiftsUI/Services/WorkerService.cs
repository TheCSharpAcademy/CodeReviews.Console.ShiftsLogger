
using Spectre.Console;
using WorkerShiftsUI.Models;
using WorkerShiftsUI.UserInteractions;
using WorkerShiftsUI.Views;

namespace WorkerShiftsUI.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly ApiService _apiService;

        public WorkerService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task ViewWorkers()
        {
            var workers = await _apiService.GetWorkersAsync();

            if (workers == null)
            {
                AnsiConsole.MarkupLine("[bold][red]No workers found.[/]");
                return;
            }
            else
            {
                UserInteraction.ShowWorkers(workers);
            }
        }

        public async Task AddWorker(Worker worker)
        {
            var createdWorker = await _apiService.CreateWorkerAsync(worker);

            if (createdWorker == null)
            {
                AnsiConsole.MarkupLine("[bold][red]Failed to create worker.[/]");
                return;
            }
            else
            {
                AnsiConsole.MarkupLine($"[bold][green]{createdWorker.Name} created with ID: {createdWorker.WorkerId}[/][/]");
            }
        }

        public async Task UpdateWorker()
        {
            var workers = await _apiService.GetWorkersAsync();
            if (workers.Count == 0 || workers == null)
            {
                AnsiConsole.MarkupLine("[bold][red]No workers found.[/][/]");
                return;
            }

            var selectedWorker = UserInteraction.GetWorkerOptionInput(workers);
            if (selectedWorker == null || selectedWorker.Name == "Back")
            {
                return;
            }

            selectedWorker.Name = AnsiConsole.Confirm("Update name?") ?
                AnsiConsole.Ask<string>("Enter new worker name: ") : selectedWorker.Name;
            while (string.IsNullOrWhiteSpace(selectedWorker.Name))
            {
                AnsiConsole.MarkupLine("[bold][red]Please enter a valid name[/][/]");
                selectedWorker.Name = AnsiConsole.Ask<string>("Enter new worker name: ");
            }

            await _apiService.UpdateWorkerAsync(selectedWorker.WorkerId, selectedWorker);

            AnsiConsole.MarkupLine($"[bold][green]{selectedWorker.Name} updated.[/][/]");
        }

        public async Task DeleteWorker()
        {
            var workers = await _apiService.GetWorkersAsync();

            if (workers.Count == 0 || workers == null)
            {
                AnsiConsole.MarkupLine("[bold][red]No workers found.[/][/]");
                return;
            }
            
            var selectedWorker = UserInteraction.GetWorkerOptionInput(workers);

            if (selectedWorker == null || selectedWorker.Name == "Back")
            {
                return;
            }

            await _apiService.DeleteWorkerAsync(selectedWorker.WorkerId);

            AnsiConsole.MarkupLine($"[bold][green]{selectedWorker.Name} deleted.[/][/]");
        }
    }
}