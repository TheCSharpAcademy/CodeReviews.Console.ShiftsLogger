using SharedLibrary.Models;

namespace ShiftsLoggerAPI.Interfaces;

public interface IShiftService
{
    public List<Shift> GetAllShifts();
    public Shift GetShift(int id);
    public Shift CreateShift(Shift shift);
    public Shift UpdateShift(Shift shift);
    public bool DeleteShift(Shift shift);
}

