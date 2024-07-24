using WorkerShiftsUI.Models;

namespace WorkerShiftsUI.Services;
public interface IWorkerService
{
    public Task ViewWorkers();
    public Task AddWorker();
    public Task UpdateWorker();
    public Task DeleteWorker();
}