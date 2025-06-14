using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Services;

public class WorkerService(ShiftsLoggerDbContext dbContext) : IWorkerService
{
    public async Task<ApiResponseDto<List<Workers?>>> GetAllWorkers(
        WorkerFilterOptions workerOptions
    )
    {
        AnsiConsole.MarkupLine(
            $"[yellow]Filter options received:[/]\n"
                + $"  [blue]WorkerId:[/] {workerOptions.WorkerId ?? null}\n"
                + $"  [blue]Name:[/] {workerOptions.Name ?? "null"}\n"
                + $"  [blue]PhoneNumber:[/] {workerOptions.PhoneNumber ?? "null"}\n"
                + $"  [blue]Email:[/] {workerOptions.Email ?? "null"}\n"
                + $"  [blue]SortBy:[/] {workerOptions.SortBy ?? "null"}\n"
                + $"  [blue]SortOrder:[/] {workerOptions.SortOrder ?? "null"}\n"
                + $"  [blue]Search:[/] '{workerOptions.Search ?? "null"}'"
        );

        var query = dbContext
            .Workers.Include(w => w.Locations)
            .Include(w => w.Shifts)
            .AsQueryable();

        // Apply all filters
        if (workerOptions.WorkerId != null && workerOptions.WorkerId is not 0)
        {
            query = query.Where(w => w.WorkerId == workerOptions.WorkerId);
        }

        if (!string.IsNullOrWhiteSpace(workerOptions.Name))
        {
            query = query.Where(w => EF.Functions.Like(w.Name, $"%{workerOptions.Name}%"));
        }
        if (!string.IsNullOrWhiteSpace(workerOptions.PhoneNumber))
        {
            query = query.Where(w =>
                EF.Functions.Like(w.PhoneNumber, $"%{workerOptions.PhoneNumber}%")
            );
        }
        if (!string.IsNullOrWhiteSpace(workerOptions.Email))
        {
            query = query.Where(w => EF.Functions.Like(w.Email, $"%{workerOptions.Email}%"));
        }

        // Simplified search implementation
        if (!string.IsNullOrWhiteSpace(workerOptions.Search))
        {
            query = query.Where(w =>
                w.WorkerId.ToString().Contains(workerOptions.Search)
                || EF.Functions.Like(w.Name, $"%{workerOptions.Search}%")
                || EF.Functions.Like(w.PhoneNumber, $"%{workerOptions.Search}%")
                || EF.Functions.Like(w.Email, $"%{workerOptions.Search}%")
            );
        }

        if (!string.IsNullOrWhiteSpace(workerOptions.SortBy))
        {
            workerOptions.SortBy = workerOptions.SortBy.ToLowerInvariant();
            workerOptions.SortOrder = workerOptions.SortOrder?.ToLowerInvariant(); // Normalize sort order to lowercase
        }
        else
        {
            workerOptions.SortBy = "workerid"; // Default sort by WorkerId if not specified
        }

        AnsiConsole.MarkupLine(
            $"[yellow]Applying sorting:[/] SortBy='{workerOptions.SortBy}', SortOrder='{workerOptions.SortOrder}'"
        );

        // Always apply sorting - whether SortBy is specified or not
        query = workerOptions.SortBy switch
        {
            "workerid" => workerOptions.SortOrder == "asc"
                ? query.OrderBy(w => w.WorkerId)
                : query.OrderByDescending(w => w.WorkerId),
            "name" => workerOptions.SortOrder == "asc"
                ? query.OrderBy(w => w.Name)
                : query.OrderByDescending(w => w.Name),
            "phonenumber" => workerOptions.SortOrder == "asc"
                ? query.OrderBy(w => w.PhoneNumber)
                : query.OrderByDescending(w => w.PhoneNumber),
            "email" => workerOptions.SortOrder == "asc"
                ? query.OrderBy(w => w.Email)
                : query.OrderByDescending(w => w.Email),
            _ => query.OrderBy(w => w.WorkerId), // Default sorting by WorkerId
        };

        AnsiConsole.MarkupLine("[yellow]Executing final query...[/]");

        // Execute query and get results
        var workers = (await query.ToListAsync()).Cast<Workers?>().ToList();

        if (workers.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No workers found with the specified criteria.[/]");
            return new ApiResponseDto<List<Workers?>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = "No workers found with the specified criteria.",
                Data = workers,
            };
        }

        AnsiConsole.MarkupLine(
            $"[green]Successfully retrieved {workers.Count} workers, sorted by '{workerOptions.SortBy}' in {workerOptions.SortOrder} order.[/]"
        );
        return new ApiResponseDto<List<Workers?>>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.OK,
            Message = "Workers retrieved successfully.",
            Data = workers,
        };
    }

    public async Task<ApiResponseDto<List<Workers?>>> GetWorkerById(int id)
    {
        Workers? worker = await dbContext
            .Workers.Include(w => w.Locations)
            .Include(w => w.Shifts)
            .FirstOrDefaultAsync(w => w.WorkerId == id);

        if (worker is null)
        {
            return new ApiResponseDto<List<Workers?>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Worker with ID: {id} not found.",
                Data = [],
            };
        }
        else
        {
            AnsiConsole.MarkupLine(
                $"[green]Successfully retrieved worker with ID: {worker.WorkerId}.[/]"
            );
            return new ApiResponseDto<List<Workers?>>
            {
                RequestFailed = false,
                ResponseCode = System.Net.HttpStatusCode.OK,
                Message = $"Worker with ID: {id} retrieved successfully.",
                Data = [],
            };
        }
    }

    public async Task<ApiResponseDto<Workers>> CreateWorker(WorkerApiRequestDto worker)
    {
        try
        {
            Workers newWorker = new()
            {
                Name = worker.Name,
                PhoneNumber = worker.PhoneNumber,
                Email = worker.Email,
            };
            var savedWorker = await dbContext.Workers.AddAsync(newWorker);
            await dbContext.SaveChangesAsync();

            AnsiConsole.MarkupLine(
                $"\n[green]Successfully created worker with ID: {savedWorker.Entity.WorkerId}[/]"
            );

            return new ApiResponseDto<Workers>
            {
                RequestFailed = false,
                ResponseCode = System.Net.HttpStatusCode.Created,
                Message = "Worker created successfully.",
                Data = savedWorker.Entity,
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Back end worker service - {ex}");
            return new ApiResponseDto<Workers>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = "An error occurred while creating the worker.",
                Data = null,
            };
        }
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
        savedWorker.WorkerId = id; // Ensure the WorkerId is set to the ID being updated
        savedWorker.Name = updatedWorker.Name;
        savedWorker.PhoneNumber = updatedWorker.PhoneNumber;
        savedWorker.Email = updatedWorker.Email;

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

        if (savedWorker is null)
        {
            return new ApiResponseDto<string?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Worker with ID: {id} not found.",
                Data = null,
            };
        }

        dbContext.Workers.Remove(savedWorker);
        await dbContext.SaveChangesAsync();

        return new ApiResponseDto<string?>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.OK,
            Message = $"Worker with ID: {id} deleted successfully.",
            Data = null,
        };
    }
}
