using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.frockett.API.DTOs;
using ShiftsLogger.frockett.API.Services;

namespace ShiftsLogger.frockett.API.Controllers;

[Route("api/shifts")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly ShiftService shiftService;
    public ShiftsController(ShiftService shiftService)
    {
        this.shiftService = shiftService;
    }

    // POST: api/shifts
    [HttpPost]
    public async Task<ActionResult<ShiftDto>> AddShift([FromBody] ShiftCreateDto shiftCreateDto)
    {
        try
        {
            var shiftDto = await shiftService.AddShiftAsync(shiftCreateDto);
            // Assuming AddShiftAsync returns the added shift DTO
            return CreatedAtAction(nameof(GetShiftById), new { id = shiftDto.Id }, shiftDto);
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately
            return StatusCode(500, ex.Message);
        }
    }

    // GET: api/shifts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShiftDto>>> GetAllShifts()
    {
        var shifts = await shiftService.GetAllShiftsAsync();
        return Ok(shifts);
    }

    // GET: api/shifts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ShiftDto>>> GetShiftById(int id)
    {
        var shifts = await shiftService.GetShiftsByEmployeeIdAsync(id);
        if (shifts == null)
        {
            return NotFound();
        }
        return Ok(shifts);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteShift(int id)
    {
        bool result = await shiftService.DeleteShiftAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
