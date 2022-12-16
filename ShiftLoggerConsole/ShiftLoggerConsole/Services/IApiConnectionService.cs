using ShiftLoggerConsole.Models;

namespace ShiftLoggerConsole.Services;

public interface IApiConnectionService
{
    public Task<List<Shift>?> GetAllShifts();
    public Task<Shift?> GetShiftById(int id);
    public Task AddShift(Shift shift);
    public Task UpdateShift(int id, Shift? shift);
    public Task DeleteShift(int id);
}