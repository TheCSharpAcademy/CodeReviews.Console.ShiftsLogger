using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Services;
using Spectre.Console;

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
    [HttpGet (Name ="Get All Shifts") ]
    public async Task <ActionResult<List<Shift>>> GetAllShifts()
    {
        try
            {
            var shifts = await _shiftService.GetAllShifts();
            AnsiConsole.MarkupLine("\n[green]Retrieved all shifts successfully.[/]");
            return Ok(shifts);
            }
        catch(Exception ex)
            {
			Console.WriteLine($"Get All Shifts failed, see Exception {ex}");

			throw;
            }
    }

    //This is the route for getting a createdShift by ID
    [HttpGet("{id}")] // This will be added to the API URI (send some data during the request
    public async Task <ActionResult<Shift>> GetShiftById(int id)
    {
        try
        {
            var result = await _shiftService.GetShiftById(id);

            if (result == null)
            {
                AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found.[/]");
                return NotFound($"\nShift with ID: {id} not found."); //Equivalent to 404
            }

            AnsiConsole.MarkupLine($"\n[green]Retrieved createdShift with ID: {id} successfully.[/]");
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
    public async Task <ActionResult<Shift>> CreateShift(Shift shift)
    {
       var createdShift = await _shiftService.CreateShift(shift);
       
       return new ObjectResult (createdShift){ StatusCode = 201 }; //201 is the status code for created
		}

    //This is the route for updating a createdShift
    [HttpPut("{id}")]
    public async Task<ActionResult<Shift>> UpdateShift(int id, Shift updatedShift)
    {
        try
        {
            var result = await _shiftService.UpdateShift(id, updatedShift);

            if (result == null)
            {
                AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found for update.[/]");
                return NotFound($"\nShift with ID: {id} not found.");
            }

            AnsiConsole.MarkupLine($"\n[green]Updated createdShift with ID: {id} successfully.[/]");
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
            var result = await _shiftService.DeleteShift(id);

            if (result == null)
            {
                AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found for deletion.[/]");
                return NotFound($"\nShift with ID: {id} not found.");
            }

            AnsiConsole.MarkupLine($"\n[green]{result}[/]");
            return new ObjectResult(result) { StatusCode = 200 }; //200 is the status code for OK
			}
        catch (Exception ex)
        {
			Console.WriteLine($"Delete Shift failed, see Exception {ex}");
			throw;
        }
    }
}
