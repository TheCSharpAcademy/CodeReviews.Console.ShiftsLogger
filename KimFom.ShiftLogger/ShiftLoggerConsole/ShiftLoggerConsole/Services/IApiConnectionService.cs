using ShiftLoggerConsole.Dtos;
using ShiftLoggerConsole.Models;

namespace ShiftLoggerConsole.Services;

public interface IApiConnectionService
{
    public Task<List<Shift>?> GetAllShifts();
    public Task<Shift?> GetShiftById(int id);
    public Task AddShift(ShiftAddDto shift);
    public Task UpdateShift(int id, ShiftUpdateDto shift);
    public Task DeleteShift(int id);
}