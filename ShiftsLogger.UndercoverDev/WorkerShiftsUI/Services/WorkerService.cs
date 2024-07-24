
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

        public async Task UpdateWorker(int id, Worker worker)
        {
            await _apiService.UpdateWorkerAsync(id, worker);
        }

        public async Task DeleteWorker(int id)
        {
            await _apiService.DeleteWorkerAsync(id);
        }
    }
}