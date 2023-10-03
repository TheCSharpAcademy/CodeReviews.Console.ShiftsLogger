using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebAPI.Models;

namespace ShiftsLoggerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly ShiftContext _context;

        public ShiftController(ShiftContext context)
        {
            _context = context;
        }

        // GET: api/Shift
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
        {
          if (_context.Shifts == null)
          {
              return NotFound();
          }
            return await _context.Shifts.ToListAsync();
        }

        // GET: api/Shift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> GetShiftModel(int id)
        {
          if (_context.Shifts == null)
          {
              return NotFound();
          }
            var shiftModel = await _context.Shifts.FindAsync(id);

            if (shiftModel == null)
            {
                return NotFound();
            }

            return shiftModel;
        }

        // PUT: api/Shift/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftModel(int id, Shift shiftModel)
        {
            if (id != shiftModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(shiftModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftModelExists(id))
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

        // POST: api/Shift
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shift>> PostShiftModel(Shift shiftModel)
        {
          if (_context.Shifts == null)
          {
              return Problem("Entity set 'ShiftContext.Shifts'  is null.");
          }
            _context.Shifts.Add(shiftModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShiftModel", new { id = shiftModel.Id }, shiftModel);
        }

        // DELETE: api/Shift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftModel(int id)
        {
            if (_context.Shifts == null)
            {
                return NotFound();
            }
            var shiftModel = await _context.Shifts.FindAsync(id);
            if (shiftModel == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shiftModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftModelExists(int id)
        {
            return (_context.Shifts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
