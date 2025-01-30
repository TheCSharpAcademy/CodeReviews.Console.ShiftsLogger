using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers
{
    [ApiController]
    [Route("api/shifts")]
    [Produces("application/json")]
    public class ShiftsController(ShiftsLoggerDbContext dbContext) : ControllerBase
    {
        private readonly ShiftsLoggerDbContext _dbContext = dbContext;

        [HttpGet]
        public async Task<ActionResult<Shift>> GetShifts()
        {
            var shifts = await _dbContext.Shifts
                .Include(s => s.Employee)
                .ToListAsync();
            return Ok(new { shifts });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> GetShiftById(int id)
        {
            var shift = await _dbContext.Shifts.FindAsync(id);
            if (shift is null) return NotFound();

            return Ok(shift);
        }

        [HttpPost]
        public async Task<ActionResult<Shift>> AddShift(Shift shift)
        {
            _dbContext.Shifts.Add(shift);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShiftById), new { id = shift.Id }, shift);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, Shift updateShift)
        {
            var shift = await _dbContext.Shifts.FindAsync(id);
            if (shift is null) return NotFound();

            shift.StartTime = updateShift.StartTime;
            shift.EndTime = updateShift.EndTime;
            shift.Duration = updateShift.Duration;
            shift.EmployeeId = updateShift.EmployeeId;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftById(int id)
        {
            var shift = await _dbContext.Shifts.FindAsync(id);
            if (shift is null) return NotFound();

            _dbContext.Shifts.Remove(shift);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
