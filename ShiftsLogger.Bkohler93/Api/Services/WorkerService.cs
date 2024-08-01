using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

public class WorkerService {
    private readonly AppDbContext db;
    public WorkerService(AppDbContext dbContext)
    {
        db = dbContext; 
    }

    public async Task<IEnumerable<GetWorkerDto>> GetWorkers()
    {
        var workers = await db.Workers.ToListAsync();

        return workers.Select(w => new GetWorkerDto{
            Id = w.Id,
            FirstName = w.FirstName,
            LastName = w.LastName,
            Position = w.Position,
        }).ToList();
    }

    public async Task<GetWorkerDto?> GetWorkerById(int id)
    {
        var worker = await db.Workers.FindAsync(id);

        if (worker == null) {
            return null; 
        }

        return new GetWorkerDto{
            Id = worker.Id,
            FirstName = worker.FirstName,
            LastName = worker.LastName,
            Position = worker.Position
        };    
    }

    public async Task<Worker?> FindWorker(int id)
    {
        return await db.Workers.FindAsync(id);
    }

    public async Task<GetWorkerDto> CreateWorker(PostWorkerDto dto)
    {
        var worker = new Worker {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Position = dto.Position,
            WorkerShifts = [],
        };

        db.Workers.Add(worker);
        await db.SaveChangesAsync();

        return new GetWorkerDto{
            Id = worker.Id,
            FirstName = worker.FirstName,
            LastName = worker.LastName,
            Position = worker.Position,
        };
    }

    public async Task UpdateWorker(PutWorkerDto dto, Worker worker)
    {
        worker.FirstName = dto.FirstName;
        worker.LastName = dto.LastName;
        worker.Position = dto.Position;

        db.Entry(worker).State = EntityState.Modified;
        await db.SaveChangesAsync();
    }

    public async Task DeleteWorker(Worker worker)
    {
        db.Workers.Remove(worker);
        await db.SaveChangesAsync();
    }    
}