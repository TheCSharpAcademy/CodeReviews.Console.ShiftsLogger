using ShiftsLoggerWebAPI.Models;

namespace ShiftsLoggerWebAPI.Services
{
	public interface IShiftService
	{
		Task<IEnumerable<Shift>> GetAllShiftsAsync();
		Task<Shift> GetShiftByIdAsync(long id);
		Task<Shift> CreateShiftAsync(Shift shift);
		Task UpdateShiftAsync(long id, Shift shift);
		Task DeleteShiftAsync(long id);
	}
}
