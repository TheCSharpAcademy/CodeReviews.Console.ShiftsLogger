using Buutyful.ShiftsLogger.Domain;
using Buutyful.ShiftsLogger.Domain.Contracts.Shift;
using Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;
using Microsoft.EntityFrameworkCore;


namespace Buutyful.ShiftsLogger.Api.Data;

public class ShiftDataAccess(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<ShiftResponse> AddAsync(CreateShiftRequest shiftRequest)
    {
        var worker = _context.Workers.FirstOrDefault(w => w.Id == shiftRequest.WorkerId) ??
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
}
