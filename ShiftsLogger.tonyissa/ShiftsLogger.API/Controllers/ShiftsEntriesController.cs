using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Models;

namespace ShiftsLogger.API.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class ShiftsEntriesController : ControllerBase
{
    private readonly ShiftsContext _context;

    public ShiftsEntriesController(ShiftsContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns a list of shifts
    /// </summary>
    /// <returns>A list of shifts</returns>
    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<IEnumerable<ShiftsEntry>>> GetShiftsEntry()
    {
        return await _context.ShiftsEntry.ToListAsync();
    }

    /// <summary>
    /// Returns a specific shift
    /// </summary>
    /// <param name="id">id of the shift</param>
    /// <returns>A specific shift</returns>
    [HttpGet("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<ShiftsEntry>> GetShiftsEntry(long id)
    {
        var shiftsEntry = await _context.ShiftsEntry.FindAsync(id);

        if (shiftsEntry == null)
        {
            return NotFound();
        }

        return shiftsEntry;
    }

    /// <summary>
    /// Updates a specific shift
    /// </summary>
    /// <param name="id">id of the shift</param>
    /// <param name="shiftsEntry">The new shift to update the old one</param>
    [HttpPut("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutShiftsEntry(long id, ShiftsEntry shiftsEntry)
    {
        if (id != shiftsEntry.Id)
        {
            return BadRequest();
        }

        _context.Entry(shiftsEntry).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShiftsEntryExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Creates a new shift
    /// </summary>
    /// <param name="shiftsEntry">The shift to be created</param>
    /// <returns>The newly created shift</returns>
    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<ActionResult<ShiftsEntry>> PostShiftsEntry(ShiftsEntry shiftsEntry)
    {
        _context.ShiftsEntry.Add(shiftsEntry);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetShiftsEntry", new { id = shiftsEntry.Id }, shiftsEntry);
    }

    /// <summary>
    /// Deletes a shift
    /// </summary>
    /// <param name="id">The id of the shift to be deleted</param>
    [HttpDelete("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteShiftsEntry(long id)
    {
        var shiftsEntry = await _context.ShiftsEntry.FindAsync(id);
        if (shiftsEntry == null)
        {
            return NotFound();
        }

        _context.ShiftsEntry.Remove(shiftsEntry);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShiftsEntryExists(long id)
    {
        return _context.ShiftsEntry.Any(e => e.Id == id);
    }
}