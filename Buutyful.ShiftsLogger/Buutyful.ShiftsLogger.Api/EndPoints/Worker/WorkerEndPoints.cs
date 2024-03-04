using Buutyful.ShiftsLogger.Api.Data;
using Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Buutyful.ShiftsLogger.Api.EndPoints.Worker;

public static class WorkerEndPoints
{
    public static void MapWorkerEndPoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/workers");

        group.MapGet("", async (WorkerDataAccess data) =>
        {
            var workers = await data.GetAsync();
            return Results.Ok(workers);
        });
        group.MapGet("{Id}", async (WorkerDataAccess data, Guid id) =>
        {
            var worker = await data.GetByIdAsync(id);
            return worker is null ? Results.NotFound() : Results.Ok(worker);
        });
        group.MapPost("", async (WorkerDataAccess data, CreateWorkerRequest workerRequest) =>
        {
            var worker = await data.AddAsync(workerRequest);
            return Results.Created($"/worker/{worker.Id}", worker);
        });
        group.MapPatch("", async (WorkerDataAccess data, UpsertWorkerRequest workerRequest) =>
        {
            var result = await data.UpdateWorkerAsync(workerRequest);
            return result.Updated ?            
            Results.NoContent() :
            Results.NotFound();
        });
        group.MapPut("", async (WorkerDataAccess data, UpsertWorkerRequest workerRequest) =>
        {
            var result = await data.UpsertWorkerAsync(workerRequest);
            return result.Created ? 
            Results.Created($"/worker/{result.Worker.Id}", result.Worker) :
            Results.NoContent();
        });
        group.MapDelete("{Id}", async (WorkerDataAccess data, Guid Id) =>
        {
            var res = await data.DeleteAsync(Id);
            return res ? Results.NoContent() : Results.NotFound();
        });
    }
}
