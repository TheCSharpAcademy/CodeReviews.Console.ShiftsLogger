using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


# pragma warning disable CS1591
[ApiController]
[Route("api/[controller]")]
public class WorkerShiftController: ControllerBase {
    private readonly WorkerShiftService service;
    public WorkerShiftController(WorkerShiftService workerShiftService)
    {
        service = workerShiftService;
    }    
# pragma warning restore CS1591

    /// <summary>
    /// Get a list of all worker shifts
    /// </summary>
    /// <returns>The list of worker shifts</returns>
    /// <response code="200">Returns the list of worker shifts</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetWorkerShiftDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetWorkerShiftDto>>> GetWorkerShifts()
    {
        var workerShifts = await service.GetWorkerShifts();

        return Ok(workerShifts);
    }

    /// <summary>
    /// Gets a worker shift by id
    /// </summary>
    /// <param name="id">The id of the worker shift</param>
    /// <returns>The worker shift</returns>
    /// <response code="200">Returns the worker shift</response>
    /// <response code="404">If the worker shift was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetWorkerShiftDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetWorkerShiftDto>> GetWorkerShift(int id)
    {
       var workerShift = await service.GetWorkerShift(id); 

       if (workerShift == null) {
            return NotFound();
       }

        return Ok(workerShift); 
    }

    /// <summary>
    /// Creates a new worker shift
    /// </summary>
    /// <param name="dto">The worker shift information</param>
    /// <returns>The created worker shift</returns>
    /// <response code="201">Returns the created worker shift</response>
    /// <response code="400">Badly formed request</response>
    /// <remarks>
    /// Sample reuest:
    /// 
    ///     POST /api/workerShift
    ///     {
    ///         "workerId": 1,
    ///         "shiftId", 3,
    ///         "shiftDate": "2024-08-01"
    ///     }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(WorkerShift), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkerShift>> PostWorker(PostWorkerShiftDto dto)
    {
        var workerShift = await service.CreateWorkerShift(dto); 

        return CreatedAtAction("GetWorkerShift", new { id = workerShift.Id }, workerShift); 
    }

    /// <summary>
    /// Updates a worker shift
    /// </summary>
    /// <param name="id">The id of the worker shift to update</param>
    /// <param name="dto">The updated worker shift information</param>
    /// <returns></returns>
    /// <response code="404">If the worker shift was not found</response>
    /// <response code="204">The worker was updated</response>
    /// <remarks>
    /// Example request:
    /// 
    ///     PUT /api/worker/{id}
    ///     {
    ///         "workerId": 1,
    ///         "shiftId": 2,
    ///         "shiftDate": "2024-08-01"
    ///     }
    ///     
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutWorkerShift(int id, PutWorkerShiftDto dto)
    {
        var workerShift = await service.FindWorkerShiftEntity(id);

        if (workerShift == null)
        {
            return NotFound();
        }

        await service.UpdateWorkerShift(workerShift, dto);

        return NoContent();
    }

    /// <summary>
    /// Deletes a worker shift by id
    /// </summary>
    /// <param name="id">The id of the worker shift to delete</param>
    /// <returns></returns>
    /// <response code="404">If the worker shift was not found</response>
    /// <response code="204">The worker was deleted</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWorkerShift(int id)
    {
        var workerShift = await service.FindWorkerShiftEntity(id);
        if (workerShift == null)
        {
            return NotFound();
        }

        await service.DeleteWorkerShift(workerShift);

        return NoContent();
    }
}