using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.DataAccess;
using ShiftsLoggerApi.Models;

namespace ShiftsLoggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly ShiftContext _context;
        private readonly ILogger<ShiftController> _logger;

        public ShiftController(ShiftContext context, ILogger<ShiftController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Shift
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftModel>>> GetShiftModels()
        {
            try
            {
                var shifts = await _context.ShiftModels.ToListAsync();
                if (shifts == null || !shifts.Any())
                {
                    return NotFound("No shifts found.");
                }
                return Ok(shifts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving shifts.");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Shift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftModel>> GetShiftModel(int id)
        {
            try
            {
                var shiftModel = await _context.ShiftModels.FindAsync(id);
                if (shiftModel == null)
                {
                    return NotFound($"Shift with ID {id} not found.");
                }
                return Ok(shiftModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving shift with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Shift/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftModel(int id, ShiftModel shiftModel)
        {
            if (id != shiftModel.Id)
            {
                return BadRequest("Shift ID mismatch.");
            }

            if (!ShiftModelExists(id))
            {
                return NotFound($"Shift with ID {id} not found.");
            }

            _context.Entry(shiftModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError("Concurrency exception occurred while updating shift.");
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the shift.");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Shift
        [HttpPost]
        public async Task<ActionResult<ShiftModel>> PostShiftModel(ShiftModel shiftModel)
        {
            try
            {
                if (shiftModel.EndOfShift < shiftModel.StartOfShift)
                {
                    return BadRequest("End shift cannot be earlier than start shift.");
                }

                _context.ShiftModels.Add(shiftModel);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetShiftModel", new { id = shiftModel.Id }, shiftModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the shift.");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Shift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftModel(int id)
        {
            try
            {
                var shiftModel = await _context.ShiftModels.FindAsync(id);
                if (shiftModel == null)
                {
                    return NotFound($"Shift with ID {id} not found.");
                }

                _context.ShiftModels.Remove(shiftModel);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the shift.");
                return StatusCode(500, "Internal server error");
            }
        }

        private bool ShiftModelExists(int id)
        {
            return _context.ShiftModels.Any(e => e.Id == id);
        }
    }
}
