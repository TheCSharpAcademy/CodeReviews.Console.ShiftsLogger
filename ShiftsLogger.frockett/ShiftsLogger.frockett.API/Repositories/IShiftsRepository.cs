using ShiftsLogger.frockett.API.Models;

namespace ShiftsLogger.frockett.API.Repositories;

public interface IShiftsRepository
{
    Task<Shift> AddShiftAsync(Shift shift);
    Task<IEnumerable<Shift>> GetAllShiftsAsync();
    Task<Shift> GetShiftByIdAsync(int shiftId);
    Task<IEnumerable<Shift>> GetShiftsByEmployeeIdAsync(int employeeId);
    Task DeleteShiftAsync(int shiftId);
    Task<Shift> UpdateShiftAsync(Shift shift);
}
