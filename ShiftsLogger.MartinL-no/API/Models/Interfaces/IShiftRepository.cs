namespace API.Models;

public interface IShiftRepository
{
    public IEnumerable<Shift> GetShifts();

    public Shift GetShift(int id);

    public bool AddShift(Shift shift);

    public bool DeleteShift(int id);

    public bool UpdateShift(int id, Shift newShift);
}