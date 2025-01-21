using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Bina28.Dtos;
using ShiftsLogger.Bina28.Models;
using ShiftsLogger.Bina28.Services;

namespace ShiftsLogger.Bina28.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController: ControllerBase
{
	private readonly IShiftService _shiftService;
	public ShiftsController(IShiftService shiftService)
	{
		_shiftService = shiftService;
	}

	[HttpGet]
	public async Task<ActionResult<ApiResponseDto<List<Shift>>>> GetAll(FilterOptions filterOptions)
	{
		return Ok(await _shiftService.GetAll(filterOptions));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponseDto<Shift>>> GetById(int id)
	{
		var shift = await _shiftService.GetById(id);
		if (shift == null)
		{
			return NotFound();
		}
		return Ok(shift);
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponseDto<List<Shift>>>> Create(Shift shift)
	{
		if(!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		var createdShift = await _shiftService.Create(shift);

		return new ObjectResult(createdShift) { StatusCode = 201 };
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponseDto<Shift>>> Update(int id, Shift updatedShift)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		var result = await _shiftService.Update(id, updatedShift);
		if (result == null)
		{
			return NotFound();
		}
		return Ok(result);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		var result =await  _shiftService.Delete(id);
		if (result == null)
		{
			return NotFound();
		}
		return NoContent();
	}
}
