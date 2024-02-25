using Buutyful.ShiftsLogger.Domain;
using Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<bool> DeleteAsync(Guid id)
    {
        var worker = _context.Workers.FirstOrDefault(w => w.Id == id);
        if (worker != null)
        {
            _context.Workers.Remove(worker);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }
        return false;
    }

    public async Task<(bool Created, WorkerResponse Worker)> UpsertWorkerAsync(UpsertWorkerRequest upsertWorker)
    {
        var worker = _context.Workers.AsNoTracking().FirstOrDefault(w => w.Id == upsertWorker.Id);
        var updatedWorker = Worker.CreateWithId(upsertWorker.Id, upsertWorker.Name, upsertWorker.Role);

        if (worker is not null)
        {
            var entry = _context.Entry(updatedWorker);
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            entry.State = EntityState.Detached;
            return (false, updatedWorker);
        }
        else
        {
            _context.Workers.Add(updatedWorker);
            await _context.SaveChangesAsync();
            return (true, updatedWorker);
        }
    }

}
