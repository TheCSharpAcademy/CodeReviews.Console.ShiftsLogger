using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Controllers;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
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
                    || w.PhoneNumber.Contains(searchLower)
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

    public static  async Task<ActionResult<Workers>> GetWorkerOptionInput(
        ShiftsDbContext dbContext,
        IWorkerService workerService
    )
    { 
        var workers = await dbContext.Workers.ToListAsync();
        var workersArray = workers.Select(w => w.Name).ToArray();
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Select a worker:[/]")
                .AddChoices(workersArray)
        );
        var id = workers.FirstOrDefault(w => w.Name == option)?.WorkerId;

        // Create an instance of WorkersController to call the non-static method
        var workersController = new WorkersController(workerService);
        var worker = await workersController.GetWorkerById(id ?? 0);

        return worker;
    }

	public static async Task FrontEndViewWorkers(HttpClient httpClient)
	{
		try
		{
			var response = await httpClient.GetFromJsonAsync<ApiResponseDto<List<WorkerDto>>>(
				"api/workers"
			);
			if (response != null && response.Data != null && !response.RequestFailed)
			{
				var table = new Table().AddColumn("Name").AddColumn("Phone").AddColumn("Email");
				foreach (var worker in response.Data)
				{
					table.AddRow(worker.Name , worker.PhoneNumber , worker.Email);
				}
				AnsiConsole.Write(table);
			}
			else
			{
				AnsiConsole.MarkupLine("[red]Failed to retrieve workers.[/]");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"View workers failed, see {ex}");
			throw;
		}
	}
    public static async Task FrontEndViewWorkerById(HttpClient httpClient)
    {
        try
        {
            var workerId = AnsiConsole.Ask<int>("Enter [green]Worker ID[/]:");
            var response = await httpClient.GetFromJsonAsync<ApiResponseDto<Workers>>(
                $"api/workers/{workerId}"
            );
            if (response != null && response.Data != null && !response.RequestFailed)
            {
                var table = new Table()
                    .AddColumn("Name")
                    .AddColumn("Phone")
                    .AddColumn("Email")
                    .AddColumn("Location ID")
                    .AddColumn("Shift ID");
                table.AddRow(
                    response.Data.Name ,
                    response.Data.PhoneNumber ,
                    response.Data.Email ,
                    response.Data.Locations.ToString() ,
                    response.Data.Shifts.ToString()
                );
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to retrieve worker.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"View worker by ID failed, see {ex}");
            throw;
        }
	}
	public static async Task FrontEndAddWorker(HttpClient httpClient)
	{
		try
		{
			var name = AnsiConsole.Ask<string>("Enter [green]Worker Name[/]:");
			var phone = AnsiConsole.Ask<string>("Enter [green]Phone Number[/]:");
			var email = AnsiConsole.Ask<string>("Enter [green]Email Address[/]:");

			var response = await httpClient.PostAsJsonAsync(
				"api/workers" ,
				new
				{
					Name = name ,
					PhoneNumber = phone ,
					Email = email ,
				}
			);
			if (response.IsSuccessStatusCode)
			{
				AnsiConsole.MarkupLine("[green]Worker added successfully![/]");
			}
			else
			{
				AnsiConsole.MarkupLine("[red]Failed to add worker.[/]");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Adding worker failed due to: {ex}");
			throw;
		}
	}
    public static async Task FrontEndUpdateWorker(HttpClient httpClient)
    {
        try
        {
            var workerId = AnsiConsole.Ask<int>("Enter [green]Worker ID[/]:");
            var name = AnsiConsole.Ask<string>("Enter [green]New Worker Name[/]:");
            var phone = AnsiConsole.Ask<string>("Enter [green]New Phone Number[/]:");
            var email = AnsiConsole.Ask<string>("Enter [green]New Email Address[/]:");
            var response = await httpClient.PutAsJsonAsync(
                $"api/workers/{workerId}" ,
                new
                {
                    Name = name ,
                    PhoneNumber = phone ,
                    Email = email ,
                }
            );
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine("[green]Worker updated successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to update worker.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Updating worker failed due to: {ex}");
            throw;
        }
	}



	public static async Task FrontEndDeleteWorker(HttpClient httpClient , ShiftsDbContext dbContext , IWorkerService workerService)
	{
		try
		{
			var workerResult = await GetWorkerOptionInput(dbContext , workerService);
			var worker = workerResult.Value;
			if (worker == null || worker.WorkerId == 0)
			{
				AnsiConsole.MarkupLine("[red]No worker selected or invalid worker.[/]");
				return;
			}
			var response = await httpClient.DeleteAsync($"api/workers/{worker.WorkerId}");
			if (response.IsSuccessStatusCode)
			{
				AnsiConsole.MarkupLine("[green]Worker deleted successfully![/]");
			}
			else
			{
				AnsiConsole.MarkupLine("[red]Failed to delete worker.[/]");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Deleting worker failed due to: {ex}");
			throw;
		}
	}
}
