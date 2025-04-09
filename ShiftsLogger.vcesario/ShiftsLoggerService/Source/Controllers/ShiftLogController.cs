using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ShiftLogController : ControllerBase
{
    private readonly IShiftLogService m_ShiftLogService;

    public ShiftLogController(IShiftLogService service)
    {
        m_ShiftLogService = service;
    }

    [HttpGet]
    public ActionResult<List<Shift>> GetShifts()
    {
        return Ok(m_ShiftLogService.GetShifts());
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShift(int id)
    {
        var result = m_ShiftLogService.GetShiftById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        return Ok(m_ShiftLogService.CreateShift(shift));
    }

    [HttpPut]
    public ActionResult<Shift> UpdateShift(Shift shift)
    {
        var result = m_ShiftLogService.UpdateShift(shift);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int id)
    {
        var result = m_ShiftLogService.DeleteShift(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}