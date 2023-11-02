using ShiftsLogger.Lonchanick.ContextDataBase;
using ShiftsLogger.Lonchanick.Models;

namespace ShiftsLogger.Lonchanick.Services;

public class WorkerService:IWorkerService
{
    ContextDB contexDB;

    public WorkerService(ContextDB contexDB)
    {
        this.contexDB = contexDB;
    }

    public IEnumerable<Worker> GetWorkers()
    {
        IEnumerable<Worker> x = contexDB.worker;
        return x;
    }

    public async Task SaveWorker(Worker worker)
    {
        contexDB.worker.Add(worker);
        await contexDB.SaveChangesAsync();
    }

    public async Task UpdateWorker(Guid id, Worker worker)
    {
        Worker? result = contexDB.worker.Find(id);
        if(result is not null)
        {
            result.Name = worker.Name;
            //contexDB.worker.Update(result);
            await contexDB.SaveChangesAsync();
        }
    }

    public async Task DeleteWorker(Guid id)
    {
        Worker? result = contexDB.worker.Find(id);
        if (result is not null)
        {
            contexDB.Remove(result);
            //contexDB.worker.Update(result);
            await contexDB.SaveChangesAsync();
        }
    }

}

public interface IWorkerService
{
    public IEnumerable<Worker> GetWorkers();
    public Task SaveWorker(Worker worker);
    public Task DeleteWorker(Guid id);
    public Task UpdateWorker(Guid id, Worker worker);

}
