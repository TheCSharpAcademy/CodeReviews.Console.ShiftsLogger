using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Services;

public class ShiftService(ShiftsDbContext dbContext, IMapper mapper) : IShiftService
{
    public async Task<ApiResponseDto<List<Shift>>> GetAllShifts(ShiftFilterOptions filterOptions)
    {
        var shifts = await dbContext.Shifts.ToListAsync();
        var query = dbContext.Shifts.AsQueryable(); // Allow for filtering

        if (!string.IsNullOrEmpty(filterOptions.WorkerId.ToString()))
		{
			query = query.Where(s => s.WorkerId.ToString() == filterOptions.WorkerId.ToString());
		}
		if (filterOptions.StartTime != null)
		{
			query = query.Where(s => s.StartTime <= filterOptions.StartTime);
		}
		if (filterOptions.EndTime != null)
		{
			query = query.Where(s => s.EndTime <=filterOptions.EndTime);
		}
		if (filterOptions.LocationId != null)
		{
			query = query.Where(s => s.LocationId.ToString() == filterOptions.LocationId.ToString());
		}
		shifts = await query.ToListAsync();
		// Check if any shifts were found

		if (shifts is null || shifts.Count == 0)
        {
            return new ApiResponseDto<List<Shift>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = "No shifts found.",
            };
        }

        return new ApiResponseDto<List<Shift>>
        {
            Data = shifts,
            Message = "Shifts retrieved successfully",
            ResponseCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<ApiResponseDto<Shift?>> GetShiftById(int id)
    {
        Shift? shift = await dbContext.Shifts.FindAsync(id);
        if (shift is null)
        {
            return new ApiResponseDto<Shift?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Shift with ID: {id} not found.",
            };
        }
        return new ApiResponseDto<Shift?>
        {
            Data = shift,
            Message = "Shift retrieved successfully",
            ResponseCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<ApiResponseDto<Shift>> CreateShift(ShiftApiRequestDto shift)
    {
        Shift newShift = mapper.Map<Shift>(shift); // Map the DTO to the Shift entity
        var savedShift = await dbContext.Shifts.AddAsync(newShift);
        await dbContext.SaveChangesAsync();

        AnsiConsole.MarkupLine(
            $"\n[green]Successfully created shift with ID: {savedShift.Entity.ShiftId}[/]"
        );
        return new ApiResponseDto<Shift>
        {
            Data = savedShift.Entity,
            Message = "Shift created successfully",
            ResponseCode = System.Net.HttpStatusCode.Created,
        };
    }

    public async Task<ApiResponseDto<Shift?>> UpdateShift(int id, ShiftApiRequestDto updatedShift)
    {
        Shift? savedShift = await dbContext.Shifts.FindAsync(id);

        if (savedShift is null)
        {
            return new ApiResponseDto<Shift?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Shift with ID: {id} not found.",
            };
        }

        // Map the updated properties from the DTO to the existing entity
        mapper.Map(updatedShift, savedShift);
        savedShift.ShiftId = id; // Ensure the ID is set correctly

        dbContext.Shifts.Update(savedShift);

        await dbContext.SaveChangesAsync();

        return new ApiResponseDto<Shift?>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.OK,
            Message = $"Shift with ID: {id} updated successfully.",
            Data = savedShift,
        };
    }

    public async Task<ApiResponseDto<string?>> DeleteShift(int id)
    {
        Shift? savedShift = await dbContext.Shifts.FindAsync(id);

        if (savedShift == null)
        {
            return new ApiResponseDto<string?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Shift with ID: {id} not found.",
            };
        }

        dbContext.Shifts.Remove(savedShift);
        await dbContext.SaveChangesAsync();

        return new ApiResponseDto<string?>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.NoContent,
            Message = $"Shift with ID: {id} deleted successfully.",
        };
    }
}
