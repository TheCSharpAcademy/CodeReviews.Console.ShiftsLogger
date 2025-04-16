
public interface IWorkerService
{
    public List<Worker> GetAllWorkers();
    public Worker? GetWorkerById(int id);
    public Worker CreateWorker(Worker worker);
    public Worker? UpdateWorker(Worker worker);
    public string? DeleteWorker(int id);
}

public class WorkerService : IWorkerService
{
    private readonly ShiftsLoggerDbContext _dbContext;

    public WorkerService(ShiftsLoggerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Worker CreateWorker(Worker worker)
    {
        var savedWorker = _dbContext.Workers.Add(worker);
        _dbContext.SaveChanges();
        return savedWorker.Entity;
    }

    public string? DeleteWorker(int id)
    {
        Worker? savedWorker = _dbContext.Workers.Find(id);

        if (savedWorker == null)
            return null;
        
        _dbContext.Workers.Remove(savedWorker);
        
        _dbContext.SaveChanges();

        return $"Successfully deleted Worker with id: {id}";
    }

    public List<Worker> GetAllWorkers()
    {
        return _dbContext.Workers.ToList();
    }

    public Worker? GetWorkerById(int id)
    {
        Worker? savedWorker = _dbContext.Workers.Find(id);
        return savedWorker;
    }

    public Worker? UpdateWorker(Worker worker)
    {
        Worker? savedWorker = _dbContext.Workers.Find(worker.Id);

        if (savedWorker == null)
            return null;

        _dbContext.Entry(savedWorker).CurrentValues.SetValues(worker);
        _dbContext.SaveChanges();

        return savedWorker;
    }
}