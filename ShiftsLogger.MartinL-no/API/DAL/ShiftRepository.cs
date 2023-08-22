using API.Models;

namespace API.Services;

public class ShiftRepository : IShiftRepository
{
    private ShiftsContext _context;

    public ShiftRepository(ShiftsContext context)
    {
        _context = context;
    }

    public IEnumerable<Shift> GetShifts()
    {
        return _context.Shifts.ToList();
    }

    public Shift GetShift(int id)
    {
        var employee = _context.Shifts.Find(id);

        return employee;
    }

    public bool AddShift(Shift shift)
    {
        _context.Shifts.Add(shift);
        var changes = _context.SaveChanges();

        return changes > 0;
    }

    public bool DeleteShift(int id)
    {
        var shift = _context.Shifts.Find(id);
        _context.Remove(shift);
        var changes = _context.SaveChanges();

        return changes > 0;
    }

    public bool UpdateShift(int id, Shift newShift)
    {
        var shift = _context.Shifts.Find(id);
        if (shift == null) return false;

        shift.StartTime = newShift.StartTime;
        shift.EndTime = newShift.EndTime;

        var changes = _context.SaveChanges();

        return changes > 0;
    }
}
