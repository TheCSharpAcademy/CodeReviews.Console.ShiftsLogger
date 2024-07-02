using ShiftsLogger.Lonchanick.ContextDataBase;
using ShiftsLogger.Lonchanick.Models;

namespace ShiftsLogger.Lonchanick.Services;

public class WorkerService : IWorkerService
{
    ContextDB contexDB;

    public WorkerService(ContextDB contexDB)
    {
        this.contexDB = contexDB;
    }

    public async Task<IEnumerable<Worker>> GetWorkers()
    {
        IEnumerable<Worker> response = contexDB.worker;
        return response;
    }

    public async Task<Worker?> GetWorker(int id)
    {
        Worker? response = contexDB.worker.Find(id);
        return response;
    }

    public async Task SaveWorker(Worker worker)
    {
        try
        {
            contexDB.worker.Add(worker);
            await contexDB.SaveChangesAsync();
        }
        catch (Exception e)
        {
            WriteLine($"Something went wrong:\n{e.Message}");
        }

    }

    public async Task UpdateWorker(int id, Worker worker)
    {
        Worker? result = contexDB.worker.Find(id);
        if (result is not null)
        {
            result.Name = worker.Name;
            await contexDB.SaveChangesAsync();
        }
    }

    public async Task DeleteWorker(int id)
    {
        Worker? result = contexDB.worker.Find(id);
        if (result is not null)
        {
            contexDB.Remove(result);
            await contexDB.SaveChangesAsync();
        }
    }

}

public interface IWorkerService
{
    public Task<IEnumerable<Worker>> GetWorkers();
    public Task SaveWorker(Worker worker);
    public Task DeleteWorker(int id);
    public Task UpdateWorker(int id, Worker worker);
    public Task<Worker?> GetWorker(int id);

}
