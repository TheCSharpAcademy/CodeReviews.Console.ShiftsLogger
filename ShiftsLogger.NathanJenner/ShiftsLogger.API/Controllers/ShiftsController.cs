using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.API.Models;
using ShiftsLogger.API.Services;

namespace ShiftsLogger.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly ShiftDbService _shiftService;

    public ShiftsController(ShiftDbService shiftService)
    {
        _shiftService = shiftService;
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Shift>> GetAll() =>
    _shiftService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Shift> Get(int id)
    {
        var shift = _shiftService.Get(id);

        if (shift == null)
            return NotFound();

        return shift;
    }

    // POST action
    [HttpPost]
    public ActionResult Create(Shift shift)
    {
        _shiftService.Add(shift);
        return CreatedAtAction(nameof(Get), new { id = shift.Id }, shift);
    }

    // PATCH action
    [HttpPatch]
    public ActionResult Update(Shift shift)
    {
        _shiftService.Update(shift);
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var shift = _shiftService.Get(id);

        if (shift is null)
            return NotFound();

        _shiftService.Delete(shift);

        return NoContent();
    }
}