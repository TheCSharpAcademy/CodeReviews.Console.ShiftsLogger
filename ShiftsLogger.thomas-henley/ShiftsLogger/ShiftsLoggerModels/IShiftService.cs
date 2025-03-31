namespace ShiftsLoggerModels;

public interface IShiftService
{
    public IEnumerable<Shift> GetShifts();
    public IEnumerable<Shift> GetShiftsByEmployeeId(int empId);
    public Shift? GetShiftById(int id);
    public Shift AddShift(Shift shift);
    public bool DeleteShift(int id);
    public Shift? UpdateShift(Shift shift);
}