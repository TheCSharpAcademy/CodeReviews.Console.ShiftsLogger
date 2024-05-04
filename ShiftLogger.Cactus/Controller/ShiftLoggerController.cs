using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLogger.Cactus.DataModel;

namespace ShiftLogger.Cactus.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ShiftLoggerController : ControllerBase
    {
        private readonly ShiftLoggerContext _context;

        public ShiftLoggerController(ShiftLoggerContext context)
        {
            _context = context;
        }

        // GET: ShiftLogger
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModel.ShiftLogger>>> GetShiftLoggers()
        {
            if (_context.ShiftLoggers == null || _context.ShiftLoggers.Count() == 0)
            {
                return NotFound();
            }
            return await _context.ShiftLoggers.ToListAsync();
        }

        // GET: ShiftLogger/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModel.ShiftLogger>> GetShiftLogger(long id)
        {
            if (_context.ShiftLoggers == null)
            {
                return NotFound();
            }
            var shiftLogger = await _context.ShiftLoggers.FindAsync(id);

            if (shiftLogger == null)
            {
                return NotFound();
            }

            return shiftLogger;
        }

        // POST: ShiftLogger
        [HttpPost]
        public async Task<ActionResult<DataModel.ShiftLogger>> PostShiftLogger(DataModel.ShiftLogger shiftLogger)
        {
            if (_context.ShiftLoggers == null)
            {
                return Problem("Entity set shiftLogger  is null.");
            }
            _context.ShiftLoggers.Add(shiftLogger);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShiftLogger), new { id = shiftLogger.Id }, shiftLogger);
        }

        // DELETE: ShiftLogger/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftLogger(long id)
        {
            if (_context.ShiftLoggers == null)
            {
                return NotFound();
            }
            var todoItem = await _context.ShiftLoggers.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.ShiftLoggers.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
