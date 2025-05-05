using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Ryanw84.Dtos;
using ShiftsLogger.Ryanw84.Models;
using ShiftsLogger.Ryanw84.Services;

namespace ShiftsLogger.Ryanw84.Controllers;

[Route("shift")]
[ApiController]
// Example: http:localhost:5009/shift
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<List<Shift>>>> GetAllShifts(
        ShiftOptions shiftOptions
    )
    {
        try
        {
            var shifts = await _shiftService.GetAllShifts(shiftOptions);
            return Ok(shifts);
        }
        catch (Exception ex)
        {
            // Handle exception (e.g., log it) and return an appropriate response
            return StatusCode(
                418,
                new { Message = "Error occurred: Get all shifts", Details = ex.Message }
            );
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<Shift>>> GetShiftById(int id)
    {
        try
        {
            var result = await _shiftService.GetShiftById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(
                418,
                new { Message = "Error occured: Get shifts by ID.", Details = ex.Message }
            );
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<Shift>>> CreateShift(ShiftApiRequestDto shift)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdShift = await _shiftService.CreateShift(shift);

            return new ObjectResult(createdShift) { StatusCode = 201 };
        }
        catch (Exception ex)
        {
            return StatusCode(
                418,
                new { Message = "Error occurred: Create Shift", Details = ex.Message }
            );
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponseDto<Shift>>> UpdateShift(
        int id,
        ShiftApiRequestDto updatedShift
    )
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _shiftService.UpdateShift(id, updatedShift);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(
                418,
                new { Message = "Error occurred: Update Shift", Details = ex.Message }
            );
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponseDto<string>>> DeleteShift(int id)
    {
        try
        {
            var result = await _shiftService.DeleteShift(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(
                418,
                new { Message = "Error occurred: Delete Shift", Details = ex.Message }
            );
            throw;
        }
    }
}
