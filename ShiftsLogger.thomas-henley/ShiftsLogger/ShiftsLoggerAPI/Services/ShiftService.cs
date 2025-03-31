using ShiftsLoggerAPI.Data;
using ShiftsLoggerModels;

namespace ShiftsLoggerAPI.Services;

public class ShiftService(ShiftsDbContext context) : IShiftService
{
    public IEnumerable<Shift> GetShifts()
    {
        return context.Shifts;
    }

    public IEnumerable<Shift> GetShiftsByEmployeeId(int empId)
    {
        return context.Shifts.Where(x => x.EmployeeId == empId);
    }

    public Shift? GetShiftById(int id)
    {
        var shift = context.Shifts.Find(id);

        return shift;
    }

    public Shift AddShift(Shift shift)
    {
        var dbShift = context.Shifts.Add(shift);
        context.SaveChanges();
        
        return dbShift.Entity;
    }

    public bool DeleteShift(int id)
    {
        var shift = context.Shifts.Find(id);

        if (shift is null) return false;
        
        context.Shifts.Remove(shift);
        context.SaveChanges();
        
        return true;
    }

    public Shift? UpdateShift(Shift shift)
    {
        var dbShift = context.Shifts.Find(shift.Id);
        if (dbShift is null) return null;
        
        context.Entry(dbShift).CurrentValues.SetValues(shift);
        context.SaveChanges();
        
        return shift;
    }
}