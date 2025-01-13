using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Services;

public class WorkerService : IWorkerService
{
    private readonly ShiftsDbContext _context;

    public WorkerService(ShiftsDbContext context)
    {
        _context = context;
    }

    public Worker Add(Worker worker) {
        var savedWorker = _context.Add(worker);
        _context.SaveChanges();
        return savedWorker.Entity;
    }

    public bool Update(Worker worker)
    {
        Worker? savedWorker = GetById(worker.Id);

        if (savedWorker == null)
        {
            return false;
        }

        savedWorker.Name = worker.Name;
        savedWorker.Department = worker.Department;

        _context.SaveChanges();
        return true;
    }

    public bool Delete(int workerId)
    {
        Worker? savedWorker = GetById(workerId);

        if (savedWorker == null)
        {
            return false;
        }

        _context.Workers.Remove(savedWorker);
        _context.SaveChanges();

        return true;
    }

    public Worker? GetById(int id)
    {
        return _context.Workers.FirstOrDefault(w => w.Id == id);
    }

    public List<Worker> GetAll()
    {
        return _context.Workers.ToList();
    }
}
