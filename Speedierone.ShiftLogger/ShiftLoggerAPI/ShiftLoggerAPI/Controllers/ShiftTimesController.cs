using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLoggerAPI.Models;

namespace ShiftLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftTimesController : ControllerBase
    {
        private readonly TodoContext _context;

        public ShiftTimesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/ShiftTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftTimes>>> GetShiftTimes()
        {
          if (_context.ShiftTimes == null)
          {
              return NotFound();
          }
            return await _context.ShiftTimes.ToListAsync();
        }

        // GET: api/ShiftTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftTimes>> GetShiftTimes(long id)
        {
          if (_context.ShiftTimes == null)
          {
              return NotFound();
          }
            var shiftTimes = await _context.ShiftTimes.FindAsync(id);

            if (shiftTimes == null)
            {
                return NotFound();
            }

            return shiftTimes;
        }

        // PUT: api/ShiftTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftTimes(long id, ShiftTimes shiftTimes)
        {
            if (id != shiftTimes.Id)
            {
                return BadRequest();
            }

            _context.Entry(shiftTimes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftTimesExists(id))
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

        // POST: api/ShiftTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftTimes>> PostShiftTimes(ShiftTimes shiftTimes)
        {
          _context.ShiftTimes.Add(shiftTimes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShiftTimes", new { id = shiftTimes.Id }, shiftTimes);
        }

        // DELETE: api/ShiftTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftTimes(long id)
        {
            if (_context.ShiftTimes == null)
            {
                return NotFound();
            }
            var shiftTimes = await _context.ShiftTimes.FindAsync(id);
            if (shiftTimes == null)
            {
                return NotFound();
            }

            _context.ShiftTimes.Remove(shiftTimes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftTimesExists(long id)
        {
            return (_context.ShiftTimes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
