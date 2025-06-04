using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.DAL;
using ShiftsLogger.DAL.Entities;
using ShiftsLogger.Model;
using ShiftsLogger.Repository.Common;

namespace ShiftsLogger.Repository;
public class ShiftRepository : IShiftRepository
{
	private readonly ShiftsContext _context;
	private readonly IMapper _mapper;

	public ShiftRepository(ShiftsContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	public async Task<ApiResponse<List<Shift>>> GetAllAsync()
	{
		var response = new ApiResponse<List<Shift>>();
		try
		{
			var shifts = await _context.Shifts
					.Where(s => s.IsActive)
					.OrderBy(s => s.DateCreated)
					.ProjectTo<Shift>(_mapper.ConfigurationProvider)
					.ToListAsync();

			if (shifts.Count == 0)
			{
				response.Message = "No shifts found!";
				response.Success = false;
			}
			else
			{
				response.Data = _mapper.Map<List<Shift>>(shifts);
				response.Success = true;
			}
		}
		catch (Exception ex)
		{
			response.Message = $"Error in ShiftRepository {nameof(GetAllAsync)}: {ex.Message}";
			response.Success = false;
		}
		return response;

	}

	public async Task<ApiResponse<Shift>> GetByIdAsync(Guid id)
	{
		var response = new ApiResponse<Shift>();
		try
		{
			var shift = await _context.Shifts
					.Where(s => s.IsActive)
					.ProjectTo<Shift>(_mapper.ConfigurationProvider)
					.SingleOrDefaultAsync(s => s.Id == id);

			if (shift is null)
			{
				response.Message = $"Shift with Id {id} doesn't exist";
				response.Success = false;
			}
			else
			{
				response.Data = shift;
				response.Success = true;
			}
		}
		catch (Exception ex)
		{
			response.Message = $"Error in ShiftRepository {nameof(GetByIdAsync)}: {ex.Message}";
			response.Success = false;
		}
		return response;
	}
	public async Task<ApiResponse<Shift>> CreateAsync(Shift shift)
	{
		var response = new ApiResponse<Shift>();
		try
		{
			var shiftEntity = _mapper.Map<ShiftEntity>(shift);
			_context.Add(shiftEntity);
			await _context.SaveChangesAsync();

			response.Data = shift;
			response.Success = true;
		}
		catch (Exception ex)
		{
			response.Message = $"Error in ShiftRepository {nameof(CreateAsync)}: {ex.Message}";
			response.Success = false;
		}
		return response;
	}

	public async Task<ApiResponse<Shift>> DeleteAsync(Shift shift)
	{
		var response = new ApiResponse<Shift>();
		try
		{
			var shiftEntity = _mapper.Map<ShiftEntity>(shift);
			_context.Update(shiftEntity);
			await _context.SaveChangesAsync();

			response.Message = "Succesfully deleted!";
			response.Success = true;
		}
		catch (Exception ex)
		{
			response.Message = $"Error in ShiftRepository {nameof(DeleteAsync)}: {ex.Message}";
			response.Success = false;
		}
		return response;
	}


	public async Task<ApiResponse<Shift>> UpdateAsync(Shift shift)
	{
		var response = new ApiResponse<Shift>();
		try
		{
			var shiftEntity = _mapper.Map<ShiftEntity>(shift);
			_context.Update(shiftEntity);
			await _context.SaveChangesAsync();

			response.Message = "Successfully updated!";
			response.Success = true;
		}
		catch (Exception ex)
		{
			response.Message = $"Error in ShiftRepository {nameof(UpdateAsync)}: {ex.Message}";
			response.Success = false;
		}
		return response;
	}

	public async Task<ApiResponse<List<Shift>>> CreateManyAsync(List<Shift> shifts)
	{
		var response = new ApiResponse<List<Shift>>();
		try
		{
			var shiftEntities = _mapper.Map<List<ShiftEntity>>(shifts);
			_context.AddRange(shiftEntities);
			await _context.SaveChangesAsync();

			response.Data = shifts;
			response.Success = true;
		}
		catch (Exception ex)
		{
			response.Message = $"Error in ShiftRepository {nameof(CreateManyAsync)}: {ex.Message}";
			response.Success = false;
		}
		return response;
	}

	public async Task<ApiResponse<List<User>>> GetUsersByShiftIdAsync(Guid id)
	{
		var response = new ApiResponse<List<User>>();
		try
		{
			var users = await _context.Shifts
					.Where(s => s.IsActive && s.Id == id)
					.SelectMany(s => s.Users.Where(u => u.IsActive))
					.OrderBy(u => u.DateCreated)
					.ProjectTo<User>(_mapper.ConfigurationProvider)
					.ToListAsync();

			if (users.Count == 0)
			{
				response.Message = $"No users found!";
				response.Success = false;
			}
			else
			{
				response.Data = users;
				response.Success = true;
			}
		}
		catch (Exception ex)
		{
			response.Message = $"Error in ShiftRepository {nameof(GetUsersByShiftIdAsync)}: {ex.Message}";
			response.Success = false;
		}
		return response;
	}

	public async Task<ApiResponse<Shift>> UpdateUsersAsync(Guid id, List<User> users)
	{
		var response = new ApiResponse<Shift>();

		try
		{
			var shiftEntity = await _context.Shifts
					.Include(s => s.Users.Where(u => u.IsActive))
					.Where(s => s.IsActive)
					.SingleOrDefaultAsync(s => s.Id == id);

			if (shiftEntity is null)
			{
				response.Message = $"Shift with Id {id} doesn't exist";
				response.Success = false;
				return response;
			}

			var usersToRemove = shiftEntity.Users
					.Where(ue => !users.Any(u => u.Id == ue.Id))
					.ToList();

			var usersToAdd = users
					.Where(u => !shiftEntity.Users.Any(ue => ue.Id == u.Id))
					.ToList();

			if (usersToRemove.Count != 0)
			{
				foreach (var user in usersToRemove)
				{
					shiftEntity.Users.Remove(user);
				}
			}

			if (usersToAdd.Count != 0)
			{
				var userEntitiesToAdd = _mapper.Map<List<UserEntity>>(usersToAdd);
				_context.Users.AttachRange(userEntitiesToAdd);
				shiftEntity.Users.AddRange(userEntitiesToAdd);
			}

			if (usersToRemove.Count == 0 && usersToAdd.Count == 0)
			{
				response.Message = "No changes to update!";
				response.Success = false;
			}
			else
			{
				await _context.SaveChangesAsync();

				response.Message = "Successfuly updated!";
				response.Success = true;
			}
		}
		catch (Exception ex)
		{
			response.Message = $"Error in ShiftRepository {nameof(UpdateUsersAsync)}: {ex.Message}";
			response.Success = false;
		}
		return response;
	}

}
