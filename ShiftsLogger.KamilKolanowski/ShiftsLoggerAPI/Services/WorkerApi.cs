using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Services;

internal class WorkerApi
{
    private readonly WorkerDbService _workerDbService;
    
    public WorkerApi(WorkerDbService workerDbService)
    {
        _workerDbService = workerDbService;
    }

    internal void Configure(WebApplication app)
    {
        app.MapPost("/workers", AddWorker);
        app.MapDelete("/workers/{id}", DeleteWorker);
        app.MapPut("/workers", EditWorker);
        app.MapGet("/workers/{id}", ReadWorker);
        app.MapGet("/workers", ReadAllWorkers);
    }

    private async Task<IResult> AddWorker([FromBody] Worker worker)
    {
        await _workerDbService.AddWorkerAsync(worker);
        return TypedResults.Created($"/workers/{worker.WorkerId}", worker);
    }

    private async Task<IResult> DeleteWorker(int id)
    {
        var worker = await _workerDbService.ReadWorkerAsync(id);
        if (worker == null)
            return TypedResults.NotFound();

        await _workerDbService.DeleteWorkerAsync(id);
        return TypedResults.NoContent();
    }

    private async Task<IResult> EditWorker([FromBody] Worker updatedWorker)
    {
        var existingWorker = await _workerDbService.ReadWorkerAsync(updatedWorker.WorkerId);
        if (existingWorker == null)
            return TypedResults.NotFound();

        existingWorker.FirstName = updatedWorker.FirstName;
        existingWorker.LastName = updatedWorker.LastName;
        existingWorker.Email = updatedWorker.Email;
        existingWorker.Role = updatedWorker.Role;

        await _workerDbService.UpdateWorkerAsync(existingWorker);
        return TypedResults.NoContent();
    }

    private async Task<IResult> ReadWorker(int id)
    {
        var worker = await _workerDbService.ReadWorkerAsync(id);
        return worker is null ? TypedResults.NotFound() : TypedResults.Ok(worker);
    }

    private async Task<IResult> ReadAllWorkers()
    {
        var workers = await _workerDbService.ReadAllWorkersAsync();
        return TypedResults.Ok(workers);
    }
}
