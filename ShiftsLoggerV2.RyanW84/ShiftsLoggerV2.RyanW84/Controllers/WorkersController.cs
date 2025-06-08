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
    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<List<Workers>>>> GetAllWorkers(
        WorkerFilterOptions workerOptions
    )
    {
        try
        {
            var result = await workerService.GetAllWorkers(workerOptions);
            if (
                result.ResponseCode is HttpStatusCode.NotFound
                || result.ResponseCode is HttpStatusCode.NoContent
            )
            {
                return NotFound(
                    new ApiResponseDto<Workers?>
                    {
                        RequestFailed = true,
                        ResponseCode = HttpStatusCode.NotFound,
                        Message = $"Error retieving Workers",
                        Data = null,
                    }
                );
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Get all workers failed, see Exception {ex}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<Workers?>>> GetWorkerById(int id)
    {
        try
        {
            var result = await workerService.GetWorkerById(id);
            if (
                result.ResponseCode is HttpStatusCode.NotFound
                || result.ResponseCode is HttpStatusCode.NoContent
            )
            {
                return NotFound(
                    new ApiResponseDto<Workers?>
                    {
                        RequestFailed = true,
                        ResponseCode = HttpStatusCode.NotFound,
                        Message = $"Worker with ID: {id} not found.",
                        Data = null,
                    }
                );
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Get by ID failed, see Exception {ex}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<Workers>>> CreateWorker(
        WorkerApiRequestDto worker
    )
    {
        try
        {
            var result = await workerService.CreateWorker(worker);
            return CreatedAtAction(
                nameof(GetWorkerById),
                new { id = result.Data.WorkerId },
                result
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Create worker failed, see Exception {ex}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponseDto<Workers?>>> UpdateWorker(
        int id,
        WorkerApiRequestDto updatedWorker
    )
    {
        try
        {
            var result = await workerService.UpdateWorker(id, updatedWorker);
            if (result == null || result.Data == null)
            {
                return NotFound(
                    new ApiResponseDto<Workers?>
                    {
                        RequestFailed = true,
                        ResponseCode = HttpStatusCode.NotFound,
                        Message = $"Worker with ID: {id} not found.",
                        Data = null,
                    }
                );
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update worker failed, see Exception {ex}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteWorker(int id)
    {
        try
        {
            var result = await workerService.DeleteWorker(id);
           if (result.ResponseCode is HttpStatusCode.NotFound)
            {
                return NotFound();
            }
           else if (result.ResponseCode is HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            return Ok(result);
		}
        catch (Exception ex)
        {
            Console.WriteLine($"Delete worker failed, see Exception {ex}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
