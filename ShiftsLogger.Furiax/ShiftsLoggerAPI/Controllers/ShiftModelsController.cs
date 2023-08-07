using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.DataAccess;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers
{
	[Route("api/ShiftsLogger")]
	[ApiController]
	public class ShiftModelsController : ControllerBase
	{
		private readonly ShiftsLoggerContext _context;

		public ShiftModelsController(ShiftsLoggerContext context)
		{
			_context = context;
		}

		// GET: api/ShiftModels
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ShiftModel>>> GetShifts()
		{
			if (_context.Shifts == null)
			{
				return NotFound();
			}
			return await _context.Shifts.ToListAsync();
		}

		// GET: api/ShiftModels/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ShiftModel>> GetShiftModel(int id)
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

		// PUT: api/ShiftModels/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutShiftModel(int id, ShiftModel shiftModel)
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

		// POST: api/ShiftModels
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<ShiftModel>> PostShiftModel(ShiftModel shiftModel)
		{
			if (_context.Shifts == null)
			{
				return Problem("Entity set 'ShiftsLoggerContext.Shifts'  is null.");
			}
			_context.Shifts.Add(shiftModel);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetShiftModel), new { id = shiftModel.Id }, shiftModel);
		}

		// DELETE: api/ShiftModels/5
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
