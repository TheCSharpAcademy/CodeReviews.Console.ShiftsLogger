using ShiftsLogger.Dejmenek.API.Models;

namespace ShiftsLogger.Dejmenek.API.Data.Interfaces;

public interface IShiftsRepository
{
    public Task<List<ShiftReadDTO>> GetShiftsAsync();
    public Task AddShiftAsync(ShiftCreateDTO shift);
    public Task<int> UpdateShiftAsync(int shiftId, ShiftUpdateDTO shift);
    public Task<int> DeleteShiftAsync(int shiftId);
}
