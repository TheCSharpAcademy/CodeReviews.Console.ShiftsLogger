using ShiftsLoggerApi.Models;

namespace ShiftsLoggerApi.Services;

public interface IShiftService
{
    Task<IEnumerable<ShiftModel>> GetShiftsAsync();
    Task<ShiftModel> GetShiftByIdAsync(int id);
    Task<ShiftModel> CreateShiftAsync(ShiftModel shiftModel);
    Task UpdateShiftAsync(int id, ShiftModel shiftModel);
    Task DeleteShiftAsync(int id);
}