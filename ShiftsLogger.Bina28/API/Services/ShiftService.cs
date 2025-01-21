using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Bina28.Data;
using ShiftsLogger.Bina28.Dtos;
using ShiftsLogger.Bina28.Models;
using System;
using System.Net;

namespace ShiftsLogger.Bina28.Services;

public class ShiftService : IShiftService{

	private readonly ShiftsDbContext _dbContext;
	public ShiftService(ShiftsDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public async Task<ApiResponseDto<Shift>> Create(Shift shift)
	{
		var savedShift = await _dbContext.Shifts.AddAsync(shift);
		await _dbContext.SaveChangesAsync();
		return new ApiResponseDto<Shift>
		{
			Data = savedShift.Entity,
			ResponseCode = HttpStatusCode.Created
		};

	}

	public async Task<ApiResponseDto<string?>> Delete(int id)
	{
		var savedShift =await _dbContext.Shifts.FindAsync(id);
		if (savedShift == null)
		{
			return new ApiResponseDto<string?>
			{
				RequestFailed = true,
				Data = null,
				ResponseCode = HttpStatusCode.NotFound,
				ErrorMessage = $"Resource with id: {id} was not found."
			};
		}
		_dbContext.Shifts.Remove(savedShift);
		await _dbContext.SaveChangesAsync();

		return new ApiResponseDto<string?>
		{
			Data = $"Successfully deleted shift with id: {id}.",
			ResponseCode = HttpStatusCode.NoContent

		};
	}

	public async Task<ApiResponseDto<List<Shift>>> GetAll(FilterOptions filterOptions)
	{
	
		// Base query
		var query = _dbContext.Shifts.AsQueryable();

		// Get total count of shifts (before pagination)
		var totalShifts = await query.CountAsync();

		// Apply pagination
		var shifts = await query
			.Skip((filterOptions.PageNumber - 1) * filterOptions.PageSize)
			.Take(filterOptions.PageSize)
			.ToListAsync();

		bool hasPrevious = filterOptions.PageNumber > 1;
		bool hasNext = (filterOptions.PageNumber * filterOptions.PageSize) < totalShifts;
		// Return paginated result
		return new ApiResponseDto<List<Shift>>
		{
			Data = shifts,
			ResponseCode = HttpStatusCode.OK,
			TotalCount = totalShifts,
			CurrentPage = filterOptions.PageNumber,
			PageSize = filterOptions.PageSize,
			HasPrevious = hasPrevious,
			HasNext = hasNext,

		};
	}

	public async Task<ApiResponseDto<Shift?>> GetById(int id)
	{
		var result = await _dbContext.Shifts.FindAsync(id);
		if (result == null)
		{
			return new ApiResponseDto<Shift?>
			{
				RequestFailed = true,
				Data = null,
				ResponseCode = HttpStatusCode.NotFound,
				ErrorMessage = $"Resource with id: {id} was not found."
			};
		}
		return new ApiResponseDto<Shift?>
		{
			Data = result,
			ResponseCode = HttpStatusCode.OK
		};
	}

	public async Task<ApiResponseDto<Shift?>> Update(int id, Shift updatedShift)
	{
		var result = await _dbContext.Shifts.FindAsync(id);
		if (result == null)
		{
			return new ApiResponseDto<Shift?>
			{
				RequestFailed = true,
				Data = null,
				ResponseCode = HttpStatusCode.NotFound,
				ErrorMessage = $"Resource with id: {id} was not found."
			};
		}

		result.EmployeeId = updatedShift.EmployeeId;
		result.StartTime = updatedShift.StartTime;
		result.EndTime = updatedShift.EndTime;
		result.ShiftType = updatedShift.ShiftType;
		result.Notes = updatedShift.Notes;

		await _dbContext.SaveChangesAsync();
		return new ApiResponseDto<Shift?>
		{
			Data = result,
			ResponseCode = HttpStatusCode.OK

		};
	}

		public static void Initialize(ShiftsDbContext context)
	{
		
		context.Database.EnsureCreated();

		
		if (context.Shifts.Any())
		{
			return; 
		}

		
		var shiftFaker = new ShiftFaker();
		var shifts = shiftFaker.GenerateShifts(100); // Generate 10 shifts

		context.Shifts.AddRange(shifts);
		context.SaveChanges();
	}
	public static void SeedDatabase(IServiceProvider serviceProvider)
	{
		// Create a scope for resolving services
		using (var scope = serviceProvider.CreateScope())
		{
			var dbContext = scope.ServiceProvider.GetRequiredService<ShiftsDbContext>();

			// Ensure that the Shifts table is seeded with data if it's empty
			Initialize(dbContext);
		}
	}
	}
