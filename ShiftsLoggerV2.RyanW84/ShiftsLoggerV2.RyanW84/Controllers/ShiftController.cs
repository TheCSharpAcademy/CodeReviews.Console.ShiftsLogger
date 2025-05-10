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
    public ActionResult<List<Shift>> GetAllShifts()
    {
        try
            {
            var shifts = _shiftService.GetAllShifts();
            AnsiConsole.MarkupLine("\n[green]Retrieved all shifts successfully.[/]");
            return Ok(shifts);
            }
        catch(Exception ex)
            {
			Console.WriteLine($"Get All Shifts failed, see Exception {ex}");

			throw;
            }
    }

    //This is the route for getting a shift by ID
    [HttpGet("{id}")] // This will be added to the API URI (send some data during the request
    public ActionResult<Shift> GetShiftById(int id)
    {
        try
        {
            var result = _shiftService.GetShiftById(id);

            if (result == null)
            {
                AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found.[/]");
                return NotFound($"\nShift with ID: {id} not found."); //Equivalent to 404
            }

            AnsiConsole.MarkupLine($"\n[green]Retrieved shift with ID: {id} successfully.[/]");
            return Ok(result);
        }
        catch (Exception ex)
        {
			Console.WriteLine($"Get by ID failed, see Exception {ex}");
			throw;
        }
    }

    //This is the route for creating a shift
    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        try
        {
            var createdShift = _shiftService.CreateShift(shift);
            AnsiConsole.MarkupLine(
                $"\n[green]Created shift with ID: {createdShift.ShiftId} successfully.[/]"
            );
            return Ok(createdShift);
        }
        catch (Exception ex)
        {
			Console.WriteLine($"Create Shift failed, see Exception {ex}");

            throw;
        }
    }

    //This is the route for updating a shift
    [HttpPut("{id}")]
    public ActionResult<Shift> UpdateShift(int id, Shift updatedShift)
    {
        try
        {
            var result = _shiftService.UpdateShift(id, updatedShift);

            if (result == null)
            {
                AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found for update.[/]");
                return NotFound($"\nShift with ID: {id} not found.");
            }

            AnsiConsole.MarkupLine($"\n[green]Updated shift with ID: {id} successfully.[/]");
            return Ok(result);
        }
        catch (Exception ex)
        {
			Console.WriteLine($"Update Shift failed, see Exception {ex}");
			throw;
        }
    }

    //This is the route for deleting a shift
    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int id)
    {
        try
        {
            var result = _shiftService.DeleteShift(id);

            if (result == null)
            {
                AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found for deletion.[/]");
                return NotFound($"\nShift with ID: {id} not found.");
            }

            AnsiConsole.MarkupLine($"\n[green]{result}[/]");
            return Ok(result);
        }
        catch (Exception ex)
        {
			Console.WriteLine($"Delete Shift failed, see Exception {ex}");
			throw;
        }
    }
}
