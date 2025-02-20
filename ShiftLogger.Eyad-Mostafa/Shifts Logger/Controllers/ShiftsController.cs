using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shifts_Logger.DTOs;
using Shifts_Logger.Models;
using Shifts_Logger.Services;

namespace Shifts_Logger.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly IShiftService _shiftService;
    private readonly IMapper _mapper;

    public ShiftsController(IShiftService shiftService, IMapper mapper)
    {
        _shiftService = shiftService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult GetShifts()
    {
        var shifts = _shiftService.GetShifts();
        return Ok(shifts);
    }

    [HttpGet("{id}")]
    public ActionResult GetShift(int id)
    {
        var shift = _shiftService.GetShift(id);
        if (shift == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ShiftDto>(shift));
    }

    [HttpPost]
    public ActionResult AddShift([FromBody] CreateShiftDto shiftDto)
    {
        var shift = _mapper.Map<Shift>(shiftDto);
        _shiftService.AddShift(shift.StartTime, shift.EndTime, shift.WorkerId);
        return CreatedAtAction(nameof(GetShift), new { id = shift.Id }, shiftDto);
    }

    [HttpPut]
    public ActionResult UpdateShift(int id, [FromBody] CreateShiftDto shiftDto)
    {
        var shift = _mapper.Map<Shift>(shiftDto);
        _shiftService.UpdateShift(id, shift.StartTime, shift.EndTime, shift.WorkerId);
        return Ok();
    }

    [HttpDelete]
    public ActionResult DeleteShift(int id)
    {
        _shiftService.DeleteShift(id);
        return Ok();
    }
}
