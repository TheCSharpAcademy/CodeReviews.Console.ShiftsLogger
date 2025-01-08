using Microsoft.AspNetCore.Mvc;
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
	public ActionResult GetAll()
	{
		return Ok(_shiftService.GetAll());
	}

	[HttpGet("{id}")]
	public ActionResult<Shift> GetById(int id)
	{
		var shift = _shiftService.GetById(id);
		if (shift == null)
		{
			return NotFound();
		}
		return Ok(shift);
	}

	[HttpPost]
	public ActionResult<Shift> Create(Shift shift)
	{
		return Ok(_shiftService.Create(shift));
	}

	[HttpPut("{id}")]
	public ActionResult<Shift> Update(int id, Shift updatedShift)
	{
		var shift = _shiftService.Update(id, updatedShift);
		if (shift == null)
		{
			return NotFound();
		}
		return Ok(shift);
	}

	[HttpDelete("{id}")]
	public ActionResult<string> Delete(int id)
	{
		var result = _shiftService.Delete(id);
		if (result == null)
		{
			return NotFound();
		}
		return result;
	}
}
