using STUDY.ASP.ShiftLoggerTryThree.Data;
using Microsoft.EntityFrameworkCore;
using STUDY.ASP.ShiftLoggerTryThree.Models;

namespace STUDY.ASP.ShiftLoggerTryThree.Services
{
    public class ShiftLoggerService : IShiftLoggerService
    {
        private readonly DataContext _context;
        public ShiftLoggerService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<ShiftLogger>> AddShift(ShiftLogger shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return await _context.Shifts.ToListAsync();
        }
        public async Task<List<ShiftLogger>> GetAllShiftLogs()
        {
            var shifts = await _context.Shifts.ToListAsync();   
            return shifts;
        }
        public async Task<ShiftLogger?> GetSingleShiftLog(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift is null)
                return null;

            return shift;
        }
        public async Task<List<ShiftLogger>?> UpdateShift(int id, ShiftLogger request)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift is null)
                return null;

            shift.EmployeeFirstName = request.EmployeeFirstName;
            shift.EmployeeLastName = request.EmployeeLastName;
            shift.ClockIn = request.ClockIn;
            shift.ClockOut = request.ClockOut;

            await _context.SaveChangesAsync();

            return await _context.Shifts.ToListAsync();
        }
        public async Task<List<ShiftLogger>?> DeleteShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift is null)
                return null;

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return await _context.Shifts.ToListAsync();
        }
    }
}
    