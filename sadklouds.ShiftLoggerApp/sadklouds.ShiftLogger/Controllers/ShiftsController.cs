using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace sadklouds.ShiftLogger.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly IShiftLoggerService _shiftLoggerService;
    private readonly ShiftDataContext _context;
    private readonly IMapper _mapper;

    public ShiftsController(IShiftLoggerService shiftLoggerService)
    {
        _shiftLoggerService = shiftLoggerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetShiftDto>>> GetShifts()
    {
        return Ok(await _shiftLoggerService.GetShifts());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<GetShiftDto>>> GetShiftById(int id)
    {
        var response = await _shiftLoggerService.GetShiftById(id);
        if (response is null)
            return NotFound();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AddShiftDto>> AddShift(AddShiftDto newShift)
    {
        await _shiftLoggerService.AddShift(newShift);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateShift(int id, UpdateShiftDto updatedShift)
    {

        var response = await _shiftLoggerService.UpdateShift(id, updatedShift);
        if (response is null)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<GetShiftDto>>> DeleteShift(int id)
    {
        var response = await _shiftLoggerService.DeleteShift(id);
        if (response is null)
            return NotFound();
        return NoContent();
    }
}
