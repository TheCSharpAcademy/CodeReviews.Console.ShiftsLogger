using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerWebAPI.Models;
using ShiftsLoggerWebAPI.Services;

namespace ShiftsLoggerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
	private readonly IShiftService _shiftService;

	public ShiftsController(IShiftService shiftService)
	{
		_shiftService = shiftService;
	}

	// GET: api/Shifts
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
	{
		var shifts = await _shiftService.GetAllShiftsAsync();
		return Ok(shifts);
	}

	// GET: api/Shifts/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Shift>> GetShift(long id)
	{
		var shift = await _shiftService.GetShiftByIdAsync(id);

		if (shift == null)
		{
			return NotFound();
		}

		return Ok(shift);
	}

	// PUT: api/Shifts/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public async Task<IActionResult> PutShift(long id, Shift shift)
	{
		if (id != shift.Id)
		{
			return BadRequest();
		}

		try
		{
			await _shiftService.UpdateShiftAsync(id, shift);
		}
		catch (KeyNotFoundException)
		{
			return NotFound();
		}

		return NoContent();
	}

	// POST: api/Shifts
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<Shift>> PostShift(Shift shift)
	{
		var createdShift = await _shiftService.CreateShiftAsync(shift);
		return CreatedAtAction(nameof(GetShift), new { id = createdShift.Id }, createdShift);
	}

	// DELETE: api/Shifts/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteShift(long id)
	{
		try
		{
			await _shiftService.DeleteShiftAsync(id);
			return NoContent();
		}
		catch (KeyNotFoundException)
		{
			return NotFound();
		}
	}
}

