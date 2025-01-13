using UI.Models;

namespace UI.Interfaces;
internal interface IWorkerService
{
    public Task<List<Worker>> GetAll();
    public Task<Worker> GetWorkerById(int id);
    public Task<bool> CreateWorker(Worker worker);
    public Task<bool> UpdateWorker(Worker worker);
    public Task<bool> DeleteWorker(int workerId);
    public Task<Dictionary<int, string>> GetAllAsDictionary();
}
