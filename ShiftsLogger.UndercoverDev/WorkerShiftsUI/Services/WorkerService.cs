
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
            UserInteraction.ShowWorkers(workers);
        }

        public async Task ViewWorker(int id)
        {
            var worker = await _apiService.GetWorkerAsync(id);
        }

        public async Task AddWorker(Worker worker)
        {
            var newWorker = await _apiService.CreateWorkerAsync(worker);

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