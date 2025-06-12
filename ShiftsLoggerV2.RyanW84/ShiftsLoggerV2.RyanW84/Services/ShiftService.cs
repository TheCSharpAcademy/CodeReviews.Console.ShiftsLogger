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
        AnsiConsole.MarkupLine(
            $"[yellow]Filter options received:[/]\n"
                + $"  [blue]ShiftId:[/] {shiftOptions.ShiftId}\n"
                + $"  [blue]WorkerId:[/] {shiftOptions.WorkerId ?? 0}\n"
                + $"  [blue]LocationId:[/] {shiftOptions.LocationId ?? 0}\n"
                + $"  [blue]LocationName:[/] {shiftOptions.LocationName ?? "null"}\n"
                + $"  [blue]StartTime:[/] {shiftOptions.StartTime?.ToString() ?? "null"}\n"
                + $"  [blue]EndTime:[/] {shiftOptions.EndTime?.ToString() ?? "null"}\n"
                + $"  [blue]SortBy:[/] {shiftOptions.SortBy ?? "null"}\n"
                + $"  [blue]SortOrder:[/] {shiftOptions.SortOrder ?? "null"}\n"
                + $"  [blue]Search:[/] '{shiftOptions.Search ?? "null"}'"
        );

        var query = dbContext.Shifts.Include(s => s.Location).Include(s => s.Worker).AsQueryable();

        // Apply all filters
        if (shiftOptions.ShiftId != null && shiftOptions.ShiftId is not 0) // This had to be done this way as Shift ID is not nullable
        {
            query = query.Where(s => s.ShiftId == shiftOptions.ShiftId);
        }

        if (shiftOptions.WorkerId is not null and not 0)
        {
            if (shiftOptions.ShiftId is not 0)
                query = query.Where(s => s.WorkerId == shiftOptions.WorkerId);
        }

        if (shiftOptions.LocationId is not null and not 0)
        {
            query = query.Where(s => s.LocationId == shiftOptions.LocationId);
        }

        if (!string.IsNullOrEmpty(shiftOptions.LocationName))
        {
            query = query.Where(s =>
                EF.Functions.Like(s.Location.Name, $"%{shiftOptions.LocationName}%")
            );
        }

        // Date filters
        if (shiftOptions.StartTime is not null)
        {
            query = query.Where(s => s.StartTime.Date >= shiftOptions.StartTime.Value.Date);
        }

        if (shiftOptions.EndTime is not null)
        {
            query = query.Where(s => s.EndTime.Date <= shiftOptions.EndTime.Value.Date);
        }

        // Simplified search implementation
        if (!string.IsNullOrWhiteSpace(shiftOptions.Search))
        {
            query = query.Where(s =>
                s.WorkerId.ToString().Contains(shiftOptions.Search)
                || s.LocationId.ToString().Contains(shiftOptions.Search)
                || EF.Functions.Like(s.Location.Name, $"%{shiftOptions.Search}%")
                || EF.Functions.Like(s.Location.TownOrCity, $"%{shiftOptions.Search}%")
                || EF.Functions.Like(s.Location.StateOrCounty, $"%{shiftOptions.Search}%")
                || EF.Functions.Like(s.Location.Country, $"%{shiftOptions.Search}%")
                || s.StartTime.ToString().Contains(shiftOptions.Search)
                || s.EndTime.ToString().Contains(shiftOptions.Search)
            );
        }

        // Apply sorting
        if (!string.IsNullOrEmpty(shiftOptions.SortBy))
        {
            var sortBy = shiftOptions.SortBy.ToLowerInvariant();
            var sortOrder = shiftOptions.SortOrder?.ToLowerInvariant() ?? "ASC";

            query = sortBy switch
            {
                "shiftid" => sortOrder == "asc"
                    ? query.OrderBy(s => s.ShiftId)
                    : query.OrderByDescending(s => s.ShiftId),
                "starttime" => sortOrder == "asc"
                    ? query.OrderBy(s => s.StartTime)
                    : query.OrderByDescending(s => s.StartTime),
                "endtime" => sortOrder == "asc"
                    ? query.OrderBy(s => s.EndTime)
                    : query.OrderByDescending(s => s.EndTime),
                "workerid" => sortOrder == "asc"
                    ? query.OrderBy(s => s.WorkerId)
                    : query.OrderByDescending(s => s.WorkerId),
                "locationid" => sortOrder == "asc"
                    ? query.OrderBy(s => s.LocationId)
                    : query.OrderByDescending(s => s.LocationId),
                "locationname" => sortOrder == "asc"
                    ? query.OrderBy(s => s.Location.Name)
                    : query.OrderByDescending(s => s.Location.Name),
                _ => sortOrder == "asc"
                    ? query.OrderBy(s => s.ShiftId)
                    : query.OrderByDescending(s => s.ShiftId),
            };
        }

        // Execute query and get results
        var shifts = (await query.ToListAsync()).Cast<Shifts?>().ToList();

        if (shifts.Count == 0)
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

        AnsiConsole.MarkupLine($"[green]Successfully retrieved {shifts.Count} shifts.[/]");
        return new ApiResponseDto<List<Shifts?>>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.OK,
            Message = "Shifts retrieved successfully.",
            Data = shifts,
        };
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
                Data = [],
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
                Data = [],
            };
        }
    }

    public async Task<ApiResponseDto<Shifts>> CreateShift(ShiftApiRequestDto shift)
    {
        try
        {
            Shifts newShift = new()
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
