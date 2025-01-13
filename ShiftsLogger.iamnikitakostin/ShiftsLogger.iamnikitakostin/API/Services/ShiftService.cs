using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Services;

public class ShiftService : IShiftService
{
    private readonly ShiftsDbContext _context;

    public ShiftService(ShiftsDbContext context)
    {
        _context = context;
    }

    public Shift Add(Shift shift)
    {
        var savedShift = _context.Add(shift);
        _context.SaveChanges();
        return savedShift.Entity;
    }

    public bool Update(Shift shift)
    {
        Shift? savedShift = GetById(shift.Id);

        if (savedShift == null) {
            return false;
        }

        savedShift.WorkerId = shift.WorkerId;
        savedShift.StartTime = shift.StartTime;
        savedShift.EndTime = shift.EndTime;
        savedShift.ShiftDuration = shift.ShiftDuration;

        _context.SaveChanges();
        return true;

    }

    public bool Delete(int id) {
        Shift? savedShift = GetById(id);

        if (savedShift == null)
        {
            return false;
        }

        _context.Shifts.Remove(savedShift);
        _context.SaveChanges();

        return true;
    }

    public List<Shift> GetAll()
    {
        return _context.Shifts.ToList();
    }

    public Shift? GetById(int id)
    {
        Shift? savedShift = _context.Shifts.FirstOrDefault(s => s.Id == id);
        return savedShift;
    }

    public List<Shift> GetAllByWorkerId(int workerid) {
        return _context.Shifts.Where(s => s.WorkerId == workerid).ToList();
    }

    public Shift? GetLatest()
    {
        return _context.Shifts.OrderBy(s => s.Id).LastOrDefault();
    }
}
