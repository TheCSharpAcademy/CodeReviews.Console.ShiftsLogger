using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.ukpagrace.Models;

namespace ShiftsLogger.ukpagrace.Controllers
{
    [Route("api/ShiftLogs")]
    [ApiController]
    public class ShiftLogsController : ControllerBase
    {
        private readonly ShiftLogContext _context;

        public ShiftLogsController(ShiftLogContext context)
        {
            _context = context;
        }

        // GET: api/ShiftLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftLogDTO>>> GetShiftLog()
        {
            return await _context.ShiftLog
                .Select(x =>LogDTO(x))
                .ToListAsync();
        }

        // GET: api/ShiftLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftLogDTO>> GetShiftLog(long id)
        {
            var shiftLog = await _context.ShiftLog.FindAsync(id);

            if (shiftLog == null)
            {
                return NotFound();
            }

            return LogDTO(shiftLog);
        }

        // PUT: api/ShiftLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftLog(long id, ShiftLogDTO shiftLogDTO)
        {
            if (id != shiftLogDTO.Id)
            {
                return BadRequest();
            }
            var shiftLog = await _context.ShiftLog.FindAsync(id);
            if (shiftLog == null)
            {
                return NotFound();
            }

            shiftLog.EmployeeId = shiftLogDTO.EmployeeId;
            shiftLog.StartTime = shiftLogDTO.StartTime;
            shiftLog.EndTime = shiftLogDTO.EndTime;
            shiftLog.Duration = shiftLogDTO.Duration;
            shiftLog.Comment = shiftLogDTO.Comment;

            _context.Entry(shiftLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftLogExists(id))
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

        // POST: api/ShiftLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftLog>> PostShiftLog(ShiftLogDTO shiftLogDTO)
        {
            var shiftLogItem = new ShiftLog
            {
                EmployeeId = shiftLogDTO.EmployeeId,
                StartTime = shiftLogDTO.StartTime,
                EndTime = shiftLogDTO.EndTime,
                Duration = shiftLogDTO.Duration,
                Comment = shiftLogDTO.Comment,
            };

            _context.ShiftLog.Add(shiftLogItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShiftLog), new { id = shiftLogItem.Id }, shiftLogItem);
        }

        // DELETE: api/ShiftLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftLog(long id)
        {
            var shiftLog = await _context.ShiftLog.FindAsync(id);
            if (shiftLog == null)
            {
                return NotFound();
            }

            _context.ShiftLog.Remove(shiftLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftLogExists(long id)
        {
            return _context.ShiftLog.Any(e => e.Id == id);
        }

        private static ShiftLogDTO LogDTO(ShiftLog shiftLog)
        {
            return new ShiftLogDTO
            {
                Id = shiftLog.Id,
                EmployeeId = shiftLog.EmployeeId,
                StartTime = shiftLog.StartTime,
                EndTime = shiftLog.EndTime,
                Duration = shiftLog.Duration,
                Comment = shiftLog.Comment
            };
        }
    }
}
