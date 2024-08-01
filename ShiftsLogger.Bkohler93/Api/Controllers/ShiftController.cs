using System.Runtime.CompilerServices;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

# pragma warning disable CS1591
[ApiController]
[Route("api/[controller]")]
public class ShiftController: ControllerBase {
    private readonly ShiftService service; 
    public ShiftController(ShiftService shiftService)
    {
        service = shiftService;
    }
# pragma warning restore CS1591

    /// <summary>
    /// Retrieves all shifts.
    /// </summary>
    /// <returns>An array of shifts.</returns>
    /// <response code="200">Returns the array of shifts. </response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetShiftDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetShiftDto>>> GetShifts()
    {
        var shifts = await service.GetShiftsAsync();

        return Ok(shifts);
    }

    /// <summary>
    /// Retrieves a shift by its id
    /// </summary>
    /// <param name="id">The id of the shift</param>
    /// <returns>The shift</returns>
    /// <response code="200">Returns the shift</response>
    /// <response code="404">If shift not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetShiftDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetShiftDto>> GetShift(int id)
    {
        var shift = await service.GetShiftById(id);

        if (shift == null) {
            return NotFound();
        }

        return Ok(shift);
    }

   /// <summary>
    /// Posts a new shift 
    /// </summary>
    /// <param name="dto">The shift to be created</param>
    /// <returns>The shift that was created</returns>
    /// <response code="201">Returns the created shift</response>
    /// <response code="400">If bad request</response>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/shift
    ///     {
    ///        "name": "Morning",
    ///        "startTime": "09:00",
    ///        "endTime": "15:00"
    ///     }
    ///     
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(GetShiftDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetShiftDto>> PostShift(PostShiftDto dto)
    {
        var createdShift = await service.CreateShift(dto); 

        return CreatedAtAction("GetShift", new { id = createdShift.Id }, createdShift); 
    }

    /// <summary>
    /// Updates a shift with new information
    /// </summary>
    /// <param name="id">The id of the shift</param>
    /// <param name="dto">The updated shift</param>
    /// <returns></returns>
    /// <response code="204">If the shift was updated</response>
    /// <response code="404">If the shift was not found</response>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/shift/{id}
    ///     {
    ///         "name": "Morning",
    ///         "startTime": "09:00",
    ///         "endTime": "15:00"
    ///     }
    ///     
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutShift(int id, PutShiftDto dto)
    {
        var shift = await service.FindShift(id);
        if (shift == null) {
            return NotFound();
        }
        await service.UpdateShift(shift, dto);

        return NoContent();
    }

    /// <summary>
    /// Deletes a shift
    /// </summary>
    /// <param name="id">The id of the shift to delete</param>
    /// <returns></returns>
    /// <response code="404">If the shift was not found</response>
    /// <response code="204">If the shift was deleted</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteShift(int id)
    {
        var shift = await service.FindShift(id);
        if (shift == null)
        {
            return NotFound();
        }

        await service.DeleteShift(shift);

        return NoContent();
    }
}