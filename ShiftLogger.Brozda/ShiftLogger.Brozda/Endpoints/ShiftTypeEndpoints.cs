using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Brozda.API.Repositories;
using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Endpoints
{
    /// <summary>
    /// Provides extension methods to map API endpoints for shift types table.
    /// </summary>
    public static class ShiftTypeEndpoints
    {
        /// <summary>
        /// Maps HTTP endpoints related to shift types to the given endpoint route builder.
        /// </summary>
        /// <param name="builder">The route builder to which the endpoints will be added.</param>
        /// <returns>The same route builder instance with shift types routes mapped.</returns>
        public static IEndpointRouteBuilder MapShiftTypeEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder
                .MapGroup("/api/shifttypes")
                .WithTags("Shift Types");

            group.MapPost("/", async (
                ShiftTypeDto dto,
                IShiftTypeRepository repo) =>
            {
                var entity = await repo.Add(dto);

                return (entity is not null)
                ? Results.Created($"/api/shifttypes/{entity.Id}", entity)
                : Results.BadRequest();
            });

            group.MapGet("/", async (IShiftTypeRepository repo) =>
            {
                var entities = await repo.GetAll();
                return Results.Ok(entities);
            });

            group.MapGet("/{id:int}", async (
                int id,
                IShiftTypeRepository repo) =>
            {
                var entity = await repo.GetById(id);

                return (entity is not null)
                ? Results.Ok(entity)
                : Results.NotFound();
            });

            group.MapPut("/{id:int}", async (
                int id,
                [FromBody] ShiftTypeDto dto,
                IShiftTypeRepository repo) =>
            {
                if (dto.Id != id)
                    return Results.BadRequest("ID value in URL does not match ID in the request body");

                var entity = await repo.Update(dto, id);

                return (entity is not null)
                ? Results.Ok(entity)
                : Results.NotFound();
            });

            group.MapDelete("/{id:int}", async (
                int id,
                IShiftTypeRepository repo) =>
            {
                var result = await repo.DeleteById(id);

                return result ?
                Results.NoContent() :
                Results.NotFound();
            });

            return builder;
        }
    }
}