using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLoggerAPI.DataAccess;
using ShiftLoggerAPI.Models;

namespace ShiftLoggerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class ShiftLoggerController : ControllerBase
    {
        private readonly ShiftLoggerContext _context;

        public ShiftLoggerController(ShiftLoggerContext context)
        {
            _context = context;
        }

        // GET: api/ShiftLoggers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftLogger>>> GetShifts()
        {
          if (_context.Shifts == null)
          {
              return NotFound();
          }
            return await _context.Shifts.ToListAsync();
        }

        // GET: api/ShiftLoggers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftLogger>> GetShiftLogger(int id)
        {
          if (_context.Shifts == null)
          {
              return NotFound();
          }
            var shiftLogger = await _context.Shifts.FindAsync(id);

            if (shiftLogger == null)
            {
                return NotFound();
            }

            return shiftLogger;
        }

        // PUT: api/ShiftLoggers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftLogger(int id, ShiftLogger shiftLogger)
        {
            if (id != shiftLogger.Id)
            {
                return BadRequest();
            }

            _context.Entry(shiftLogger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftLoggerExists(id))
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

        // POST: api/ShiftLoggers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftLogger>> PostShiftLogger(ShiftLogger shiftLogger)
        {
          if (_context.Shifts == null)
          {
              return Problem("Entity set 'ShiftLoggerContext.Shifts'  is null.");
          }
            _context.Shifts.Add(shiftLogger);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShiftLogger), new { id = shiftLogger.Id }, shiftLogger);
        }

        // DELETE: api/ShiftLoggers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftLogger(int id)
        {
            if (_context.Shifts == null)
            {
                return NotFound();
            }
            var shiftLogger = await _context.Shifts.FindAsync(id);
            if (shiftLogger == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shiftLogger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftLoggerExists(int id)
        {
            return (_context.Shifts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
