using ShiftsLogger.Dejmenek.UI.Models;

namespace ShiftsLogger.Dejmenek.UI.Services.Interfaces;
public interface IShiftService
{
    Task AddShift();
    Task DeleteShift();
    Task UpdateShift();
    Task<List<ShiftReadDTO>> GetAllShifts();
}
