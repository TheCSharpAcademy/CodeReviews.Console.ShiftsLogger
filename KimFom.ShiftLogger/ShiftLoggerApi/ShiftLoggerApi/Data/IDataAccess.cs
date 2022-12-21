using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Data;

public interface IDataAccess
{
    public Task<List<Shift>> GetShiftsAsync();
    public Task<Shift> GetShiftByIdAsync(int id);
    public Task AddShiftAsync(Shift shift);
    public Task UpdateShiftAsync(int id, Shift shift);
    public Task DeleteShiftAsync(int id);
}