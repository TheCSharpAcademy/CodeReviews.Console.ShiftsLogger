using ShiftsLogger.API.Models;
using System.Data;

namespace ShiftsLogger.API.Services;

public class ShiftDbService
{
    private readonly ApplicationDbContext _context;

    public ShiftDbService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Shift> GetAll()
    {
        List<Shift> shifts = _context.Shifts.ToList();
        return shifts;
    }

    public Shift Get(int id)
    {
        var shift = _context.Shifts.FirstOrDefault(x => x.Id == id);
        return shift;
    }

    public void Add(Shift shift)
    {
        _context.Shifts.Add(shift);
        _context.SaveChanges();
    }

    public void Update(Shift shift)
    {
        _context.Shifts.Update(shift);
        _context.SaveChanges();
    }
}
