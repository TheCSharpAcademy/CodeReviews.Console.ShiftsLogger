using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Services;

public class WorkerService(ShiftsDbContext dbContext, IMapper mapper) : IWorkerService
{
    public async Task<ApiResponseDto<List<Workers>>> GetAllWorkers(
        WorkerFilterOptions workerOptions
    )
    {
        var query = dbContext.Workers.Include(w => w.Shifts).AsQueryable();

        List<Workers>? workers;

        if (!string.IsNullOrEmpty(workerOptions.WorkerId.ToString()))
        {
            query = query.Where(w => w.WorkerId.ToString() == workerOptions.WorkerId.ToString());
        }

        if (workerOptions.SortBy == "worker_id" || !string.IsNullOrEmpty(workerOptions.SortBy))
        {
            if (!string.IsNullOrEmpty(workerOptions.SortBy))
            {
                var sortBy = workerOptions.SortBy.ToLowerInvariant();
                var sortOrder = workerOptions.SortOrder?.ToLowerInvariant() ?? "asc";

                switch (sortBy)
                {
                    case "worker_id":
                        query = workerOptions.SortOrder.Equals(
                            "ASC",
                            StringComparison.CurrentCultureIgnoreCase
                        )
                            ? query.OrderBy(s => s.WorkerId)
                            : query.OrderByDescending(s => s.WorkerId);
                        break;
                    default:
                        query = workerOptions.SortOrder.Equals(
                            "ASC",
                            StringComparison.CurrentCultureIgnoreCase
                        )
                            ? query.OrderBy(w => w.WorkerId)
                            : query.OrderByDescending(w => w.WorkerId);
                        break;
                }
            }
        }

        if (!string.IsNullOrEmpty(workerOptions.Search))
        {
            string searchLower = workerOptions.Search.ToLower();
            var data = await query.ToListAsync();

            workers = data.Where(w =>
                    w.WorkerId.ToString().Contains(searchLower)
                    || w.Name.ToLower().Contains(searchLower)
                    || w.Phone.Contains(searchLower)
                    || w.Email.ToLower().Contains(searchLower)
                )
                .ToList();
        }
        else
        {
            workers = await query.ToListAsync();
        }

        if (workers is null || workers.Count == 0)
        {
            return new ApiResponseDto<List<Workers>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = "No workers found.",
            };
        }

        return new ApiResponseDto<List<Workers>>
        {
            Data = workers,
            Message = "Workers retrieved successfully",
            ResponseCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<ApiResponseDto<Workers?>> GetWorkerById(int id)
    {
        Workers? worker = await dbContext
            .Workers.Include(w => w.Shifts)
            .Include(w => w.Locations)
            .FirstOrDefaultAsync(w => w.WorkerId == id);
        if (worker is null)
        {
            return new ApiResponseDto<Workers?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Worker with ID: {id} not found.",
            };
        }
        return new ApiResponseDto<Workers?>
        {
            Data = worker,
            Message = "Worker retrieved successfully",
            ResponseCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<ApiResponseDto<Workers>> CreateWorker(WorkerApiRequestDto workerDto)
    {
        Workers newWorker = mapper.Map<Workers>(workerDto);
        var savedWorker = await dbContext.Workers.AddAsync(newWorker);
        await dbContext.SaveChangesAsync();

        AnsiConsole.MarkupLine(
            $"\n[green]Successfully created worker with ID: {savedWorker.Entity.WorkerId}[/]"
        );
        return new ApiResponseDto<Workers>
        {
            Data = savedWorker.Entity,
            Message = "Worker created successfully",
            ResponseCode = System.Net.HttpStatusCode.Created,
        };
    }

    public async Task<ApiResponseDto<Workers?>> UpdateWorker(
        int id,
        WorkerApiRequestDto updatedWorker
    )
    {
        Workers? savedWorker = await dbContext.Workers.FindAsync(id);

        if (savedWorker is null)
        {
            return new ApiResponseDto<Workers?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Worker with ID: {id} not found.",
            };
        }

        mapper.Map(updatedWorker, savedWorker);
        savedWorker.WorkerId = id;

        dbContext.Workers.Update(savedWorker);

        await dbContext.SaveChangesAsync();

        return new ApiResponseDto<Workers?>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.OK,
            Message = $"Worker with ID: {id} updated successfully.",
            Data = savedWorker,
        };
    }

    public async Task<ApiResponseDto<string?>> DeleteWorker(int id)
    {
        Workers? savedWorker = await dbContext.Workers.FindAsync(id);

        if (savedWorker == null)
        {
            return new ApiResponseDto<string?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Worker with ID: {id} not found.",
            };
        }

        dbContext.Workers.Remove(savedWorker);
        await dbContext.SaveChangesAsync();

        return new ApiResponseDto<string?>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.NoContent,
            Message = $"Worker with ID: {id} deleted successfully.",
        };
    }
}
