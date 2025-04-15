using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly IShiftService _shiftService;    
    public ShiftsController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public ActionResult<List<Shift>> GetAllShifts()
    {
        var result = _shiftService.GetAllShifts();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShiftById(int id)
    {
        var result = _shiftService.GetShiftById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        var result = _shiftService.CreateShift(shift);
        return Ok(result);
    }

    [HttpPut]
    public ActionResult<Shift> UpdateShift(Shift shift)
    {
        var result = _shiftService.UpdateShift(shift);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int id)
    {
        var result = _shiftService.DeleteShift(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}