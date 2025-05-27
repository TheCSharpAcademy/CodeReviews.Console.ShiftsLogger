using Shifts_Logger.Models;

namespace Shifts_Logger.Services;

public interface IShiftService
{
    void AddShift(DateTime startTime, DateTime? endTime, int workerId);
    IEnumerable<Shift> GetShifts();
    Shift GetShift(int id);
    void UpdateShift(int id, DateTime startTime, DateTime? endTime, int workerId);
    void DeleteShift(int id);
}
