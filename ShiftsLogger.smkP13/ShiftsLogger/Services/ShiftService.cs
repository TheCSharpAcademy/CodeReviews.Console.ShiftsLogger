using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShiftWebApi.Data;
using ShiftWebApi.Models;

namespace ShiftWebApi.Services;
public interface IShiftService
{
    public List<Shift> GetAllShifts();
    public Shift? GetShiftById(int id);
    public Shift? CreateShift(Shift shift);
    public Shift? UpdateShift(int id, Shift UpdatedShift);
    public string? DeleteShift(int id);
    List<Shift>? GetShiftsByUserId(int id);
}

public class ShiftService : IShiftService
{
    private readonly ShiftDbContext _dbContext;
    public ShiftService(ShiftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Shift? CreateShift(Shift shift)
    {
        bool userExists = _dbContext.Users.Any(x => x.UserId == shift.UserId);
        if (userExists)
        {
            EntityEntry<Shift> newShift = _dbContext.Add(shift);
            _dbContext.SaveChanges();
            return newShift.Entity;
        }
        return null;
    }

    public Shift? UpdateShift(int id, Shift updatedShift)
    {
        Shift? shiftToUpdate = _dbContext.Shifts.Find(id);
        if (shiftToUpdate == null) return null;
        _dbContext.Entry(shiftToUpdate).CurrentValues.SetValues(updatedShift);
        _dbContext.SaveChanges();
        return shiftToUpdate;
    }

    public string? DeleteShift(int id)
    {
        Shift? shiftToDelete = _dbContext.Shifts.Find(id);
        if (shiftToDelete == null) return null;
        _dbContext.Shifts.Remove(shiftToDelete);
        _dbContext.SaveChanges();
        return $"Successfully deleted shift with id: {id}";
    }

    public List<Shift> GetAllShifts()
    {
        List<Shift> shifts = _dbContext.Shifts.OrderBy(x => x.StartTime).ToList();
        foreach (Shift shift in shifts)
        {
            shift.StartTime = DateTime.Parse(shift.StartTime.ToString("yyyy.MM.dd HH:mm:ss"));
            shift.EndTime = DateTime.Parse(shift.EndTime.ToString("yyyy.MM.dd HH:mm:ss"));
        }
        return shifts;
    }

    public Shift? GetShiftById(int id)
    {
        Shift? shift = _dbContext.Shifts.Find(id);
        return shift == null ? null : shift;
    }

    public List<Shift>? GetShiftsByUserId(int id)
    {
        List<Shift> shifts = GetAllShifts().FindAll(x => x.UserId == id);
        return shifts.Count == 0 ? null : shifts;
    }
}
