using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Api;
using ShiftLogger.Api.Data;
using ShiftLogger.Api.Models;

[ApiController]
[Route("api/shifts")]
public class ShiftController : ControllerBase
{

    private readonly ApplicationDbContext _db;
    
    public ShiftController(ApplicationDbContext db)
    {
       _db = db; 
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Shift>> GetAllShifts()
    {
        return Ok(_db.Shifts.ToList());
    }

    [HttpGet("{id:int}", Name = "GetShift")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Shift> GetShift(int id)
    {
        if(id < 1)
        {
            return BadRequest();
        }
        var shift =  _db.Shifts.FirstOrDefault(s => s.Id == id);

        if(shift is null)
        {
            return NotFound();
        }

        return Ok(shift);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateShiftAsync([FromBody]Shift shift)
    {
        if(shift == null)
        {
            return BadRequest();
        }
        if(shift.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if(shift.Start > shift.End)
        {
            ModelState.AddModelError("ModelError", "Start 'DateTime' Cannot be bigger than End 'DateTime'");
            return BadRequest(ModelState);
        }
    
        await _db.Shifts.AddAsync(shift);
        await _db.SaveChangesAsync();
        return CreatedAtRoute("GetShift", new {Id = shift.Id}, shift);
    }    


    [HttpPut("{id:int}", Name = "UpdateShift")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public async Task<IActionResult> UpdateShift(int id, [FromBody] Shift shift)
    {
        if(id == 0 || shift.Id == 0 || id != shift.Id)
        {
            return BadRequest();
        }
        
        if(shift.Start > shift.End)
        {
            ModelState.AddModelError("ModelError", "Start 'DateTime' cannot be bigger than End 'DateTime'");
            return BadRequest(ModelState);
        }

        var entry = _db.Shifts.FirstOrDefault(s => s.Id == id);
        if(entry is null)
        {
            return NotFound();
        }
        
        entry.EmployeeName = shift.EmployeeName;
        entry.Start = shift.Start;
        entry.End = shift.End;

        _db.Shifts.Update(entry);
        await _db.SaveChangesAsync();
        
        return Ok();

    }

    [HttpDelete("{id:int}", Name = "DeleteShift")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public IActionResult DeleteShift(int id)
    {
        if(id < 1)
        {
            return BadRequest();
        }
        var shift = _db.Shifts.FirstOrDefault(s => s.Id == id);
        if(shift == null) return NotFound();

        _db.Shifts.Remove(shift);
        _db.SaveChanges();
        return Ok();
    }
    
}