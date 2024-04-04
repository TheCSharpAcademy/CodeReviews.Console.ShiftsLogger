using ShiftsLogger.Dejmenek.UI.Models;

namespace ShiftsLogger.Dejmenek.UI.Data.Interfaces;
public interface IShiftsRepository
{
    Task AddShift(ShiftAddDTO shiftDto);
    Task DeleteShift(int shiftId);
    Task UpdateShift(int shiftId, ShiftUpdateDTO shiftDto);
    Task<List<Shift>?> GetAllShifts();
}
