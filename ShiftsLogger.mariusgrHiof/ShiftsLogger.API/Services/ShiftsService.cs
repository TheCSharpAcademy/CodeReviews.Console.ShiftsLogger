using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Data;
using ShiftsLogger.API.DTOs.Shift;
using ShiftsLogger.API.Models;

namespace ShiftsLogger.API.Services;
public class ShiftsService
{
    private readonly ApplicationDbContext _context;
    public ShiftsService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<GetShiftDto>> GetAllShiftsAsync()
    {
        var shifts = await _context.Shifts
            .Select(s => new GetShiftDto
            {
                Id = s.Id,
                Start = s.Start,
                End = s.End,
                WorkerId = s.WorkerId,
            })
            .ToListAsync();

        return shifts;
    }
    public async Task<GetShiftDto?> GetShiftByIdAsync(int id)
    {
        var shift = await _context
           .Shifts
           .FirstOrDefaultAsync(s => s.Id == id);

        if (shift == null) return null;

        // Map from Shift => GetShiftDTO
        GetShiftDto shiftDTO = new GetShiftDto
        {
            Id = shift.Id,
            Start = shift.Start,
            End = shift.End,
            WorkerId = shift.WorkerId
        };

        return shiftDTO;
    }
    public async Task<Shift?> CreateShiftAsync(AddShiftDto newShift)
    {
        if (newShift == null) return null;

        // Map from AddShiftDTO => Shift
        Shift shift = new Shift
        {
            Start = newShift.Start,
            End = newShift.End,
            WorkerId = newShift.WorkerId
        };

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        return shift;
    }
    public async Task<Shift?> UpdateShiftAsync(int id, UpdateShiftDto updateShift)
    {
        if (updateShift == null) return null;
        if (id != updateShift.Id) return null;

        var shift = await _context
            .Shifts
            .FirstOrDefaultAsync(s => s.Id == id);

        if (shift == null) return null;

        shift.Start = updateShift.Start;
        shift.End = updateShift.End;
        shift.WorkerId = updateShift.WorkerId;

        _context.Shifts.Update(shift);
        await _context.SaveChangesAsync();

        return shift;
    }
    public async Task<Shift?> DeleteShiftAsync(int id)
    {
        var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.Id == id);

        if (shift == null) return null;

        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();

        return shift;
    }
}

