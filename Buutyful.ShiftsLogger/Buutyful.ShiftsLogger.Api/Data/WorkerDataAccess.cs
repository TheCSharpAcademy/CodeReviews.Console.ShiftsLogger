using Buutyful.ShiftsLogger.Domain;
using Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;
using Microsoft.EntityFrameworkCore;

namespace Buutyful.ShiftsLogger.Api.Data;

public class WorkerDataAccess(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<WorkerResponse> AddAsync(CreateWorkerRequest workerRequest)
    {
        var worker = Worker.Create(workerRequest.Name, workerRequest.Role);
        await _context.Workers.AddAsync(worker);
        await _context.SaveChangesAsync();
        return new WorkerResponse(worker.Id, worker.Name, worker.Role);
    }
    public async Task<List<WorkerResponse>> GetAsync()
    {
        var workers = await _context.Workers.ToListAsync();
        return workers.Select(w => new WorkerResponse(w.Id, w.Name, w.Role)).ToList();
    }
    public async Task<WorkerResponse?> GetByIdAsync(Guid id)
    {
        var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == id);
        return worker is not null ?
            new WorkerResponse(worker.Id, worker.Name, worker.Role) :
            null;
    }
    public async Task Delete(Guid id)
    {
        var worker = _context.Workers.FirstOrDefault(w => w.Id == id);
        if (worker != null)
        {
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<WorkerResponse?> UpdateWorker(UpsertWorkerRequest upsertWorker)
    {
        var worker = _context.Workers.FirstOrDefault(w => w.Id == upsertWorker.Id);
        if (worker != null)
        {
            worker = Worker.CreateWithId(upsertWorker.Id, upsertWorker.Name, upsertWorker.Role);
            await _context.SaveChangesAsync();
            return new WorkerResponse(worker.Id, worker.Name, worker.Role);
        }
        return null;
    }
    public async Task<WorkerResponse> UpsertWorker(UpsertWorkerRequest upsertWorker)
    {
        var worker = _context.Workers.FirstOrDefault(w => w.Id == upsertWorker.Id);

        if (worker is null)
        {
            worker = Worker.CreateWithId(upsertWorker.Id, upsertWorker.Name, upsertWorker.Role);
            await _context.SaveChangesAsync();
            return new WorkerResponse(worker.Id, worker.Name, worker.Role);
        }
        else
        {
            var newWorker = Worker.Create(upsertWorker.Name, upsertWorker.Role);
            _context.Workers.Add(newWorker);
            await _context.SaveChangesAsync();
            return new WorkerResponse(newWorker.Id, newWorker.Name, newWorker.Role);
        }
    }

}
