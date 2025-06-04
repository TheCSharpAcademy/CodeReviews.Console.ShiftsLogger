using ShiftsLogger.Model;

namespace ShiftsLogger.Service.Common;
public interface IUserService
{
	Task<ApiResponse<List<User>>> GetAllAsync();
	Task<ApiResponse<User>> GetByIdAsync(Guid id);
	Task<ApiResponse<User>> CreateAsync(User user);
	Task<ApiResponse<User>> DeleteAsync(Guid id);
	Task<ApiResponse<User>> UpdateAsync(Guid id, User user);
	Task<ApiResponse<User>> UpdateShiftsAsync(Guid id, List<Shift> shifts);
	Task<ApiResponse<List<Shift>>> GetShiftsByUserIdAsync(Guid id);
}
