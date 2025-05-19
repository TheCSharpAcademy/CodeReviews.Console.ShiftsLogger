using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Services;
using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Controllers;

[ApiController]
//http://localhost:5009/api/shiftcontroller/ this is what the route will look like
[Route("api/[controller]")]
public class ShiftsController(IShiftService shiftService) : ControllerBase
{
    // This is the route for getting all shifts
    [HttpGet(Name = "Get All Shifts")]
    public async Task<ActionResult<List<Shifts>>> GetAllShifts(ShiftFilterOptions shiftOptions)
    {
        try
        {
         
            return Ok(await shiftService.GetAllShifts(shiftOptions));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Get All Shifts failed, see Exception {ex}");

            throw;
        }
    }

    // This is the route for getting a createdShift by ID
    [HttpGet("{id}")] // This will be added to the API URI (send some data during the request
    public async Task<ActionResult<Shifts>> GetShiftById(int id)
    {
        try
        {
            var result = await shiftService.GetShiftById(id);

            if (result == null)
            {
                return NotFound(); // Equivalent to 404
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Get by ID failed, see Exception {ex}");
            throw;
        }
    }

    // This is the route for creating a createdShift
    [HttpPost]
    public async Task<ActionResult<Shifts>> CreateShift(ShiftApiRequestDto shift)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return new ObjectResult(await shiftService.CreateShift(shift)) { StatusCode = 201 }; //201 is the status code for Created
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Create shift failed, see Exception {ex}");
            throw;
        }
    }

    // This is the route for updating a createdShift
    [HttpPut("{id}")]
    public async Task<ActionResult<Shifts>> UpdateShift(int id, ShiftApiRequestDto updatedShift)
    {
        try
        {
            var result = await shiftService.UpdateShift(id, updatedShift);

            if (result is null)
            {
                return NotFound(); // Equivalent to 404
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update shift failed, see Exception {ex}");
            throw;
        }
    }

    // Fix for CS0019 and CS1525 errors in the DeleteShift method
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteShift(int id)
    {
        try
        {
            var result = await shiftService.DeleteShift(id);

            // Corrected the condition to check the ResponseCode property of the result
            if (result.ResponseCode == HttpStatusCode.NotFound || result is null)
            {
                return NotFound();
            }

            return NoContent(); // Equivalent to 204
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Delete Shifts failed, see Exception {ex}");
            throw;
        }
    }
}
