using Microsoft.EntityFrameworkCore;

using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;

using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Services;

public class WorkerService(ShiftsDbContext dbContext) : IWorkerService
{
	public async Task<ApiResponseDto<List<Workers?>>> GetAllWorkers(
		WorkerFilterOptions workerOptions
	)
	{
		var query = dbContext.Workers.AsQueryable();
		List<Workers?> workers;

		if (!string.IsNullOrEmpty(workerOptions.Name))
		{
			query = query.Where(w => w.Name.ToLower().Contains(workerOptions.Name.ToLower()));
		}
		if (!string.IsNullOrEmpty(workerOptions.SortBy))
		{
			var sortBy = workerOptions.SortBy.ToLowerInvariant();
			var sortOrder = workerOptions.SortOrder?.ToLowerInvariant() ?? "asc";

			switch (sortBy)
			{
				case "worker_id":
					query =
						sortOrder == "asc"
							? query.OrderBy(w => w.WorkerId)
							: query.OrderByDescending(w => w.WorkerId);
					break;
				case "name":
					query =
						sortOrder == "asc"
							? query.OrderBy(w => w.Name)
							: query.OrderByDescending(w => w.Name);
					break;
				default:
					query =
						sortOrder == "asc"
							? query.OrderBy(w => w.WorkerId)
							: query.OrderByDescending(w => w.WorkerId);
					break;
			}
		}

		if (!string.IsNullOrEmpty(workerOptions.Search))
		{
			string search = workerOptions.Search.ToLowerInvariant();
			var data = await query.ToListAsync();

			workers = data.Where(w =>
					w.WorkerId.ToString().Contains(search)
					|| (w.Name != null && w.Name.ToLower().Contains(search))
				)
				.Cast<Workers?>()
				.ToList();
		}
		else
		{
			workers = (await query.ToListAsync()).Cast<Workers?>().ToList();
		}

		if (workers is null || workers.Count == 0)
		{
			AnsiConsole.MarkupLine("[red]No workers found with the specified criteria.[/]");
			return new ApiResponseDto<List<Workers?>>
			{
				RequestFailed = true ,
				ResponseCode = System.Net.HttpStatusCode.NotFound ,
				Message = "No workers found with the specified criteria." ,
				Data = workers ,
			};
		}
		else
		{
			AnsiConsole.MarkupLine($"[green]Successfully retrieved {workers.Count} workers.[/]");
			return new ApiResponseDto<List<Workers?>>()
			{
				RequestFailed = false ,
				ResponseCode = System.Net.HttpStatusCode.OK ,
				Message = "Workers retrieved successfully." ,
				Data = workers ,
			};
		}
	}

	public async Task<ApiResponseDto<List<Workers?>>> GetWorkerById(int id)
	{
		Workers? worker = await dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == id);

		if (worker is null)
		{
			return new ApiResponseDto<List<Workers?>>
			{
				RequestFailed = true ,
				ResponseCode = System.Net.HttpStatusCode.NotFound ,
				Message = $"Worker with ID: {id} not found." ,
				Data = new List<Workers?>() ,
			};
		}
		else
		{
			AnsiConsole.MarkupLine(
				$"[green]Successfully retrieved worker with ID: {worker.WorkerId}.[/]"
			);
			return new ApiResponseDto<List<Workers?>>
			{
				RequestFailed = false ,
				ResponseCode = System.Net.HttpStatusCode.OK ,
				Message = $"Worker with ID: {id} retrieved successfully." ,
				Data = new List<Workers?> { worker } ,
			};
		}
	}

	public async Task<ApiResponseDto<Workers>> CreateWorker(WorkerApiRequestDto worker)
	{
		try
		{
			Workers newWorker = new Workers
			{
				Name = worker.Name ,
				// Add other properties as needed
			};
			var savedWorker = await dbContext.Workers.AddAsync(newWorker);
			await dbContext.SaveChangesAsync();

			AnsiConsole.MarkupLine(
				$"\n[green]Successfully created worker with ID: {savedWorker.Entity.WorkerId}[/]"
			);

			return new ApiResponseDto<Workers>
			{
				RequestFailed = false ,
				ResponseCode = System.Net.HttpStatusCode.Created ,
				Message = "Worker created successfully." ,
				Data = savedWorker.Entity ,
			};
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Back end worker service - {ex}");
			return new ApiResponseDto<Workers>
			{
				RequestFailed = true ,
				ResponseCode = System.Net.HttpStatusCode.InternalServerError ,
				Message = "An error occurred while creating the worker." ,
				Data = null ,
			};
		}
	}

	public async Task<ApiResponseDto<Workers?>> UpdateWorker(
		int id ,
		WorkerApiRequestDto updatedWorker
	)
	{
		Workers? savedWorker = await dbContext.Workers.FindAsync(id);

		if (savedWorker is null)
		{
			return new ApiResponseDto<Workers?>
			{
				RequestFailed = true ,
				ResponseCode = System.Net.HttpStatusCode.NotFound ,
				Message = $"Worker with ID: {id} not found." ,
			};
		}
		savedWorker.WorkerId = id; // Ensure the WorkerId is set to the ID being updated
		savedWorker.Name = updatedWorker.Name;
		// Update other properties as needed

		dbContext.Workers.Update(savedWorker);
		await dbContext.SaveChangesAsync();

		return new ApiResponseDto<Workers?>
		{
			RequestFailed = false ,
			ResponseCode = System.Net.HttpStatusCode.OK ,
			Message = $"Worker with ID: {id} updated successfully." ,
			Data = savedWorker ,
		};
	}

	public async Task<ApiResponseDto<string?>> DeleteWorker(int id)
	{
		Workers? savedWorker = await dbContext.Workers.FindAsync(id);

		if (savedWorker is null)
		{
			return new ApiResponseDto<string?>
			{
				RequestFailed = true ,
				ResponseCode = System.Net.HttpStatusCode.NotFound ,
				Message = $"Worker with ID: {id} not found." ,
				Data = null ,
			};
		}

		dbContext.Workers.Remove(savedWorker);
		await dbContext.SaveChangesAsync();

		return new ApiResponseDto<string?>
		{
			RequestFailed = false ,
			ResponseCode = System.Net.HttpStatusCode.OK ,
			Message = $"Worker with ID: {id} deleted successfully." ,
			Data = null ,
		};
	}
}
