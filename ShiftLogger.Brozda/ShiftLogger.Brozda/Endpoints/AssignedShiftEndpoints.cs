using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Brozda.API.Repositories;
using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Endpoints
{
    /// <summary>
    /// Provides extension methods to map API endpoints for assigned shifts table.
    /// </summary>
    public static class AssignedShiftEndpoints
    {
        /// <summary>
        /// Maps HTTP endpoints related to assigned shifts to the given endpoint route builder.
        /// </summary>
        /// <param name="builder">The route builder to which the endpoints will be added.</param>
        /// <returns>The same route builder instance with assigned shift routes mapped.</returns>
        public static IEndpointRouteBuilder MapAssignedShiftsEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder
                .MapGroup("/api/assignedshifts")
                .WithTags("Assigned Shifts");

            group.MapPost("/", async (
                AssignedShiftDto dto,
                IAssignedShiftRepository repo) =>
            {
                var entity = await repo.Add(dto);

                return (entity is not null)
                ? Results.Created($"/api/assignedshifts/{entity.Id}", entity)
                : Results.BadRequest();
            });

            group.MapGet("/", async (IAssignedShiftRepository repo) =>
            {
                var entities = await repo.GetAll();

                return Results.Ok(entities);
            });

            group.MapGet("/{id:int}", async (
                int id,
                IAssignedShiftRepository repo) =>
            {
                var entity = await repo.GetById(id);

                return (entity is not null)
                ? Results.Ok(entity)
                : Results.NotFound();
            });
            group.MapGet("/date/{date:datetime}", async (
               DateTime date,
               IAssignedShiftRepository repo) =>
            {
                var entity = await repo.GetShiftsForDateMapped(date);

                return (entity is not null)
                ? Results.Ok(entity)
                : Results.NotFound();
            });
            group.MapGet("/worker/{id:int}", async (int id, IWorkerRepository workerRepo, IAssignedShiftRepository shiftsRepo) =>
            {
                var workerExist = await workerRepo.DoesWorkerExist(id);

                if (!workerExist)
                    return Results.NotFound();

                var entities = await shiftsRepo.GetAllForWorkerMapped(id);

                return Results.Ok(entities);
            });

            group.MapPut("/{id:int}", async (
                int id,
                [FromBody] AssignedShiftDto dto,
                IAssignedShiftRepository repo) =>
            {
                if (dto.Id != id)
                    return Results.BadRequest();

                var entity = await repo.Update(dto, id);

                return (entity is not null)
                ? Results.Ok(entity)
                : Results.NotFound();
            });

            group.MapDelete("/{id:int}", async (
                int id,
                IAssignedShiftRepository repo) =>
            {
                var result = await repo.DeleteById(id);

                return result
                ? Results.NoContent()
                : Results.NotFound();
            });

            return builder;
        }
    }
}