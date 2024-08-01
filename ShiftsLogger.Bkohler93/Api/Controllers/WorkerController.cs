using System.Runtime.CompilerServices;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

# pragma warning disable CS1591
[ApiController]
[Route("api/[controller]")]
public class WorkerController: ControllerBase {
    private readonly WorkerService service; 
    public WorkerController(WorkerService workerService)
    {
        service = workerService;
    }    
# pragma warning restore CS1591

    /// <summary>
    /// Gets a list of workers
    /// </summary>
    /// <returns>The list of workers</returns>
    /// <response code="200">Returns the list of workers. </response> 
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetWorkerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetWorkerDto>>> GetWorkers()
    {
        var workers = await service.GetWorkers();

        return Ok(workers);     
    }

    /// <summary>
    /// Gets a worker
    /// </summary>
    /// <param name="id">The id of the worker</param>
    /// <returns>The worker</returns>
    /// <response code="404">If the worker was not found</response>
    /// <response code="200">Returns the worker</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetWorkerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetWorkerDto>> GetWorker(int id)
    {
        var worker = await service.GetWorkerById(id);

        if (worker == null) {
            return NotFound();
        } 

        return worker;
    }

    /// <summary>
    /// Creates a new worker
    /// </summary>
    /// <param name="dto">The worker to create</param>
    /// <returns>The created worker</returns>
    /// <response code="201">Returns the created worker</response>
    /// <response code="400">Bad request</response>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/worker
    ///     {
    ///          "firstName": "Brett",
    ///          "lastName": "Relhok",
    ///          "position": "Software Engineer"
    ///     }
    ///     
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(GetWorkerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetWorkerDto>> PostWorker(PostWorkerDto dto)
    {
        var worker = await service.CreateWorker(dto); 

        return CreatedAtAction("GetWorker", new { id = worker.Id }, worker); 
    }

    /// <summary>
    /// Updates a worker
    /// </summary>
    /// <param name="id">The id of the worker to update</param>
    /// <param name="dto">The updated worker's information</param>
    /// <returns>The updated worker</returns>
    /// <response code="404">Worker was not found</response>
    /// <response code="204">The worker was updated</response>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/worker
    ///     {
    ///          "firstName": "Brett",
    ///          "lastName": "Relhok",
    ///          "position": "Cook"
    ///     }     
    ///     
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutWorker(int id, PutWorkerDto dto)
    {
        var worker = await service.FindWorker(id);

        if (worker == null)
        {
            return NotFound();
        }

        await service.UpdateWorker(dto, worker); 
        return NoContent();
    }

    /// <summary>
    /// Deletes a worker 
    /// </summary>
    /// <param name="id">The id of the worker to delete</param>
    /// <returns></returns>
    /// <response code="404">If the worker was not found</response>
    /// <response code="204">If the worker was deleted</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWorker(int id)
    {
        var worker = await service.FindWorker(id);
        if (worker == null)
        {
            return NotFound();
        }

        await service.DeleteWorker(worker); 

        return NoContent();
    }
}