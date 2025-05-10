using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Services;
using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Controllers;

[ApiController]
//http://localhost:5009/api/shiftcontroller/ this is what the route will look like
[Route("api/[controller]")]
public class ShiftController(IShiftService shiftService) : ControllerBase
{
    //This is the route for getting all shifts
    [HttpGet(Name = "Get All Shifts")]
    public async Task<ActionResult<List<Shift>>> GetAllShifts()
    {
        try
        {
            var shifts = await shiftService.GetAllShifts();
            return Ok(await shiftService.GetAllShifts());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Get All Shifts failed, see Exception {ex}");

            throw;
        }
    }

    //This is the route for getting a createdShift by ID
    [HttpGet("{id}")] // This will be added to the API URI (send some data during the request
    public async Task<ActionResult<Shift>> GetShiftById(int id)
    {
        try
        {
            var result = await shiftService.GetShiftById(id);

            if (result == null)
            {
                return NotFound(); //Equivalent to 404
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Get by ID failed, see Exception {ex}");
            throw;
        }
    }

    //This is the route for creating a createdShift
    [HttpPost]
    public async Task<ActionResult<Shift>> CreateShift(ShiftApiRequestDto shift)
    {
        try
        {
            return new ObjectResult(await shiftService.CreateShift(shift)) { StatusCode = 201 }; //201 is the status code for Created
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Create Shift failed, see Exception {ex}");
            throw;
        }
    }

    //This is the route for updating a createdShift
    [HttpPut("{id}")]
    public async Task<ActionResult<Shift>> UpdateShift(int id, ShiftApiRequestDto updatedShift)
    {
        try
        {
            var result = await shiftService.UpdateShift(id, updatedShift);

            if (result is null)
            {
                return NotFound(); //Equivalent to 404
				}

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update Shift failed, see Exception {ex}");
            throw;
        }
    }

    //This is the route for deleting a createdShift
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteShift(int id)
    {
        try
        {
            var result = await shiftService.DeleteShift(id);

            if (result == null)
            {
                return NotFound();
            }
            return NoContent(); //Equivalent to 204
			}
        catch (Exception ex)
        {
            Console.WriteLine($"Delete Shift failed, see Exception {ex}");
            throw;
        }
    }
}
