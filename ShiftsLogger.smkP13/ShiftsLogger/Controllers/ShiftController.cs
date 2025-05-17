using Microsoft.AspNetCore.Mvc;
using ShiftWebApi.Models;
using ShiftWebApi.Services;

namespace ShiftWebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ShiftController(IShiftService shiftService, IUserService userService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public ActionResult<List<Shift>> GetAllShifts()
    {
        return Ok(_shiftService.GetAllShifts());
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShiftById(int id)
    {
        Shift? result = _shiftService.GetShiftById(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("user/{id}")]
    public ActionResult<List<Shift>> GetShiftByUserId(int id)
    {
        List<Shift>? result = _shiftService.GetShiftsByUserId(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        Shift? result = _shiftService.CreateShift(shift);
        return result == null ? BadRequest() : Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult<Shift> UpdateShift(int id, Shift shift)
    {
        Shift? result = _shiftService.UpdateShift(id, shift);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string?> DeleteShift(int id)
    {
        string? result = _shiftService.DeleteShift(id);
        return result == null ? NotFound() : Ok(result);
    }
}
