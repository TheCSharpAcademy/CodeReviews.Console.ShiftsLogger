using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkerShiftsAPI.DTOs;
using WorkerShiftsAPI.Models;

namespace WorkerShiftsAPI.Controllers
{
    [Route("api/shifts")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly WorkerShiftContext _context;

        public ShiftController(WorkerShiftContext context)
        {
            _context = context;
        }

        // GET: api/shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftDTO>>> GetShifts()
        {
            return await _context.Shifts
                .Include(s => s.Worker)
                .Select(s => ShiftToDTO(s))
                .ToListAsync();
        }

        // GET: api/shifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDTO>> GetShift(int id)
        {
            var shift = await _context.Shifts
                .Include(s => s.Worker)
                .Select(s => ShiftToDTO(s))
                .FirstOrDefaultAsync(s => s.ShiftId == id);

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }

        // PUT: api/shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShift(int id, ShiftDTO shiftDTO)
        {
            if (id != shiftDTO.ShiftId)
            {
                return BadRequest();
            }

            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            shift.ShiftId = shiftDTO.ShiftId;
            shift.StartTime = shiftDTO.StartTime;
            shift.EndTime = shiftDTO.EndTime;
            shift.WorkerId = shiftDTO.WorkerId;

            _context.Entry(shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ShiftExists(id))
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

        // POST: api/shifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftDTO>> PostShift(ShiftDTO shiftDTO)
        {
            var worker = await _context.Workers.FindAsync(shiftDTO.WorkerId);
            if (worker == null)
            {
                return BadRequest("Invalid worker ID");
            }

            var shift = new Shift
            {
                StartTime = shiftDTO.StartTime,
                EndTime = shiftDTO.EndTime,
                WorkerId = shiftDTO.WorkerId
            };
            
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            shiftDTO.ShiftId = shift.ShiftId;
            shiftDTO.WorkerName = worker.Name;

            return CreatedAtAction(nameof(GetShift), new { id = shiftDTO.ShiftId }, ShiftToDTO(shift));
        }

        // DELETE: api/shifts/5
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

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.ShiftId == id);
        }

        internal static ShiftDTO ShiftToDTO(Shift shift) =>
        new()
        {
            ShiftId = shift.ShiftId,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            WorkerId = shift.WorkerId,
            WorkerName = shift.Worker.Name
        };
    }
}
