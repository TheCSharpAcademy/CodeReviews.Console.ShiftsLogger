using Shiftlogger.Entities;

namespace Shiftlogger.Repositories.Interfaces;

public interface IShiftRepository
{
    List<Shift> GetAllShifts();
    Shift GetShiftById(int id);
    void AddShift(Shift shift);
    void UpdateShift(Shift shift);
    void DeleteShift(Shift shift);
}