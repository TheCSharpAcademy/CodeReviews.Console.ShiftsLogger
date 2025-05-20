using AutoMapper;

using Microsoft.EntityFrameworkCore;

using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;

using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Services;

public class ShiftService(ShiftsDbContext dbContext , IMapper mapper) : IShiftService
{
	public async Task<ApiResponseDto<List<Shifts>>> GetAllShifts(ShiftFilterOptions shiftOptions)
	{
		//var shifts = await dbContext.Shifts.ToListAsync();
		var query = dbContext.Shifts.Include(s => s.Location).Include(s => s.Worker).AsQueryable(); // Allow for filtering
		List<Shifts>? shifts;

		if (!string.IsNullOrEmpty(shiftOptions.WorkerId.ToString()))
		{
			query = query.Where(s => s.WorkerId.ToString() == shiftOptions.WorkerId.ToString());
		}
		if (shiftOptions.StartTime != null)
		{
			query = query.Where(s => s.StartTime <= shiftOptions.StartTime);
		}
		if (shiftOptions.EndTime != null)
		{
			query = query.Where(s => s.EndTime <= shiftOptions.EndTime);
		}
		if (shiftOptions.LocationId != null)
		{
			query = query.Where(s => s.LocationId.ToString() == shiftOptions.LocationId.ToString());
		}
		if (!string.IsNullOrEmpty(shiftOptions.LocationName))
		{
			query = query.Where(s =>
				s.Location.Name.ToLower().Contains(shiftOptions.LocationName.ToLower())
			);
		}
		if (shiftOptions.StartTime != null)
		{
			query = query.Where(s => s.StartTime.Date >= shiftOptions.StartTime.Value.Date); // allows for filtering by just date (have to use StartTime.Date in the query)
		}
		if (shiftOptions.EndTime != null)
		{
			query = query.Where(s => s.EndTime.Date <= shiftOptions.EndTime.Value.Date); // allows for filtering by just date (have to use EndTime.Date in the query)
		}
		if (shiftOptions.SortBy == "shift_id" || !string.IsNullOrEmpty(shiftOptions.SortBy))
		{
			if (!string.IsNullOrEmpty(shiftOptions.SortBy))
			{
				var sortBy = shiftOptions.SortBy.ToLowerInvariant();
				var sortOrder = shiftOptions.SortOrder?.ToLowerInvariant() ?? "asc";

				switch (sortBy)
				{
					case "shift_id":
						query = shiftOptions.SortOrder.Equals(
							"ASC" ,
							StringComparison.CurrentCultureIgnoreCase
						)
							? query.OrderBy(s => s.ShiftId)
							: query.OrderByDescending(s => s.ShiftId);
						break;
					case "start_time":
						query = shiftOptions.SortOrder.Equals(
							"ASC" ,
							StringComparison.CurrentCultureIgnoreCase
						)
							? query.OrderBy(s => s.StartTime)
							: query.OrderByDescending(s => s.StartTime);
						break;
					case "end_time":
						query = shiftOptions.SortOrder.Equals(
							"ASC" ,
							StringComparison.CurrentCultureIgnoreCase
						)
							? query.OrderBy(s => s.EndTime)
							: query.OrderByDescending(s => s.EndTime);
						break;
					case "worker_id":
						query = shiftOptions.SortOrder.Equals(
							"ASC" ,
							StringComparison.CurrentCultureIgnoreCase
						)
							? query.OrderBy(s => s.WorkerId)
							: query.OrderByDescending(s => s.WorkerId);
						break;
					case "location_id":
						query = shiftOptions.SortOrder.Equals(
							"ASC" ,
							StringComparison.CurrentCultureIgnoreCase
						)
							? query.OrderBy(s => s.LocationId)
							: query.OrderByDescending(s => s.LocationId);
						break;
					case "location_name":
						query = shiftOptions.SortOrder.Equals(
							"ASC" ,
							StringComparison.CurrentCultureIgnoreCase
						)
							? query.OrderBy(s => s.Location.Name)
							: query.OrderByDescending(s => s.Location.Name);
						break;
					default:
						query = shiftOptions.SortOrder.Equals(
							"ASC" ,
							StringComparison.CurrentCultureIgnoreCase
						)
							? query.OrderBy(s => s.ShiftId)
							: query.OrderByDescending(s => s.ShiftId);
						break;
				}
			}
		}

		if (!string.IsNullOrEmpty(shiftOptions.Search))
		{
			string searchLower = shiftOptions.Search.ToLower();
			var searchChars = searchLower.ToCharArray();

			var data = await query.ToListAsync();

			shifts = data.Where(s =>
					s.WorkerId.ToString().Contains(searchLower)
					|| s.StartTime.ToString().Contains(searchLower)
					|| s.EndTime.ToString().Contains(searchLower)
					|| s.LocationId.ToString().Contains(searchLower)
					|| s.Location.Name.ToLower().Contains(searchLower)
					|| s.Location.TownOrCity.ToLower().Contains(searchLower)
					|| s.Location.StateOrCounty.ToLower().Contains(searchLower)
					|| s.Location.Country.ToLower().Contains(searchLower)
					|| searchChars.All(c =>
						s.StartTime.ToString("yyyy-MM-ddTHH:mm:ss").ToLower().Contains(c)
					)
					|| searchChars.All(c =>
						s.EndTime.ToString("yyyy-MM-ddTHH:mm:ss").ToLower().Contains(c)
					)
				)
				.ToList();
		}
		else
		{
			shifts = await query.ToListAsync();
		}

		if (shifts is null || shifts.Count == 0)
		{
			return new ApiResponseDto<List<Shifts>>
			{
				RequestFailed = true ,
				ResponseCode = System.Net.HttpStatusCode.NotFound ,
				Message = "No shifts found." ,
			};
		}

		return new ApiResponseDto<List<Shifts>>
		{
			Data = shifts ,
			Message = "Shifts retrieved successfully" ,
			ResponseCode = System.Net.HttpStatusCode.OK ,
		};
	}

	public async Task<ApiResponseDto<Shifts?>> GetShiftById(int id)
	{
		Shifts? shift = await dbContext
			.Shifts.Include(s => s.Location)
			.Include(s => s.Worker)
			.FirstOrDefaultAsync(s => s.ShiftId == id);
		if (shift is null)
		{
			return new ApiResponseDto<Shifts?>
			{
				RequestFailed = true ,
				ResponseCode = System.Net.HttpStatusCode.NotFound ,
				Message = $"Shifts with ID: {id} not found." ,
			};
		}
		return new ApiResponseDto<Shifts?>
		{
			Data = shift ,
			Message = "Shifts retrieved successfully" ,
			ResponseCode = System.Net.HttpStatusCode.OK ,
		};
	}

	public async Task<ApiResponseDto<Shifts>> CreateShift(ShiftApiRequestDto shift)
	{
		Shifts newShift = mapper.Map<Shifts>(shift); // Map the DTO to the Shifts entity
		var savedShift = await dbContext.Shifts.AddAsync(newShift);
		await dbContext.SaveChangesAsync();

		AnsiConsole.MarkupLine(
			$"\n[green]Successfully created shift with ID: {savedShift.Entity.ShiftId}[/]"
		);
		return new ApiResponseDto<Shifts>
		{
			Data = savedShift.Entity ,
			Message = "Shifts created successfully" ,
			ResponseCode = System.Net.HttpStatusCode.Created ,
		};
	}

	public async Task<ApiResponseDto<Shifts?>> UpdateShift(int id , ShiftApiRequestDto updatedShift)
	{
		Shifts? savedShift = await dbContext.Shifts.FindAsync(id);

		if (savedShift is null)
		{
			return new ApiResponseDto<Shifts?>
			{
				RequestFailed = true ,
				ResponseCode = System.Net.HttpStatusCode.NotFound ,
				Message = $"Shifts with ID: {id} not found." ,
			};
		}

		// Map the updated properties from the DTO to the existing entity
		mapper.Map(updatedShift , savedShift);
		savedShift.ShiftId = id; // Ensure the ID is set correctly

		dbContext.Shifts.Update(savedShift);

		await dbContext.SaveChangesAsync();

		return new ApiResponseDto<Shifts?>
		{
			RequestFailed = false ,
			ResponseCode = System.Net.HttpStatusCode.OK ,
			Message = $"Shifts with ID: {id} updated successfully." ,
			Data = savedShift ,
		};
	}

	public async Task<ApiResponseDto<string?>> DeleteShift(int id)
	{
		Shifts? savedShift = await dbContext.Shifts.FindAsync(id);

		if (savedShift == null)
		{
			return new ApiResponseDto<string?>
			{
				RequestFailed = true ,
				ResponseCode = System.Net.HttpStatusCode.NotFound ,
				Message = $"Shifts with ID: {id} not found." ,
			};
		}

		dbContext.Shifts.Remove(savedShift);
		await dbContext.SaveChangesAsync();

		return new ApiResponseDto<string?>
		{
			RequestFailed = false ,
			ResponseCode = System.Net.HttpStatusCode.NoContent ,
			Message = $"Shifts with ID: {id} deleted successfully." ,
		};
	}

	public async Task FrontEndAddShift(HttpClient httpClient)
	{
		try
		{
			var workerId = AnsiConsole.Ask<int>("Enter [green]Worker ID[/]:");
			var locationId = AnsiConsole.Ask<int>("Enter [green]Location ID[/]:");
			var startTime = AnsiConsole.Ask<DateTime>(
				"Enter [green]Start Time (yyyy-MM-DD HH:mm)[/]:"
			);
			var endTime = AnsiConsole.Ask<DateTime>("Enter [green]End Time (yyyy-MM-DD HH:mm)[/]:");

			var response = await httpClient.PostAsJsonAsync(
				"api/shifts" ,
				new
				{
					WorkerId = workerId ,
					LocationId = locationId ,
					StartTime = startTime ,
					EndTime = endTime ,
				}
			);
			if (response.IsSuccessStatusCode)
			{
				AnsiConsole.MarkupLine("[green]Shift added successfully![/]");
			}
			else
			{
				AnsiConsole.MarkupLine("[red]Failed to add location.[/]");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Add shiuft failed, see {ex}");
			throw;
		}
	}

	public async Task FrontEndViewShifts(HttpClient httpClient)
	{
		try
		{
			var response = await httpClient.GetFromJsonAsync<ApiResponseDto<List<ShiftsDto>>>(
				"api/shifts"
			);
			if (response != null && response.Data != null && !response.RequestFailed)
			{
				var table = new Table()
					.AddColumn("Worker ID")
					.AddColumn("Location ID")
					.AddColumn("Start Time")
					.AddColumn("End Time");

				foreach (var shift in response.Data)
				{
					table.AddRow(
						shift.WorkerId.ToString() ,
						shift.LocationId.ToString() ,
						shift.StartTime.ToString("yyyy-MM-d HH:mm:ss (en-GB)") ,
						shift.EndTime.ToString("yyyy-MM-d HH:mm:ss (en-GB)")
					);
				}
				AnsiConsole.Write(table);
			}
			else
			{
				AnsiConsole.MarkupLine(
					$"[red]Failed to retrieve shifts: {response?.Message ?? "Unknown error"}[/]"
				);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[red]Get All Shifts failed, see Exception: {ex.Message}[/]");
		}
	}
}
