using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftApi.DTOs;
using ShiftApi.Models;
using ShiftApi.Service;

namespace ShiftApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly ShiftService _shiftService;
    public ShiftController(ShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ShiftDTO>>> GetAllShifts()
    {
        var shifts = await _shiftService.GetAllShifts();
        return Ok(shifts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShiftDTO>> GetShift(int id)
    {
        var shift = await _shiftService.GetById(id);

        if (shift is null)
            return NotFound();

        return Ok(shift);
    }

    [HttpPost]
    public async Task<ActionResult> PostShift(Shift shift)
    {
        await _shiftService.Post(shift);

        return CreatedAtAction(nameof(GetShift), new { id = shift.ShiftId }, shift);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShift(int id, ShiftDTO shiftDTO)
    {
        var shift = await _shiftService.FindAsync(id);

        if (id != shiftDTO.ShiftId)
        {
            return BadRequest();
        }

        try
        {
            await _shiftService.Put(id, shiftDTO);
        }
        catch (Exception ex)
        {
            if (shift is null || !_shiftService.ShiftExists(shift))
            {
                return NotFound();
            }
            else
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var shift = await _shiftService.FindAsync(id);

        if (shift is null)
            return NotFound();

        await _shiftService.Delete(shift);

        return NoContent();
    }
}
