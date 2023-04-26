using Microsoft.AspNetCore.Mvc;

namespace sadklouds.ShiftLogger.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly IShiftLoggerService _shiftLoggerService;

    public ShiftsController(IShiftLoggerService shiftLoggerService)
    {
        _shiftLoggerService = shiftLoggerService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetShiftDto>>>> GetShifts()
    {
        return Ok(await _shiftLoggerService.GetShifts());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetShiftDto>>>> GetShiftById(int id)
    {
        var response = await _shiftLoggerService.GetShiftById(id);
        if (response.Data is null)
            return NotFound(response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<AddShiftDto>>> AddShift(AddShiftDto newShift)
    {
        return Ok(await _shiftLoggerService.AddShift(newShift));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetShiftDto>>>> UpdateShift(UpdateShiftDto updatedShift)
    {
        var response = await _shiftLoggerService.UpdateShift(updatedShift);
        if (response.Data is null)
            return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetShiftDto>>>> DeleteShift(int id)
    {
        var response = await _shiftLoggerService.DeleteShift(id);
        if (response.Data is null)
            return NotFound(response);
        return Ok(response);
    }
}
