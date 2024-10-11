using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.API.Contracts;
using ShiftsLogger.API.Data;
using ShiftsLogger.API.Models.Shifts;

namespace ShiftsLogger.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ShiftsController : ControllerBase
  {
    private readonly IShiftServices _services;
    public ShiftsController(IShiftServices services)
    {
      _services = services;
    }

    [HttpGet]
    public async Task<IActionResult> GetShifts()
    {
      try
      {
        var shifts = await _services.GetAllShiftsAsync();

        return Ok(shifts);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddShift(Shift shift)
    {
      try
      {
        await _services.AddShiftAsync(shift);

        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("edit/{shiftId}")]
    public async Task<IActionResult> EditShift(int shiftId, ShiftEditDto updatedShift)
    {
      try
      {
        await _services.UpdateShiftAsync(shiftId, updatedShift);

        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
      try
      {
        await _services.DeleteShiftAsync(id);

        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

  }
}
