using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.K_MYR;

public class ShiftsService(DataContext context) : IShiftsService
{
    private readonly DataContext _context = context;

    public async Task<ShiftDTO> AddShiftAsync(ShiftInsertModel shiftDTO, ApplicationUser user)
    {
        Shift shift = new()
        {
            StartTime = shiftDTO.StartTime,
            EndTime = shiftDTO.EndTime,
            Duration = (shiftDTO.EndTime - shiftDTO.StartTime).Ticks,
            UserId = user.Id
        };

        user!.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        return shift.GetDTO();
    }

    public async Task<bool> UpdateShiftAsync(int id, ShiftInsertModel shift, ApplicationUser user)
    {
        var shiftResult = await _context.Shifts.FindAsync(id);

        if (shiftResult is null || shiftResult.UserId != user.Id)
            return false;

        shiftResult.StartTime = shift.StartTime;
        shiftResult.EndTime = shift.EndTime;
        shiftResult.Duration = (shift.EndTime - shift.StartTime).Ticks;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ShiftExists(id))
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteShiftAsync(int id, ApplicationUser user)
    {
        var shift = await _context.Shifts.FindAsync(id);

        if (shift is null || shift.UserId != user.Id)
            return false;

        _context.Remove(shift);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ShiftExists(id))
        {
            return false;
        }

        return true;
    }

    private bool ShiftExists(int id) => _context.Shifts.Any(s => s.Id == id);
}
