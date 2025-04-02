using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerAPI.Dto;
using ShiftsLoggerAPI.Interfaces;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    private readonly IShiftRepository? _shiftRepository;
    private readonly IMapper _mapper;
    public ShiftController(IShiftRepository shiftRepository, IMapper mapper)
    {
        _shiftRepository = shiftRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Shift>))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ICollection<Shift>>> GetShiftsAsync()
    {
        var shifts = _mapper.Map<List<ShiftDto>>(await _shiftRepository.GetShiftsAsync());

        if (shifts == null)
            return NotFound();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(shifts);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Shift))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Shift>> FindShiftAsync(int id)
    {
        var shift = _mapper.Map<ShiftDto>(await _shiftRepository.FindShiftAsync(id));

        if (shift == null)
            return NotFound();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(shift);
    }

    [HttpGet("empId/{empId}")]
    [ProducesResponseType(200, Type = typeof(Shift))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Shift>> FindShiftsAsync(int empId)
    {
        var shift = _mapper.Map<List<ShiftDto>>(await _shiftRepository.FindEmpShiftsAsync(empId));

        if (shift == null)
            return NotFound();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(shift);
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(Shift))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Shift>> CreateShiftAsync([FromBody] Shift shift)
    {
        var newShift = _mapper.Map<ShiftDto>(await _shiftRepository.CreateShiftAsync(shift));
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(newShift);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(200, Type = typeof(string))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<string>> DeleteShiftAsync(int id)
    {
        var deleteResult = await _shiftRepository.DeleteShiftAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (deleteResult == null)
            return NotFound();
        return Ok(deleteResult);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Shift))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Shift>> UpdateEmployeeAsync(int id, [FromBody] Shift updatedShift)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        updatedShift.ShiftId = id;

        var shift = await _shiftRepository.UpdateShiftAsync(id, updatedShift);

        if (shift == null)
            return NotFound();

        return Ok(_mapper.Map<ShiftDto>(shift));
    }
}