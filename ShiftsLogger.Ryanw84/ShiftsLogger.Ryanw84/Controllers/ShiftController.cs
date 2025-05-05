using Microsoft.AspNetCore.Mvc;

using ShiftsLogger.Ryanw84.Dtos;
using ShiftsLogger.Ryanw84.Models;
using ShiftsLogger.Ryanw84.Services;

namespace ShiftsLogger.Ryanw84.Controllers;
[Route("shift")]
[ApiController]
// Example: http:localhost:5009/shift
public class ShiftController: ControllerBase
    {
    private readonly IShiftService _shiftService;

    public ShiftController(IShiftService shiftService)
        {
        _shiftService = shiftService;
        }

    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<List<Shift>>>> GetAllShifts(ShiftOptions shiftOptions)
        {
        try
            {
            var shifts = await _shiftService.GetAllShifts(shiftOptions);
            return Ok(shifts);
            } catch(Exception ex)
            {
            // Handle exception (e.g., log it) and return an appropriate response
            return StatusCode(500 , new { Message = "An error occurred while retrieving shifts." , Details = ex.Message });
            }
        }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<Shift>>> GetShiftById(int id)
        {
        var result = await _shiftService.GetShiftById(id);

        if(result == null)
            {
            return NotFound();
            }

        return Ok(result);
        }

    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<Shift>>> CreateShift(ShiftApiRequestDto shift)
        {
        if(!ModelState.IsValid)
            {
            return BadRequest(ModelState);
            }

        var createdShift = await _shiftService.CreateShift(shift);

        return new ObjectResult(createdShift) { StatusCode = 201 };
        }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponseDto<Shift>>> UpdateFlight(int id , ShiftApiRequestDto updatedShift)
        {
        if(!ModelState.IsValid)
            {
            return BadRequest(ModelState);
            }

        var result = await _shiftService.UpdateShift(id , updatedShift);

        if(result == null)
            {
            return NotFound();
            }

        return Ok(result);
        }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponseDto<string>>> DeleteShift(int id)
        {
        var result = await _shiftService.DeleteShift(id);

        if(result == null)
            {
            return NotFound();
            }

        return NoContent();
        }
    }

