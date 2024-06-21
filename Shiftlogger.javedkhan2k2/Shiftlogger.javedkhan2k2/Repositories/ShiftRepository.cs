using Microsoft.EntityFrameworkCore;
using Shiftlogger.Data;
using Shiftlogger.Entities;
using Shiftlogger.Repositories.Interfaces;

namespace Shiftlogger.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly ShiftloggerDbContext _context;

    public ShiftRepository(ShiftloggerDbContext context)
    {
        _context = context;
    }

    public void AddShift(Shift shift)
    {
        _context.Add(shift);
        _context.SaveChanges();
    }

    public void DeleteShift(Shift shift)
    {
        _context.Remove(shift);
        _context.SaveChanges();
    }

    public List<Shift> GetAllShifts() => _context.Shifts.Include(w => w.Worker).ToList();

    public Shift GetShiftById(int id) => _context.Shifts.AsNoTracking().Include(w => w.Worker).FirstOrDefault(s => s.Id == id);

    public void UpdateShift(Shift shift)
    {
        _context.Attach(shift).State = EntityState.Modified;
        _context.SaveChanges();
    }
}