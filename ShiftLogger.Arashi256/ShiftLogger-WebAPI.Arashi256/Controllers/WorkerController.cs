using Microsoft.AspNetCore.Mvc;
using ShiftLogger_Shared.Arashi256.Models;
using ShiftLogger_Shared.Arashi256.Classes;
using ShiftLogger_WebAPI.Arashi256.Services;

namespace ShiftLogger_WebAPI.Arashi256.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerService _workerService;

        public WorkerController(WorkerService service)
        {
            _workerService = service;
        }

        // GET: api/Worker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerOutputDto>>> GetWorkers()
        {
            // Call the service method to get Workers with automatically generated DisplayIds
            var response = await _workerService.GetAllWorkersWithDisplayIdsAsync();
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    var workers = response.Data as List<WorkerOutputDto>;
                    return Ok(workers);
                case ResponseStatus.Failure:
                    return NotFound(response.Message);
                default:
                    return StatusCode(500, response.Message);
            }
        }

        // GET: api/Worker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerOutputDto>> GetWorker(int id)
        {
            // Call service to get single Worker by it's id
            var response = await _workerService.GetWorkerByIdAsync(id);
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    var worker = response.Data as WorkerOutputDto;
                    return Ok(worker);
                case ResponseStatus.Failure:
                    return NotFound(response.Message);
                default:
                    return StatusCode(500, response.Message);
            }
        }

        // PUT: api/Worker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(int id, WorkerInputDto workerDto)
        {
            // Call service to update the Worker
            var response = await _workerService.UpdateWorkerAsync(id, workerDto);
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.Failure:
                    return BadRequest(response.Message);
                default:
                    return StatusCode(500, response.Message);
            }
        }

        // POST: api/Worker
        [HttpPost]
        public async Task<ActionResult<WorkerOutputDto>> PostWorker(WorkerInputDto workerDto)
        {
            // Use the service to add a Worker
            var response = await _workerService.AddWorkerAsync(workerDto);
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    // Cast the response.Data to Worker
                    var worker = response.Data as WorkerOutputDto;
                    if (worker != null) 
                        return CreatedAtAction(nameof(GetWorker), new { id = worker.Id }, worker);
                    else
                        return StatusCode(500, response.Message);
                case ResponseStatus.Failure:
                    return BadRequest(response.Message);
                default:
                    return StatusCode(500, response.Message);
            }
        }

        // DELETE: api/Worker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            // Use the service to delete the Worker
            var response = await _workerService.DeleteWorkerAsync(id);
            switch (response.Status)
            { 
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.Failure:
                    return BadRequest(response.Message);
                default:
                    return StatusCode(500, response.Message);
            }
        }
    }
}
