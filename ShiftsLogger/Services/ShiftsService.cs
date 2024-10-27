using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Services
{
    public interface IShiftsService
    {
        Task<bool> AddShiftAsync(ShiftCreate shift);
        Task<bool> RemoveShiftAsync(int shiftID);
        Task<bool> UpdateShiftAsync(ShiftUpdate newShift);
        Task<List<Shift>> GetShiftsAsync(int workerID);
    }
    public class ShiftsService : IShiftsService
    {
        private readonly ShiftsLoggerContext _ctx;

        // Inject DbContext through Dependency Injection
        public ShiftsService(ShiftsLoggerContext context)
        {
            _ctx = context;
        }
        public async Task<bool> AddShiftAsync(ShiftCreate shift)
        {
           
                if (_ctx.Workers.Find(shift.WorkerId) is null || _ctx.Workers.IsNullOrEmpty())
                    return false;
                _ctx.Shifts.Add(new Shift(shift));
                await _ctx.SaveChangesAsync();
                return true;
            
        }

        public async Task<List<Shift>> GetShiftsAsync(int workerID)
        {
            
                return await _ctx.Shifts.Where(shift=>shift.WorkerId == workerID).ToListAsync();
            
        }

        public async Task<bool> RemoveShiftAsync(int shiftID)
        {
           
                Shift? shift = await _ctx.Shifts.FindAsync(shiftID);
            if (shift is null)
                return false;
                _ctx.Shifts.Remove(shift);
                await _ctx.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> UpdateShiftAsync(ShiftUpdate newShift)
        {
           
                var shift = await _ctx.Shifts.FindAsync(newShift.Id);
                if (shift is null)
                    return false;
            if (newShift.Start is not null && !newShift.Start.Equals(DateTime.MinValue) && newShift.Start < DateTime.Now)
                shift.Start = (DateTime)newShift.Start;
            if (newShift.End is not null && !newShift.End.Equals(DateTime.MinValue) && newShift.End < DateTime.Now)
                shift.End = (DateTime)newShift.End;

            await _ctx.SaveChangesAsync();
                return true; 
            
        }
    }
}
