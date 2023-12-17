using ShiftsLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI
{
    internal class ShiftsLoggerService
    {
        private readonly ShiftContext _context;

        public ShiftsLoggerService(ShiftContext context)
        {
            _context = context;
        }
        internal async Task AddShift(ShiftModel newShift)
        {
            await _context.Shifts.AddAsync(newShift);
            await _context.SaveChangesAsync();
        }

        internal async Task DeleteShift()
        {
            throw new NotImplementedException();
        }

        internal async Task GetShiftById()
        {
            throw new NotImplementedException();
        }

        internal async Task GetShifts()
        {
            throw new NotImplementedException();
        }

        internal async Task UpdateShift()
        {
            throw new NotImplementedException();
        }
    }
}
