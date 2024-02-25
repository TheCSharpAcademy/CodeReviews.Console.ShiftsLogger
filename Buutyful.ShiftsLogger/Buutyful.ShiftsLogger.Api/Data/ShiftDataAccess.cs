using Buutyful.ShiftsLogger.Domain;
using Buutyful.ShiftsLogger.Domain.Contracts.Shift;
using Microsoft.EntityFrameworkCore;


namespace Buutyful.ShiftsLogger.Api.Data;

public class ShiftDataAccess(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<ShiftResponse> AddAsync(CreateShiftRequest shiftRequest)
    {
        var worker = await _context.Workers.FindAsync(shiftRequest.WorkerId) ??
            throw new ArgumentException("worker not found with given guid");

        var shift = Shift.Create(shiftRequest.WorkerId, shiftRequest.ShiftDay, shiftRequest.StartAt, shiftRequest.EndAt);

        await _context.Shifts.AddAsync(shift);
        await _context.SaveChangesAsync();
        
        return shift;
    }
    public async Task<List<ShiftResponse>> GetAsync()
    {
        var shifts = await _context.Shifts.ToListAsync();
        return shifts.Select(s => 
        {
            ShiftResponse res = s;
            return res;
        }).ToList();
    }
    public async Task<ShiftResponse?> GetByIdAsync(Guid shiftId)
    {
        var shift = await _context.Shifts.FindAsync(shiftId);
        if (shift is null) return null;
        return shift;
    }
    public async Task<bool> DeleteAsync(Guid shiftId)
    {
        var shift = await _context.Shifts.FindAsync(shiftId);
        if (shift is null) return false;
        _context.Shifts.Remove(shift);
        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }
}
