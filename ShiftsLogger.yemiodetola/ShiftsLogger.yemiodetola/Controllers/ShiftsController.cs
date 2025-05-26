using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShfitsLogger.yemiodetola.Contexts;
using ShfitsLogger.yemiodetola.Models;
using ShfitsLogger.yemiodetola.Services;

namespace ShfitsLogger.yemiodetola.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController : ControllerBase
{
  private readonly IShiftService _shiftService;
  public ShiftsController(IShiftService shiftService)
  {
    _shiftService = shiftService;
  }


  [HttpGet]
  public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
  {
    try
    {
      var shifts = await _shiftService.GetAllShiftsAsync();
      return Ok(shifts);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error {ex.Message}");
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Shift>> GetShift(int id)
  {
    try
    {
      var shift = await _shiftService.GetShiftByIdAsync(id);
      if (shift == null)
      {
        return NotFound();
      }
      return Ok(shift);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error {ex.Message}");
    }
  }


  [HttpPost]
  public async Task<ActionResult<Shift>> CreateShift(Shift shift)
  {
    try
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var createdshift = await _shiftService.CreateShiftAsync(shift);
      return CreatedAtAction(nameof(GetShift), new { Id = createdshift.Id }, createdshift);
    }
    catch (ArgumentException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error: {ex.Message}");
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<Shift>> UpdateShift(int id, Shift shift)
  {
    try
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var updatedShift = await _shiftService.UpdateShiftAsync(id, shift);
      if (updatedShift == null)
      {
        return NotFound();
      }
      return Ok(updatedShift);
    }
    catch (ArgumentException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error: {ex.Message}");
    }
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteShift(int id)
  {
    try
    {
      var deleted = await _shiftService.DeleteShiftAsync(id);
      if (!deleted)
      {
        return NotFound();
      }
      return NoContent();
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error: {ex.Message}");
    }
  }

}
