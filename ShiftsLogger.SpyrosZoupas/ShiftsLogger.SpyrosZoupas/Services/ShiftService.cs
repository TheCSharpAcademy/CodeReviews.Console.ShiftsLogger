using ShiftsLogger.SpyrosZoupas.DAL;
using ShiftsLogger.SpyrosZoupas.DAL.Model;

namespace ShiftsLogger.SpyrosZoupas.Services;

public class ShiftService : IShiftService
{
    private readonly ShiftsLoggerDbContext _dbContext;

    public ShiftService(ShiftsLoggerDbContext dbContext) 
    {
        _dbContext = dbContext;
    }

    public Shift CreateShift(Shift shift)
    {
        var savedShift = _dbContext.Add(shift);
        _dbContext.SaveChanges();
        return savedShift.Entity;
    }

    public string? DeleteShift(int id)
    {
        Shift? existingShift = _dbContext.Shifts.Find(id);
        if (existingShift == null) return null;

        _dbContext.Shifts.Remove(existingShift);
        _dbContext.SaveChanges();

        return $"Successfully deleted Shift with Id of {id}";
    }

    public List<Shift> GetAllShifts() =>
        _dbContext.Shifts.ToList();

    public Shift? GetShiftById(int id) =>
        _dbContext.Shifts.Find(id);


    public Shift? UpdateShift(Shift shift)
    {
        Shift? existingShift = _dbContext.Shifts.Find(shift.ShiftId);
        if (existingShift == null) return null;

        _dbContext.Entry(existingShift).CurrentValues.SetValues(shift);
        _dbContext.SaveChanges();

        return existingShift;   
    }
}
