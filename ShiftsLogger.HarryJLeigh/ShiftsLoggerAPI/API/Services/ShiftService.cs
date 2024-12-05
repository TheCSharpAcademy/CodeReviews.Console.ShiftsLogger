using ShiftsLogger.API.Data;
using Shared.Models;

namespace ShiftsLogger.API.Services;

public interface IShiftService
{
    public List<Shift> GetAllShifts();
    public Shift? CreateShift(Shift shift);
    public Shift? UpdateShift(Shift updateShift);
    public string? DeleteShift(int id);
}

public class ShiftService(ShiftsDbContext dbContext) : IShiftService
{
    private readonly ShiftsDbContext _context = dbContext;

    public List<Shift> GetAllShifts() => _context.Shifts.ToList();
    
    public Shift CreateShift(Shift shift)
    {
        var savedShift = _context.Shifts.Add(shift);
        _context.SaveChanges();
        return savedShift.Entity;
    }

    public Shift? UpdateShift(Shift shift)
    {
        Shift? savedShift = _context.Shifts.Find(shift.Id);
        if (savedShift == null) return null;
        _context.Entry(savedShift).CurrentValues.SetValues(shift);
        _context.SaveChanges();
        return savedShift;
    }

    public string? DeleteShift(int id)
    {
        Shift? savedShift = _context.Shifts.Find(id);
        if (savedShift == null) return null;
        _context.Shifts.Remove(savedShift);
        _context.SaveChanges();
        return $"Successfully deleted shift with id: {id}";
    }
}