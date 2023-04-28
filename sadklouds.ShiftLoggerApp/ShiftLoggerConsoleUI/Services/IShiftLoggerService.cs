using ShiftLoggerConsoleUI.Models;

namespace ShiftLoggerConsoleUI.Services;

internal interface IShiftLoggerService
{
    public Task<List<DisplayShiftDto>> GetShifts();
    public Task<DisplayShiftDto> GetShiftsById(int id);
    public Task<string> AddShift(DateTime shiftStart, DateTime shiftEnd);
    public Task<string> UpdateShift(int id, DateTime shiftStart, DateTime shiftEnd);
    public Task<string> DeleteShift(int id);
}
