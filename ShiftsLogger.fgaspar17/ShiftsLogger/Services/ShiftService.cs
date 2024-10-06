using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.Services
{

    public class ShiftService
    {
        public readonly ShiftContext _context;

        public ShiftService(ShiftContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShiftDto>> GetShifts()
        {
            return await _context.Shifts
                .Include(s => s.Employee) // Include the related Employee entity
                .Select(s => ShiftMapper.MapToDto(s))
                .ToListAsync();
        }

        public async Task<ShiftDto> GetShiftById(int id)
        {
            var shift = await _context.Shifts
                .Include(s => s.Employee)
                .Where(s => s.ShiftId == id)
                .FirstOrDefaultAsync();

            return ShiftMapper.MapToDto(shift);
        }

        public async Task UpdateShift(Shift shift)
        {
            try
            {
                shift.Employee = await _context.Employees.Where(e => e.EmployeeId == shift.EmployeeId).FirstOrDefaultAsync();

                _context.Employees.Attach(shift.Employee);

                _context.Entry(shift).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InsertShift(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShiftById(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
        }
    }
}