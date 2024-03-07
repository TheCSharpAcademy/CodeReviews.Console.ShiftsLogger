﻿using Buutyful.ShiftsLogger.Domain;
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
        return worker;
    }
    public async Task<List<WorkerResponse>> GetAsync()
    {
        var workers = await _context.Workers.ToListAsync();
        return workers.Select(w => new WorkerResponse(w.Id, w.Name, w.Role)).ToList();
    }
    public async Task<WorkerResponse?> GetByIdAsync(Guid id)
    {
        var worker = await _context.Workers.FindAsync(id);
        return worker is not null ?
            new WorkerResponse(worker.Id, worker.Name, worker.Role) :
            null;
    }
    public async Task<Results<Ok, NotFound>> DeleteAsync(Guid id)
    {
        var rows = await _context.Workers.Where(s => s.Id == id)
                                        .ExecuteDeleteAsync();

        return rows > 0 ? TypedResults.Ok() :
                          TypedResults.NotFound();
    }
    public async Task<(bool Updated, WorkerResponse Worker)> UpdateWorkerAsync(UpsertWorkerRequest upsertWorker)
    {
        var updatedWorker = Worker.CreateWithId(upsertWorker.Id, upsertWorker.Name, upsertWorker.Role);
        var rows = await _context.Workers.Where(w => w.Id == upsertWorker.Id)
                                          .ExecuteUpdateAsync(set => set
                                          .SetProperty(worker => worker.Name, updatedWorker.Name)
                                          .SetProperty(worker => worker.Role, updatedWorker.Role));
        return (rows > 0, updatedWorker);
      
    }

    public async Task<(bool Created, WorkerResponse Worker)> UpsertWorkerAsync(UpsertWorkerRequest upsertWorker)
    {
        var found = await _context.Workers.AnyAsync(w => w.Id == upsertWorker.Id);
        var updatedWorker = Worker.CreateWithId(upsertWorker.Id, upsertWorker.Name, upsertWorker.Role);

        if (found)
        {
           _context.Update(updatedWorker);
            await _context.SaveChangesAsync();            
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
