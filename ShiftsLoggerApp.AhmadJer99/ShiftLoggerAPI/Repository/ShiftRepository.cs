using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interfaces;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Repository;

internal class ShiftRepository : IShiftRepository
{
    private readonly ShitftsLoggerDbContext _context;

    public ShiftRepository(ShitftsLoggerDbContext context)
    {
        _context = context;
    }
    public async Task<Shift> CreateShiftAsync(Shift shift)
    {
        var newShift = await _context.Shifts.AddAsync(shift);
        _context.SaveChanges();
        return newShift.Entity;
    }

    public async Task<string> DeleteShiftAsync(int id)
    {
        var shift = await _context.Shifts.Where(s => s.ShiftId == id).FirstOrDefaultAsync();

        if (shift == null)
            return null;

        _context.Remove(shift);
        await _context.SaveChangesAsync();
        return $"Successfully deleted shift with id: {id}";
    }

    public async Task<List<Shift>> FindEmpShiftsAsync(int empId)
    {
        var shifts = await _context.Shifts.AsNoTracking().Where(s => s.EmpId == empId).ToListAsync();

        if (shifts == null)
            return null;

        return shifts;
    }

    public async Task<Shift> FindShiftAsync(int id)
    {
        var shift = await _context.Shifts.AsNoTracking().Where(s => s.ShiftId == id).FirstOrDefaultAsync();

        if (shift == null)
            return null;

        return shift;
    }

    public async Task<ICollection<Shift>> GetShiftsAsync()
    {
        return await _context.Shifts.AsNoTracking().ToListAsync();
    }


    public async Task<Shift> UpdateShiftAsync(int id, Shift updatedShift)
    {
        var shift = await _context.Shifts.Where(s => s.ShiftId == id).FirstOrDefaultAsync();

        if (shift == null)
            return null;

        if (updatedShift.StartDateTime != default)
            shift.StartDateTime = updatedShift.StartDateTime;

        if (updatedShift.EndDateTime != default)
            shift.EndDateTime = updatedShift.EndDateTime;

        if (updatedShift.EmpId != 0)
            shift.EmpId = updatedShift.EmpId;

        await _context.SaveChangesAsync();
        return shift;
    }

}
