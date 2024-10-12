using ShiftsLogger.API.Data;
using ShiftsLogger.API.Models.Shifts;

namespace ShiftsLogger.API.Contracts
{
  public interface IShiftServices
  {
    Task<IEnumerable<ShiftDto>> GetAllShiftsAsync();
    Task<ShiftDto> GetShiftByIdAsync(int id);
    Task<ShiftDto> GetShift(int id);
    Task AddShiftAsync(Shift shift);
    Task UpdateShiftAsync(int shiftId, ShiftEditDto shift);
    Task DeleteShiftAsync(int id);
  }
}
