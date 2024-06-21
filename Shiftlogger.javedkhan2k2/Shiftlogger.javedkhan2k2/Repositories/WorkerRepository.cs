using Microsoft.EntityFrameworkCore;
using Shiftlogger.Data;
using Shiftlogger.Entities;

namespace Shiftlogger.Repositories.Interfaces;

public class WorkerRepository : IWorkerRepository
{
    private readonly ShiftloggerDbContext _context;

    public WorkerRepository(ShiftloggerDbContext context)
    {
        _context = context;
    }

    public void AddWorker(Worker worker)
    {
        _context.Add(worker);
        _context.SaveChanges();
    }

    public void DeleteWorker(Worker worker)
    {
        _context.Remove(worker);
        _context.SaveChanges();
    }

    public List<Worker> GetAllWorkers() => _context.Workers.Include(s => s.Shifts).ToList();

    public Worker GetWorkerById(int id) => _context.Workers.AsNoTracking().Include(s => s.Shifts).FirstOrDefault(w => w.Id == id);

    public void UpdateWorker(Worker worker)
    {
        _context.Attach(worker).State = EntityState.Modified;
        _context.SaveChanges();
    }
}