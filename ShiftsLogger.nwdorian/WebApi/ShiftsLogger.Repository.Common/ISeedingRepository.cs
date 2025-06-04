using ShiftsLogger.Model;

namespace ShiftsLogger.Repository.Common;
public interface ISeedingRepository
{
	Task<ApiResponse<string>> AddUsersAndShiftsAsync(List<User> users, List<Shift> shifts);
	Task<bool> RecordsExistAsync();
}
