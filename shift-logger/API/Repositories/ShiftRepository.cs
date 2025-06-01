using API.Data;
using API.Models;
using API.Services;

namespace API.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly ShiftsDbContext _context;

    public ShiftRepository(ShiftsDbContext context)
    {
        _context = context;
    }

    public async Task Add(Shift shift)
    {
        await _context.Shifts.AddAsync(shift);
        await SaveChanges();
    }

    public async Task Delete(int id)
    {
        var shiftToDelete = _context.Shifts.FirstOrDefault(shift => shift.Id == id);
        if (shiftToDelete == null)
            return;
        _context.Shifts.Remove(shiftToDelete);
        await SaveChanges();
    }

    public IQueryable<Shift> GetAll()
    {
        return _context.Shifts;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task Update(Shift shift)
    {
        var shiftToUpdate = _context.Shifts.
            FirstOrDefault(x => x.Id == shift.Id);
        if (shiftToUpdate == null)
            return;

        shiftToUpdate.EmployeeName = shift.EmployeeName;
        shiftToUpdate.Start = shift.Start;
        shiftToUpdate.End = shift.End;
        shiftToUpdate.Duration = shift.Duration;
        await _context.SaveChangesAsync();
    }
}