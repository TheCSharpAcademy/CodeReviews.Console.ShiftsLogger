using WorkerShiftsUI.Models;

namespace WorkerShiftsUI.Services;
public interface IWorkerService
{
    public Task ViewWorkers();
    public Task AddWorker(Worker worker);
    public Task UpdateWorker();
    public Task DeleteWorker();
}