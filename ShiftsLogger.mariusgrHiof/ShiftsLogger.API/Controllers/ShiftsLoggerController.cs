using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.API.Data;
using ShiftsLogger.API.DTOs.Shift;
using ShiftsLogger.API.Models;
using ShiftsLogger.API.Services;

namespace ShiftsLogger.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsLoggerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ShiftsService _shiftsService;

    public ShiftsLoggerController(ApplicationDbContext context, ShiftsService shiftsService)
    {
        _context = context;
        _shiftsService = shiftsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShifts()
    {
        var shiftsDTO = await _shiftsService.GetAllShiftsAsync();

        return Ok(shiftsDTO);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
        var shiftDTO = await _shiftsService.GetShiftByIdAsync(id);
        if (shiftDTO == null) return NotFound();

        return Ok(shiftDTO);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShift(AddShiftDto newShift)
    {
        Shift? shift = await _shiftsService.CreateShiftAsync(newShift);
        if (shift == null) return BadRequest();

        return CreatedAtAction(nameof(GetShiftById), new { Id = shift.Id }, shift);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShift(int id, UpdateShiftDto updatedShift)
    {
        Shift? shift = await _shiftsService.UpdateShiftAsync(id, updatedShift);
        if (shift == null) return BadRequest();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShiftById(int id)
    {
        var shift = await _shiftsService.DeleteShiftAsync(id);
        if (shift == null) return NotFound();

        return NoContent();
    }
}
