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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftLogDto>>> GetShiftLog()
        {
            return await _context.ShiftLog
                .Select(x =>LogDto(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftLogDto>> GetShiftLog(long id)
        {
            var shiftLog = await _context.ShiftLog.FindAsync(id);

            if (shiftLog == null)
            {
                return NotFound();
            }

            return LogDto(shiftLog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftLog(long id, ShiftLogDto ShiftLogDto)
        {
            if (id != ShiftLogDto.Id)
            {
                return BadRequest();
            }
            var shiftLog = await _context.ShiftLog.FindAsync(id);
            if (shiftLog == null)
            {
                return NotFound();
            }

            shiftLog.EmployeeId = ShiftLogDto.EmployeeId;
            shiftLog.StartTime = ShiftLogDto.StartTime;
            shiftLog.EndTime = ShiftLogDto.EndTime;
            shiftLog.Duration = ShiftLogDto.Duration;
            shiftLog.Comment = ShiftLogDto.Comment;

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

        [HttpPost]
        public async Task<ActionResult<ShiftLog>> PostShiftLog(ShiftLogDto ShiftLogDto)
        {
            var shiftLogItem = new ShiftLog
            {
                EmployeeId = ShiftLogDto.EmployeeId,
                StartTime = ShiftLogDto.StartTime,
                EndTime = ShiftLogDto.EndTime,
                Duration = ShiftLogDto.Duration,
                Comment = ShiftLogDto.Comment,
            };

            _context.ShiftLog.Add(shiftLogItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShiftLog), new { id = shiftLogItem.Id }, shiftLogItem);
        }

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

        private static ShiftLogDto LogDto(ShiftLog shiftLog)
        {
            return new ShiftLogDto
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
