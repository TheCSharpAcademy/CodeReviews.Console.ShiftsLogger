using Microsoft.AspNetCore.Mvc;
using ShiftLoggerAPI.Models;
using ShiftLoggerAPI.Services;

namespace ShiftLoggerAPI.Controllers;

[Route("api/shifts")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ShiftsController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpPost]
    public async Task<ActionResult<Shift>> PostShift(Shift shift)
    {
        await _shiftService.CreateShift(shift);
        return CreatedAtAction(nameof(GetShiftById), new { id = shift.Id }, shift);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShifts()
    {
        var shifts = await _shiftService.GetShifts();
        return Ok(shifts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
        var shift = await _shiftService.GetShiftById(id);
        return Ok(shift);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShifts(int id, Shift updatedShift)
    {
        await _shiftService.UpdateShift(id, updatedShift);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        if (id <= 0) return BadRequest();
        await _shiftService.DeleteShift(id);
        return NoContent();
    }
}