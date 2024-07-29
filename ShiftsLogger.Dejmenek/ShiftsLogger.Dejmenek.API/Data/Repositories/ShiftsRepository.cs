using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Dejmenek.API.Data.Interfaces;
using ShiftsLogger.Dejmenek.API.Helpers;
using ShiftsLogger.Dejmenek.API.Models;

namespace ShiftsLogger.Dejmenek.API.Data.Repositories;

public class ShiftsRepository : IShiftsRepository
{
    private readonly ShiftsContext _shiftsContext;

    public ShiftsRepository(ShiftsContext shiftsContext)
    {
        _shiftsContext = shiftsContext;
    }

    public async Task AddShiftAsync(ShiftCreateDTO shiftDto)
    {
        var shift = Mapper.FromShiftCreateDto(shiftDto);

        await _shiftsContext.Shifts.AddAsync(shift);
        await _shiftsContext.SaveChangesAsync();
    }

    public async Task<int> DeleteShiftAsync(int shiftId)
    {
        var shift = await _shiftsContext.Shifts.FirstOrDefaultAsync(s => s.Id == shiftId);

        if (shift is null)
        {
            return -1;
        }

        _shiftsContext.Shifts.Remove(shift);

        return await _shiftsContext.SaveChangesAsync();
    }

    public async Task<List<ShiftReadDTO>> GetShiftsAsync()
    {
        var shifts = await _shiftsContext.Shifts.Include(s => s.Employee).Select(s => Mapper.ToShiftReadDto(s)).ToListAsync();

        return shifts;
    }

    public async Task<int> UpdateShiftAsync(int shiftId, ShiftUpdateDTO shift)
    {
        var oldShift = await _shiftsContext.Shifts.FirstOrDefaultAsync(x => x.Id == shiftId);

        if (oldShift is null)
        {
            return -1;
        }

        oldShift.EndTime = shift.EndTime;
        oldShift.Duration = shift.Duration;

        return await _shiftsContext.SaveChangesAsync();
    }
}
