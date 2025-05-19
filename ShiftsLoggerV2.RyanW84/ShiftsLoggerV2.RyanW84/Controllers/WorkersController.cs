using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using ShiftsLoggerV2.RyanW84.Services;

namespace ShiftsLoggerV2.RyanW84.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkersController(IWorkerService workerService) : ControllerBase
{
    [HttpGet(Name = "Get All Workers")]
    public async Task<ActionResult<ApiResponseDto<List<Workers>>>> GetAllWorkers(
        WorkerFilterOptions workerOptions
    )
    {
        try
        {
            return Ok(await workerService.GetAllWorkers(workerOptions));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Get all workers failed, see Exception {ex}");

            throw;
        }
    }

    // This is the route for getting a worker by ID
    [HttpGet("{id}")] // This will be added to the API URI (send some data during the request
    public async Task<ActionResult<Workers>> GetWorkerById(int id)
    {
        try
        {
            var result = await workerService.GetWorkerById(id);

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
    public async Task<ActionResult<Workers>> CreateWorker(WorkerApiRequestDto worker)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return new ObjectResult(await workerService.CreateWorker(worker))
                {
                    StatusCode = 201,
                }; //201 is the status code for Created
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Create worker failed, see Exception {ex}");
            throw;
        }
    }

    // This is the route for updating a createdShift
    [HttpPut("{id}")]
    public async Task<ActionResult<Workers>> UpdateWorker(int id, WorkerApiRequestDto updatedWorker)
    {
        try
        {
            var result = await workerService.UpdateWorker(id, updatedWorker);

            if (result is null)
            {
                return NotFound(); // Equivalent to 404
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update worker failed, see Exception {ex}");
            throw;
        }
    }

    // Fix for CS0019 and CS1525 errors in the DeleteShift method
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteShift(int id)
    {
        try
        {
            var result = await workerService.DeleteWorker(id);

            // Corrected the condition to check the ResponseCode property of the result
            if (result.ResponseCode == HttpStatusCode.NotFound || result is null)
            {
                return NotFound();
            }

            return NoContent(); // Equivalent to 204
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Delete worker failed, see Exception {ex}");
            throw;
        }
    }
}
