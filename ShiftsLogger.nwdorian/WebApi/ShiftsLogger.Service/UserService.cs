using ShiftsLogger.Model;
using ShiftsLogger.Repository.Common;
using ShiftsLogger.Service.Common;

namespace ShiftsLogger.Service;
public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<ApiResponse<List<User>>> GetAllAsync()
	{
		return await _userRepository.GetAllAsync();
	}

	public async Task<ApiResponse<User>> GetByIdAsync(Guid id)
	{
		return await _userRepository.GetByIdAsync(id);
	}

	public async Task<ApiResponse<User>> CreateAsync(User user)
	{
		user.Id = Guid.NewGuid();
		user.IsActive = true;
		user.DateCreated = DateTime.Now;
		user.DateUpdated = DateTime.Now;

		return await _userRepository.CreateAsync(user);
	}

	public async Task<ApiResponse<User>> DeleteAsync(Guid id)
	{
		var response = await _userRepository.GetByIdAsync(id);

		if (!response.Success || response.Data is null)
		{
			return response;
		}

		var user = response.Data;

		user.IsActive = false;

		return await _userRepository.DeleteAsync(user);
	}

	public async Task<ApiResponse<User>> UpdateAsync(Guid id, User user)
	{
		var response = await _userRepository.GetByIdAsync(id);

		if (!response.Success || response.Data is null)
		{
			return response;
		}

		var existingUser = response.Data;

		if (!string.IsNullOrWhiteSpace(user.FirstName))
		{
			existingUser.FirstName = user.FirstName;
		}

		if (!string.IsNullOrWhiteSpace(user.LastName))
		{
			existingUser.LastName = user.LastName;
		}

		if (!string.IsNullOrWhiteSpace(user.Email))
		{
			existingUser.Email = user.Email;
		}

		existingUser.DateUpdated = DateTime.Now;

		return await _userRepository.UpdateAsync(existingUser);
	}

	public async Task<ApiResponse<User>> UpdateShiftsAsync(Guid id, List<Shift> shifts)
	{
		var response = await _userRepository.GetByIdAsync(id);

		if (!response.Success || response.Data is null)
		{
			return response;
		}

		return await _userRepository.UpdateShiftsAsync(id, shifts);
	}

	public async Task<ApiResponse<List<Shift>>> GetShiftsByUserIdAsync(Guid id)
	{
		var response = new ApiResponse<List<Shift>>();
		var getByIdResponse = await _userRepository.GetByIdAsync(id);
		if (!getByIdResponse.Success)
		{
			response.Message = getByIdResponse.Message;
			response.Success = false;
			return response;
		}

		response = await _userRepository.GetShiftsByUserIdAsync(id);
		return response;

	}
}
