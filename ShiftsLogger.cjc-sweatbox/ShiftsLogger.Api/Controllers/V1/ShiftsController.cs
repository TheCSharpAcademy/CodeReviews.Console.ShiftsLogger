using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Api.Contracts.V1;
using ShiftsLogger.Api.Contracts.V1.Requests;
using ShiftsLogger.Api.Contracts.V1.Responses;
using ShiftsLogger.Data.Entities;
using ShiftsLogger.Data.Services;

namespace ShiftsLogger.Api.Controllers.V1;

/// <summary>
/// Controls the interactions between the shift api and the shift database.
/// </summary>
[ApiController]
public class ShiftsController : ControllerBase
{
    #region Fields

    private readonly IShiftService _shiftService;

    #endregion
    #region Constructors

    public ShiftsController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    #endregion
    #region Methods

    [HttpPost(ApiRoutes.Shifts.Create)]
    public async Task<ActionResult<Shift>> CreateShiftAsync([FromBody] ShiftRequest request)
    {
        var shift = new Shift
        {
            Id = Guid.NewGuid(),
            StartTime = request.StartTime,
            EndTime = request.EndTime
        };

        await _shiftService.CreateAsync(shift);

        var response = new ShiftResponse { Id = shift.Id };

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Shifts.GetById.Replace("{shiftId}", shift.Id.ToString())}";

        return Created(locationUrl, response);
    }


    [HttpGet(ApiRoutes.Shifts.Get)]
    public async Task<ActionResult<IEnumerable<Shift>>> GetShiftAsync()
    {
        return Ok(await _shiftService.ReturnAsync());
    }

    [HttpGet(ApiRoutes.Shifts.GetById)]
    public async Task<ActionResult<Shift>> GetShiftAsync([FromRoute] Guid shiftId)
    {
        var shift = await _shiftService.ReturnByIdAsync(shiftId);

        if (shift == null)
        {
            return NotFound();
        }

        return Ok(shift);
    }

    [HttpPut(ApiRoutes.Shifts.Update)]
    public async Task<IActionResult> UpdateShiftAsync([FromRoute] Guid shiftId, [FromBody] ShiftRequest request)
    {
        var shift = await _shiftService.ReturnByIdAsync(shiftId);
        if (shift == null)
        {
            return BadRequest();
        }

        shift.StartTime = request.StartTime;
        shift.EndTime = request.EndTime;

        var updated = await _shiftService.UpdateAsync(shift);
        return updated ? Ok(shift) : NotFound();
    }

    [HttpDelete(ApiRoutes.Shifts.Delete)]
    public async Task<IActionResult> DeleteShiftAsync([FromRoute] Guid shiftId)
    {
        var deleted = await _shiftService.DeleteAsync(shiftId);

        return deleted ? NoContent() : NotFound();
    }

    #endregion
}
