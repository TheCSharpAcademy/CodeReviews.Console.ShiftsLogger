using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController: ControllerBase {
    private readonly ShiftService service; 
    public ShiftController(ShiftService shiftService)
    {
        service = shiftService;
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetShiftDto>>> GetShifts()
    {
        var shifts = await service.GetShiftsAsync();

        return Ok(shifts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetShiftDto>> GetShift(int id)
    {
        var shift = await service.GetShiftById(id);

        if (shift == null) {
            return NotFound();
        }

        return Ok(shift);
    }

    [HttpPost]
    public async Task<ActionResult<GetShiftDto>> PostShift(PostShiftDto dto)
    {
        var createdShift = await service.CreateShift(dto); 

        return CreatedAtAction("GetShift", new { id = createdShift.Id }, createdShift); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShift(int id, PutShiftDto dto)
    {
        var shift = await service.FindShift(id);
        if (shift == null) {
            return NotFound();
        }
        await service.UpdateShift(shift, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        var shift = await service.FindShift(id);
        if (shift == null)
        {
            return NotFound();
        }

        await service.DeleteShift(shift);

        return NoContent();
    }
}