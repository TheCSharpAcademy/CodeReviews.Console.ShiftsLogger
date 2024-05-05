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

        // GET: ShiftLogger/id/5
        [HttpGet("id/{id}")]
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

        // GET: ShiftLogger/name/employeeName
        [HttpGet("name/{employeeName}")]
        public async Task<ActionResult<IEnumerable<DataModel.ShiftLogger>>> GetShiftLogger(string employeeName)
        {
            if (_context.ShiftLoggers == null)
            {
                return NotFound();
            }

            var shiftLoggers = await _context.ShiftLoggers.Where(x => x.EmployeeName == employeeName).ToListAsync();

            if (shiftLoggers == null)
            {
                return NotFound();
            }

            return shiftLoggers;
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

        // DELETE: ShiftLogger/name
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteShiftLoggersByName(string name)
        {
            var shiftLoggers = await _context.ShiftLoggers.Where(x => x.EmployeeName == name).ToListAsync();

            if (shiftLoggers == null || !shiftLoggers.Any())
            {
                return NotFound();
            }

            _context.ShiftLoggers.RemoveRange(shiftLoggers);
            await _context.SaveChangesAsync();

            return Ok(shiftLoggers);
        }

        // PUT: ShiftLogger/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, DataModel.ShiftLogger shiftLogger)
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

            return Ok(shiftLogger);
        }

        private bool ShiftLoggerExists(long id)
        {
            return (_context.ShiftLoggers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
