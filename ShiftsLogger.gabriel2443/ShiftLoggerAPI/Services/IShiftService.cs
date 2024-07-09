using ShiftLoggerAPI.Models;

namespace ShiftLoggerAPI.Services;

public interface IShiftService
{
    Task CreateShift(Shift shift);

    Task<List<Shift>> GetShifts();

    Task<Shift> GetShiftById(int id);

    Task UpdateShift(int id, Shift updatedShift);

    Task DeleteShift(int id);
}