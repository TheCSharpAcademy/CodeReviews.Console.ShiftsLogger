using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerApi.Models;
using ShiftsLoggerApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        // GET: api/Shift
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftModel>>> GetShiftModels()
        {
            var shifts = await _shiftService.GetShiftsAsync();
            return Ok(shifts);
        }

        // GET: api/Shift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftModel>> GetShiftModel(int id)
        {
            var shiftModel = await _shiftService.GetShiftByIdAsync(id);

            if (shiftModel == null)
            {
                return NotFound();
            }

            return Ok(shiftModel);
        }

        // PUT: api/Shift/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftModel(int id, ShiftModel shiftModel)
        {
            if (id != shiftModel.Id)
            {
                return BadRequest();
            }

            try
            {
                await _shiftService.UpdateShiftAsync(id, shiftModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _shiftService.GetShiftByIdAsync(id) == null)
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
        [HttpPost]
        public async Task<ActionResult<ShiftModel>> PostShiftModel(ShiftModel shiftModel)
        {
            await _shiftService.CreateShiftAsync(shiftModel);
            return CreatedAtAction("GetShiftModel", new { id = shiftModel.Id }, shiftModel);
        }

        // DELETE: api/Shift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftModel(int id)
        {
            var shiftModel = await _shiftService.GetShiftByIdAsync(id);
            if (shiftModel == null)
            {
                return NotFound();
            }

            await _shiftService.DeleteShiftAsync(id);
            return NoContent();
        }
    }
}
