using Microsoft.AspNetCore.Mvc;
using shiftsLogger.doc415.Models;
using shiftsLogger.doc415.Services;

namespace shiftsLogger.doc415.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftsController : Controller
{
    private readonly ILogger<ShiftsController> _logger;

    public ShiftsController(ILogger<ShiftsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetShifts")]
    public IEnumerable<Shift> Get()
    {
        return ShiftService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> Get(int id)
    {
        var shift = ShiftService.GetByID(id);

        if (shift == null)
            return NotFound();

        return shift;
    }

    [HttpPost]
    public ActionResult Create(Shift shift)
    {
        ShiftService.Add(shift);
        return CreatedAtAction(nameof(Get), new
        {
            Name = shift.Name,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Duration = shift.Duration,
            shift
        }
        );
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Shift shift)
    {
        if (id != shift.Id)
        {
            return BadRequest();
        }

        var shiftToUpdate = ShiftService.GetByID(shift.Id);
        if (shiftToUpdate == null)
            return NotFound();
        ShiftService.Update(shift);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var shift = ShiftService.GetByID(id);
        if (shift == null)
            return NotFound();

        ShiftService.Delete(id);
        return NoContent();
    }
}
