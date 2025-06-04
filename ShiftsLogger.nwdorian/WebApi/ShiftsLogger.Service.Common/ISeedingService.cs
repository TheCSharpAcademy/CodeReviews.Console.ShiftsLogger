using ShiftsLogger.Model;

namespace ShiftsLogger.Service.Common;
public interface ISeedingService
{
	Task<ApiResponse<string>> SeedDataAsync();
}
