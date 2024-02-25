using Buutyful.ShiftsLogger.Api.Data;
using Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;
using System.Runtime.CompilerServices;

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
    }
}
