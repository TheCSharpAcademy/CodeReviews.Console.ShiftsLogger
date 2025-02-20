using Shifts_Logger.Data;
using Shifts_Logger.Models;

namespace Shifts_Logger.Services;

public class WorkerService : IWorkerService
{
    private readonly AppDbContext _context;

    public WorkerService(AppDbContext context)
    {
        _context = context;
    }

    public void AddWorker(string name)
    {
        _context.Workers.Add(new Worker { Name = name });
        _context.SaveChanges();
    }

    public void DeleteWorker(int id)
    {
        _context.Workers.Remove(new Worker { Id = id });
        _context.SaveChanges();
    }

    public Worker GetWorker(int id)
    {
        return _context.Workers.FirstOrDefault(w => w.Id == id);
    }

    public IEnumerable<Worker> GetWorkers()
    {
        return _context.Workers.ToList();
    }

    public void UpdateWorker(int id, string name)
    {
        var worker = _context.Workers.FirstOrDefault(w => w.Id == id);
        if (worker != null)
        {
            worker.Name = name;
            _context.SaveChanges();
        }
    }
}
