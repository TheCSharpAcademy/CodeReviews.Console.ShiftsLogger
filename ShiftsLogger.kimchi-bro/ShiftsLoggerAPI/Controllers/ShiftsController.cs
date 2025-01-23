using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers
{
    [ApiController]
    [Route("api/shifts")]
    [Produces("application/json")]
    public class ShiftsController : ControllerBase
    {
        private readonly ShiftContext _context;

        public ShiftsController(ShiftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetShifts()
        {
            var shifts = await _context.Shifts.ToListAsync();
            return new { shifts };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> GetShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShift(int id, Shift shift)
        {
            if (id != shift.Id)
            {
                return BadRequest();
            }

            _context.Entry(shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostShift(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShift", new { id = shift.Id }, shift);
        }

        [HttpPost("addmock")]
        public IActionResult PostShifts([FromBody] List<Shift> shifts)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Shifts.AddRange(shifts);
                _context.SaveChanges();
                transaction.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delmock")]
        public async Task<IActionResult> DeleteShifts()
        {
            _context.Shifts.RemoveRange(_context.Shifts);
            await _context.SaveChangesAsync();
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Shifts', RESEED, 0)");

            return NoContent();
        }

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.Id == id);
        }
    }
}
