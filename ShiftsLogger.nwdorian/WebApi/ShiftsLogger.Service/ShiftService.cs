using ShiftsLogger.Model;
using ShiftsLogger.Repository.Common;
using ShiftsLogger.Service.Common;

namespace ShiftsLogger.Service;
public class ShiftService : IShiftService
{
	private readonly IShiftRepository _shiftRepository;

	public ShiftService(IShiftRepository shiftRepository)
	{
		_shiftRepository = shiftRepository;
	}
	public async Task<ApiResponse<List<Shift>>> GetAllAsync()
	{
		return await _shiftRepository.GetAllAsync();
	}

	public async Task<ApiResponse<Shift>> GetByIdAsync(Guid id)
	{
		return await _shiftRepository.GetByIdAsync(id);
	}

	public async Task<ApiResponse<Shift>> CreateAsync(Shift shift)
	{
		shift.Id = Guid.NewGuid();
		shift.IsActive = true;
		shift.DateCreated = DateTime.Now;
		shift.DateUpdated = DateTime.Now;

		return await _shiftRepository.CreateAsync(shift);
	}

	public async Task<ApiResponse<Shift>> DeleteAsync(Guid id)
	{
		var response = await _shiftRepository.GetByIdAsync(id);

		if (!response.Success || response.Data is null)
		{
			return response;
		}

		var shift = response.Data;

		shift.IsActive = false;

		return await _shiftRepository.DeleteAsync(shift);
	}


	public async Task<ApiResponse<Shift>> UpdateAsync(Guid id, Shift shift)
	{
		var response = await _shiftRepository.GetByIdAsync(id);

		if (!response.Success || response.Data is null)
		{
			return response;
		}

		var existingShift = response.Data;

		if (shift.StartTime != DateTime.MinValue)
		{
			existingShift.StartTime = shift.StartTime;
		}

		if (shift.EndTime != DateTime.MinValue)
		{
			existingShift.EndTime = shift.EndTime;
		}

		existingShift.DateUpdated = DateTime.Now;

		return await _shiftRepository.UpdateAsync(existingShift);
	}

	public async Task<ApiResponse<Shift>> UpdateUsersAsync(Guid id, List<User> users)
	{
		var response = await _shiftRepository.GetByIdAsync(id);
		if (!response.Success)
		{
			return response;
		}

		return await _shiftRepository.UpdateUsersAsync(id, users);
	}

	public async Task<ApiResponse<List<User>>> GetUsersByShiftIdAsync(Guid id)
	{
		var response = new ApiResponse<List<User>>();
		var getByIdResponse = await _shiftRepository.GetByIdAsync(id);
		if (!getByIdResponse.Success)
		{
			response.Message = getByIdResponse.Message;
			response.Success = false;
			return response;
		}

		response = await _shiftRepository.GetUsersByShiftIdAsync(id);
		return response;
	}
}
