using Shifts_Logger.Models;

namespace Shifts_Logger.Services;

public interface IWorkerService
{
    IEnumerable<Worker> GetWorkers();
    Worker GetWorker(int id);
    void AddWorker(string name);
    void UpdateWorker(int id, string name);
    void DeleteWorker(int id);
}
