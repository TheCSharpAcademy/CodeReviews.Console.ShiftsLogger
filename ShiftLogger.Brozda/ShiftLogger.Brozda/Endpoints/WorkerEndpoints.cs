using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Brozda.API.Repositories;
using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Endpoints
{
    /// <summary>
    /// Provides extension methods to map API endpoints for workers table.
    /// </summary>
    public static class WorkerEndpoints
    {
        /// <summary>
        /// Maps HTTP endpoints related to workers to the given endpoint route builder.
        /// </summary>
        /// <param name="builder">The route builder to which the endpoints will be added.</param>
        /// <returns>The same route builder instance with workers routes mapped.</returns>
        public static IEndpointRouteBuilder MapWorkerEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder
                .MapGroup("/api/workers")
                .WithTags("Workers");

            group.MapPost("/", async (
                WorkerDto dto,
                IWorkerRepository repo) =>
            {
                var entity = await repo.Add(dto);

                return (entity is not null)
                ? Results.Created($"/api/workers/{entity.Id}", entity)
                : Results.BadRequest();
            });

            group.MapGet("/", async (IWorkerRepository repo) =>
            {
                var entitites = await repo.GetAll();

                return Results.Ok(entitites);
            });

            group.MapGet("/{id:int}", async (
                int id,
                IWorkerRepository repo) =>
            {
                var entity = await repo.GetById(id);

                return (entity is not null)
                ? Results.Ok(entity)
                : Results.NotFound();
            });

            group.MapPut("/{id:int}", async (
                int id,
                [FromBody] WorkerDto dto,
                IWorkerRepository repo) =>
            {
                var entity = await repo.Update(dto, id);

                return (entity is not null)
                ? Results.Ok(entity)
                : Results.NotFound();
            });
            group.MapDelete("/{id:int}", async (int id, IWorkerRepository repo) =>
            {
                var result = await repo.DeleteById(id);

                return result
                ? Results.NoContent()
                : Results.NotFound();
            }
            );

            return builder;
        }
    }
}