using ShiftsLogger.SpyrosZoupas.DAL.Model;

namespace ShiftsLogger.SpyrosZoupas.Services;

public interface IShiftService
{
    public Shift CreateShift(Shift shift);
    public Shift? UpdateShift(Shift shift);
    public string? DeleteShift(int id);
    public Shift? GetShiftById(int id);
    public List<Shift> GetAllShifts();
}