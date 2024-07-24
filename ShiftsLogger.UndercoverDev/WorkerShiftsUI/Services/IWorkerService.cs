using WorkerShiftsUI.Models;

namespace WorkerShiftsUI.Services;
public interface IWorkerService
{
    public Task ViewWorkers();
    public Task AddWorker(Worker worker);
    public Task UpdateWorker(int id, Worker worker);
    public Task DeleteWorker(int id);
}