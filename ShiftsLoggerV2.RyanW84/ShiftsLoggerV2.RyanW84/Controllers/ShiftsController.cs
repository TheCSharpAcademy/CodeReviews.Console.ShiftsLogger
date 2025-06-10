using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using ShiftsLoggerV2.RyanW84.Services;

namespace ShiftsLoggerV2.RyanW84.Controllers;

[ApiController]
//http://localhost:5009/api/shifts/ this is what the route will look like
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

            if (result.ResponseCode is System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(); // Equivalent to 404
            }
            else if (result.ResponseCode is System.Net.HttpStatusCode.NoContent)
            {
                return NoContent(); // Equivalent to 204
            }
            else if (result.ResponseCode is System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(result.Message); // Equivalent to 400
            }
            else if (result.RequestFailed)
            {
                return StatusCode((int)result.ResponseCode, result.Message); // Return the response code and message
            }
            Console.WriteLine($"Shift with ID {id} retrieved successfully.");
            // Return the shift data with a 200 OK status code
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

            if (result.ResponseCode is System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else if (result.ResponseCode is System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(result.Message);
            }
            else if (result.RequestFailed)
            {
                return StatusCode((int)result.ResponseCode, result.Message);
            }

            Console.WriteLine($"Shift with ID {id} deleted successfully.");
            return NoContent(); // Equivalent to 204
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Delete Shifts failed, see Exception {ex}");
            throw;
        }
    }
}
