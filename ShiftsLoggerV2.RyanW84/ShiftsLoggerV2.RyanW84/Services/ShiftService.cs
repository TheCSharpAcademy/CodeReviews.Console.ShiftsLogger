using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Services;

class ShiftService(ShiftsDbContext dbContext) : IShiftService
{
    public async Task<ApiResponseDto<List<Shifts?>>> GetAllShifts(ShiftFilterOptions shiftOptions)
    {
        var query = dbContext.Shifts.Include(s => s.Location).Include(s => s.Worker).AsQueryable();
        List<Shifts?> shifts;

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
            query = query.Where(s => s.StartTime.Date >= shiftOptions.StartTime.Value.Date);
        }
        if (shiftOptions.EndTime != null)
        {
            query = query.Where(s => s.EndTime.Date <= shiftOptions.EndTime.Value.Date);
        }
        if (!string.IsNullOrEmpty(shiftOptions.SortBy))
        {
            var sortBy = shiftOptions.SortBy.ToLowerInvariant();
            var sortOrder = shiftOptions.SortOrder?.ToLowerInvariant() ?? "asc";

            switch (sortBy)
            {
                case "shift_id":
                    query =
                        sortOrder == "asc"
                            ? query.OrderBy(s => s.ShiftId)
                            : query.OrderByDescending(s => s.ShiftId);
                    break;
                case "start_time":
                    query =
                        sortOrder == "asc"
                            ? query.OrderBy(s => s.StartTime)
                            : query.OrderByDescending(s => s.StartTime);
                    break;
                case "end_time":
                    query =
                        sortOrder == "asc"
                            ? query.OrderBy(s => s.EndTime)
                            : query.OrderByDescending(s => s.EndTime);
                    break;
                case "worker_id":
                    query =
                        sortOrder == "asc"
                            ? query.OrderBy(s => s.WorkerId)
                            : query.OrderByDescending(s => s.WorkerId);
                    break;
                case "location_id":
                    query =
                        sortOrder == "asc"
                            ? query.OrderBy(s => s.LocationId)
                            : query.OrderByDescending(s => s.LocationId);
                    break;
                case "location_name":
                    query =
                        sortOrder == "asc"
                            ? query.OrderBy(s => s.Location.Name)
                            : query.OrderByDescending(s => s.Location.Name);
                    break;
                default:
                    query =
                        sortOrder == "asc"
                            ? query.OrderBy(s => s.ShiftId)
                            : query.OrderByDescending(s => s.ShiftId);
                    break;
            }
        }

        if (!string.IsNullOrEmpty(shiftOptions.Search))
        {
            string search =
                shiftOptions.Search.IndexOf((char)StringComparison.InvariantCultureIgnoreCase) >= 0
                    ? shiftOptions.Search.ToLowerInvariant()
                    : shiftOptions.Search;
            var searchChars = search.ToCharArray();

            var data = await query.ToListAsync();

            shifts = data.Where(s =>
                    s.WorkerId.ToString().Contains(search)
                    || s.StartTime.ToString().Contains(search)
                    || s.EndTime.ToString().Contains(search)
                    || s.LocationId.ToString().Contains(search)
                    || s.Location.Name.Contains(search)
                    || s.Location.TownOrCity.Contains(search)
                    || s.Location.StateOrCounty.Contains(search)
                    || s.Location.Country.Contains(search)
                    || searchChars.All(c =>
                        s.StartTime.ToString("yyyy-MM-ddTHH:mm:ss").ToLower().Contains(c)
                    )
                    || searchChars.All(c =>
                        s.EndTime.ToString("yyyy-MM-ddTHH:mm:ss").ToLower().Contains(c)
                    )
                )
                .Cast<Shifts?>()
                .ToList();
        }
        else
        {
            shifts = (await query.ToListAsync()).Cast<Shifts?>().ToList();
        }

        if (shifts is null || shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No shifts found with the specified criteria.[/]");

            return new ApiResponseDto<List<Shifts?>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = "No shifts found with the specified criteria.",
                Data = shifts,
            };
        }
        else
        {
            AnsiConsole.MarkupLine($"[green]Successfully retrieved {shifts.Count} shifts.[/]");

            return new ApiResponseDto<List<Shifts?>>()
            {
                RequestFailed = false,
                ResponseCode = System.Net.HttpStatusCode.OK,
                Message = "Shifts retrieved successfully.",
                Data = shifts,
            };
        }
    }

    public async Task<ApiResponseDto<List<Shifts?>>> GetShiftById(int id)
    {
        Shifts? shift = await dbContext
            .Shifts.Include(s => s.Location)
            .Include(s => s.Worker)
            .FirstOrDefaultAsync(s => s.ShiftId == id);

        if (shift is null)
        {
            return new ApiResponseDto<List<Shifts?>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Shift with ID: {id} not found.",
                Data = new List<Shifts?>(),
            };
        }
        else
        {
            AnsiConsole.MarkupLine(
                $"[green]Successfully retrieved shift with ID: {shift.ShiftId}.[/]"
            );
            return new ApiResponseDto<List<Shifts?>>
            {
                RequestFailed = false,
                ResponseCode = System.Net.HttpStatusCode.OK,
                Message = $"Shift with ID: {id} retrieved successfully.",
                Data = new List<Shifts?> { shift },
            };
        }
    }

    public async Task<ApiResponseDto<Shifts>> CreateShift(ShiftApiRequestDto shift)
    {
        try
        {
            Shifts newShift = new Shifts
            {
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                WorkerId = shift.WorkerId,
                LocationId = shift.LocationId,
            };
            var savedShift = await dbContext.Shifts.AddAsync(newShift);
            await dbContext.SaveChangesAsync();

            AnsiConsole.MarkupLine(
                $"\n[green]Successfully created shift with ID: {savedShift.Entity.ShiftId}[/]"
            );

            return new ApiResponseDto<Shifts>
            {
                RequestFailed = false,
                ResponseCode = System.Net.HttpStatusCode.Created,
                Message = "Shift created successfully.",
                Data = savedShift.Entity,
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Back end shift service - {ex}");
            return new ApiResponseDto<Shifts>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = "An error occurred while creating the shift.",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<Shifts?>> UpdateShift(int id, ShiftApiRequestDto updatedShift)
    {
        Shifts? savedShift = await dbContext.Shifts.FindAsync(id);

        if (savedShift is null)
        {
            return new ApiResponseDto<Shifts?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Shift with ID: {id} not found.",
            };
        }
        savedShift.ShiftId = id; // Ensure the ShiftId is set to the ID being updated
        savedShift.StartTime = updatedShift.StartTime;
        savedShift.EndTime = updatedShift.EndTime;
        savedShift.WorkerId = updatedShift.WorkerId;
        savedShift.LocationId = updatedShift.LocationId;

        dbContext.Shifts.Update(savedShift);
        await dbContext.SaveChangesAsync();

        return new ApiResponseDto<Shifts?>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.OK,
            Message = $"Shift with ID: {id} updated successfully.",
            Data = savedShift,
        };
    }

    public async Task<ApiResponseDto<string?>> DeleteShift(int id)
    {
        Shifts? savedShift = await dbContext.Shifts.FindAsync(id);

        if (savedShift is null)
        {
            return new ApiResponseDto<string?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Shift with ID: {id} not found.",
                Data = null,
            };
        }

        dbContext.Shifts.Remove(savedShift);
        await dbContext.SaveChangesAsync();

        return new ApiResponseDto<string?>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.OK,
            Message = $"Shift with ID: {id} deleted successfully.",
            Data = null,
        };
    }
}
