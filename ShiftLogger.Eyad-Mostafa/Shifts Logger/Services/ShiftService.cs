using Shifts_Logger.Data;
using Shifts_Logger.Models;

namespace Shifts_Logger.Services;

public class ShiftService : IShiftService
{
    private readonly AppDbContext _context;

    public ShiftService(AppDbContext context)
    {
        _context = context;
    }

    public void AddShift(DateTime startTime, DateTime? endTime, int workerId)
    {
        var shift = new Shift
        {
            StartTime = startTime,
            EndTime = endTime,
            WorkerId = workerId
        };
        _context.Shifts.Add(shift);
        _context.SaveChanges();
    }
    public IEnumerable<Shift> GetShifts()
    {
        return _context.Shifts.ToList();
    }

    public Shift GetShift(int id)
    {
        return _context.Shifts.FirstOrDefault(s => s.Id == id);
    }

    public void UpdateShift(int id, DateTime startTime, DateTime? endTime, int workerId)
    {
        var shift = _context.Shifts.FirstOrDefault(s => s.Id == id);
        if (shift != null)
        {
            shift.StartTime = startTime;
            shift.EndTime = endTime;
            shift.WorkerId = workerId;
            _context.SaveChanges();
        }
    }

    public void DeleteShift(int id)
    {
        var shift = _context.Shifts.FirstOrDefault(s => s.Id == id);
        if (shift != null)
        {
            _context.Shifts.Remove(shift);
            _context.SaveChanges();
        }
    }
}