using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.DAL;
using ShiftsLogger.DAL.Entities;
using ShiftsLogger.Model;
using ShiftsLogger.Repository.Common;

namespace ShiftsLogger.Repository;
public class SeedingRepository : ISeedingRepository
{
	private readonly ShiftsContext _context;
	private readonly IMapper _mapper;

	public SeedingRepository(ShiftsContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	public async Task<ApiResponse<string>> AddUsersAndShiftsAsync(List<User> users, List<Shift> shifts)
	{
		var response = new ApiResponse<string>();
		try
		{
			var userEntities = _mapper.Map<List<UserEntity>>(users);
			var shiftEntities = _mapper.Map<List<ShiftEntity>>(shifts);

			foreach (var user in userEntities)
			{
				user.Shifts.AddRange(shiftEntities);
			}
			_context.Users.AddRange(userEntities);
			await _context.SaveChangesAsync();

			response.Message = "Database seeded successfully!";
			response.Success = true;
		}
		catch (Exception ex)
		{
			response.Message = $"Error in SeedingRepository AddUsersAndShiftsAsync: {ex.Message}";
			response.Success = false;
		}
		return response;
	}

	public async Task<bool> RecordsExistAsync()
	{
		try
		{
			if (await _context.Users.AnyAsync() || await _context.Shifts.AnyAsync())
			{
				return true;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}
}
