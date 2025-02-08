using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerWebAPI.DTOs;
using ShiftsLoggerWebAPI.Services;

namespace ShiftsLoggerWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : Controller
{
    private readonly IShiftService _shiftService;
    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpPost]
    public ActionResult<string> CreateShift(ShiftDto shiftDto)
    {
        var result = _shiftService.CreateShift(shiftDto);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet]
    public ActionResult<List<ShiftDto>> GetAllShifts()
    {
        var result = _shiftService.GetAllShifts();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("Employee/{id}")]
    public ActionResult<ShiftDto> GetLast10Shift(int id)
    {
        var result = _shiftService.Get10ShiftsByEmployee(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<ShiftDto> GetShiftById(int id)
    {
        var result = _shiftService.GetShiftById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPut]
    public ActionResult<string> UpdateShift(ShiftDto updatedShift)
    {
        var result = _shiftService.UpdateShift(updatedShift);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int id)
    {
        var result = _shiftService.DeleteShift(id);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}