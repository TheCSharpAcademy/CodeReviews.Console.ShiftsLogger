
public interface IWorkerService
{
    public List<Worker> GetAllWorkers();
    public Worker? GetWorkerById(int workerId);
    public Worker CreateWorker(Worker worker);
    public Worker? UpdateWorker(Worker worker);
    public string? DeleteWorker(int workerId);
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

    public string? DeleteWorker(int workerId)
    {
        // check for valid worker
        Worker? savedWorker = _dbContext.Workers
            .Where(worker => worker.EmployeeId == workerId)
            .FirstOrDefault();
        if (savedWorker == null)
            return null;

        // remove any shifts assigned to worker
        List<Shift>? workersShifts = _dbContext.Shifts.Where(shift => shift.WorkerId == workerId).ToList();
        if (workersShifts != null)
        {
            foreach (Shift shift in workersShifts)
                _dbContext.Remove(shift);
        }
        
        // takes out the worker 
        _dbContext.Workers.Remove(savedWorker);
        _dbContext.SaveChanges();
        return $"Successfully deleted Worker with db id: {savedWorker.Id}, WorkerId: {workerId}";
    }

    public List<Worker> GetAllWorkers()
    {
        return _dbContext.Workers.ToList();
    }

    public Worker? GetWorkerById(int workerId)
    {
        Worker? savedWorker = _dbContext.Workers
            .Where(worker => worker.EmployeeId == workerId)
            .FirstOrDefault();
        return savedWorker;
    }

    public Worker? UpdateWorker(Worker worker)
    {
        // check for valid worker
        Worker? savedWorker = _dbContext.Workers
            .Where(w => w.EmployeeId == worker.EmployeeId)
            .FirstOrDefault();
        if (savedWorker == null)
            return null;

        // update worker
        worker.Id = savedWorker.Id;
        _dbContext.Entry(savedWorker).CurrentValues.SetValues(worker);
        _dbContext.SaveChanges();

        return savedWorker;
    }
}