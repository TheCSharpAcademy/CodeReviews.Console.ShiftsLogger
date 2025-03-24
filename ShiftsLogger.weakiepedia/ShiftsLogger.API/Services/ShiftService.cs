using ShiftsLogger.weakiepedia.Data;
using ShiftsLogger.weakiepedia.Models;

namespace ShiftsLogger.weakiepedia.Services;

public interface IShiftService
{
    public List<Shift>? GetAllShifts();
    public Shift? GetShiftById(int id);
    public Shift? CreateShift(Shift shift);
    public Shift? UpdateShift(int id, Shift shift);
    public string? DeleteShift(int id);
}

public class ShiftService : IShiftService
{
    private ShiftsDbContext _db;

    public ShiftService(ShiftsDbContext db)
    {
        _db = db;
    }

    public List<Shift>? GetAllShifts()
    {
        var shifts = _db.Shifts.ToList();
        
        return shifts.Any() ? shifts : null;
    }

    public Shift? GetShiftById(int id)
    {
        return _db.Shifts.Find(id);
    }

    public Shift? CreateShift(Shift shift)
    {
        if (_db.Employees.Find(shift.EmployeeId) == null)
            return null;
        
        shift.DurationInSeconds = (long)(shift.EndTime - shift.StartTime).TotalSeconds;
        
        _db.Shifts.Add(shift);
        _db.SaveChanges();
        
        return shift;
    }

    public Shift? UpdateShift(int id, Shift shift)
    {
        var savedShift = _db.Shifts.Find(id);

        if (savedShift == null || savedShift.Id != id || _db.Employees.Find(shift.EmployeeId) == null) 
            return null;
        
        shift.DurationInSeconds = (long)(shift.EndTime - shift.StartTime).TotalSeconds;
        
        _db.Entry(savedShift).CurrentValues.SetValues(shift);
        _db.SaveChanges();
        
        return savedShift;
    }

    public string? DeleteShift(int id)
    {
        var shift = _db.Shifts.Find(id);
        
        if (shift == null) 
            return null;
        
        _db.Shifts.Remove(shift);
        _db.SaveChanges();
        
        return "Successfully found and deleted the shift.";
    }
}