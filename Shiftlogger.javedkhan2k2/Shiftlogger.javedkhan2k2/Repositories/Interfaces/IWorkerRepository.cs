using Shiftlogger.Entities;

namespace Shiftlogger.Repositories.Interfaces;

public interface IWorkerRepository
{
    List<Worker> GetAllWorkers();
    Worker GetWorkerById(int id);
    void AddWorker(Worker worker);
    void UpdateWorker(Worker worker);
    void DeleteWorker(Worker worker);
}