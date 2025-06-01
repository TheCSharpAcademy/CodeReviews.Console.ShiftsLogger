using HKhemanthSharma.ShiftLoggerAPI.Model;
using HKhemanthSharma.ShiftLoggerAPI.Model.Dto;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HKhemanthSharma.ShiftLoggerAPI.Services
{
    public interface IShiftRepository
    {
        Task<List<Shift>> GetAllShiftsAsync();
        Task<Shift> GetShiftByIdAsync(int Id);
        Task<Shift> CreateShift(ShiftDto shift);
        Task<Shift> UpdateShiftAsync(ShiftDto shift, int Id);
        Task<Shift> DeleteShiftAsync(int Id);
    }
    public class ShiftRepository : IShiftRepository
    {
        private readonly ShiftDbContext _context;
        public ShiftRepository(ShiftDbContext context)
        {
            _context = context;
        }
        public async Task<List<Shift>> GetAllShiftsAsync()
        {
            return await _context.Shifts.Include(x => x.Worker).ToListAsync();
        }
        public async Task<Shift> GetShiftByIdAsync(int Id)
        {
            return await _context.Shifts.FirstOrDefaultAsync(x => x.ShiftId == Id);
        }
        public async Task<Shift> CreateShift(ShiftDto shift)
        {
            string NormalisedStartTime = DateTime.Parse(shift.ShiftStartTime).ToString("HH:mm");
            string NormalisedSEndTime = DateTime.Parse(shift.ShiftEndTime).ToString("HH:mm");
            DateTime.TryParseExact(shift.ShiftDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ShiftDate);
            DateTime.TryParseExact(NormalisedStartTime, "HH:mm", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime ShiftStartTime);
            DateTime.TryParseExact(NormalisedSEndTime, "HH:mm", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime ShiftEndTime);
            Shift NewShift = new Shift
            {
                ShiftDate = ShiftDate,
                ShiftStartTime = ShiftStartTime,
                ShiftEndTime = ShiftEndTime,
                WorkerId = shift.WorkerId
            };
            var AddedEntity = await _context.Shifts.AddAsync(NewShift);
            await _context.SaveChangesAsync();
            return AddedEntity.Entity;
        }
        public async Task<Shift> UpdateShiftAsync(ShiftDto shift, int Id)
        {
            string NormalisedStartTime = DateTime.Parse(shift.ShiftStartTime).ToString("HH:mm");
            string NormalisedSEndTime = DateTime.Parse(shift.ShiftEndTime).ToString("HH:mm");
            var UpdateShift = await _context.Shifts.FirstOrDefaultAsync(x => x.ShiftId == Id);
            DateTime.TryParseExact(shift.ShiftDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ShiftDate);
            DateTime.TryParseExact(NormalisedStartTime, "HH:mm", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime ShiftStartTime);
            DateTime.TryParseExact(NormalisedSEndTime, "HH:mm", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime ShiftEndTime);
            if (UpdateShift != null)
            {
                UpdateShift.ShiftStartTime = ShiftStartTime;
                UpdateShift.ShiftEndTime = ShiftEndTime;
                UpdateShift.ShiftDate = ShiftDate;
                UpdateShift.WorkerId = shift.WorkerId;
                await _context.SaveChangesAsync();
            }
            return UpdateShift;
        }
        public async Task<Shift> DeleteShiftAsync(int Id)
        {
            var DeleteShift = await _context.Shifts.FirstOrDefaultAsync(x => x.ShiftId == Id);
            _context.Shifts.Remove(DeleteShift);
            await _context.SaveChangesAsync();
            return DeleteShift;
        }
    }
}
