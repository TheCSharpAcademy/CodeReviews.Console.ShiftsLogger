using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShiftLogger.Data;
using ShiftLogger.Models;

namespace ShiftLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly ShiftLoggerContext _context;
        public ShiftController(ShiftLoggerContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Shift>> Get()
            => await _context.Shifts.ToListAsync();

        [HttpGet("open")]
        [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShiftOpen(bool shiftOpen = true)
        {
            var shifts = await _context.Shifts.Where(s => s.ShiftOpen == shiftOpen).ToListAsync();

            if (shifts == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(shifts);
            }
        }


        [HttpGet("id")]
        [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByID(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            return shift == null ? NotFound() : Ok(shift);
        }


        [HttpGet("byemployee/{employeeId}")]
        [ProducesResponseType(typeof(IEnumerable<Shift>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEmployeeId(int employeeId)
        {
            // Find the shifts matching the employee ID
            var shifts = await _context.Shifts
                .Where(s => s.EmployeeId == employeeId)
                .ToListAsync();

            // Check if any shifts were found
            if (shifts.Count == 0)
            {
                // No shifts found for the given employee ID, return an empty response
                return Ok(shifts);
            }
            else
            {
                // Return the shifts found for the given employee ID
                return Ok(shifts);
            }
        }


        [HttpGet("byemployee/{employeeId}/{shiftOpen}")]
        [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmployeeIdAndShiftOpen(int employeeId, bool shiftOpen)
        {
            // Find the first shift matching the employee ID and shift open status
            var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.ShiftOpen == shiftOpen);

            // Check if a shift was found
            if (shift == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(shift);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Shift shift)
        {
            await _context.Shifts.AddAsync(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByID), new { id = shift.Id }, shift);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Shift shift)
        {
            if (id != shift.Id) return BadRequest();

            _context.Update(shift);
            await _context.SaveChangesAsync();

            var updatedShift = await _context.Shifts.FindAsync(id);

            return Ok(updatedShift);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var issueToDelete = await _context.Shifts.FindAsync(id);
            if (issueToDelete == null) return NotFound();

            _context.Shifts.Remove(issueToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
