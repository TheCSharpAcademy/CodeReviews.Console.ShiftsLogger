using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Services;

namespace ShiftsLoggerV2.RyanW84.Controllers;

[ApiController]
//http://localhost:5009/api/shiftcontroller/ this is what the route will look like
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;

    //We need to inject the IShiftService into the controller so we can use it
    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    //This is the route for getting all shifts
    [HttpGet]
    public ActionResult<List<Shift>> GetAllShifts()
    {
        var shifts = _shiftService.GetAllShifts();
        return Ok(shifts);
    }

    //This is the route for getting a shift by ID
    [HttpGet("{id}")] // This will be added to the API URI (send some data during the request
    public ActionResult<Shift> GetShiftById(int id)
    {
        var shift = _shiftService.GetShiftById(id);
        if (shift == null)
        {
            return NotFound();
        }
        return Ok(_shiftService.GetShiftById(id));
    }

    //This is the route for creating a shift
    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        {
            return Ok(_shiftService.CreateShift(shift));
        }
    }

    //This is the route for updating a shift
    [HttpPut("{id}")]
    public ActionResult<Shift> UpdateShift(int id, Shift updatedShift)
    {
        return Ok(_shiftService.UpdateShift(id, updatedShift));
    }

    //This is the route for deleting a shift
    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int id)
    {
        return Ok(_shiftService.DeleteShift(id));
    }
}
