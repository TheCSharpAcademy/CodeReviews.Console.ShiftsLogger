using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/shift/[controller]")]
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
        return Ok(_shiftService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShift(int id) {
        var result = _shiftService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("latest")]
    public ActionResult<Shift> GetLatestShift()
    {
        var result = _shiftService.GetLatest();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all-worker-{id}")]
    public ActionResult<List<Shift>> GetAllShiftsByWorker(int id)
    {
        var result = _shiftService.GetAllByWorkerId(id);

        if (result.Count == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift) {
        var result = _shiftService.Add(shift);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    public ActionResult<Shift> UpdateShift(Shift shift)
    {
        bool result = _shiftService.Update(shift);

        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult<Shift> DeleteShift(int id) {
        bool result = _shiftService.Delete(id);

        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }
}
