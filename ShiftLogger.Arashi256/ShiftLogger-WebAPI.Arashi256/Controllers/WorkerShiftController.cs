using Microsoft.AspNetCore.Mvc;
using ShiftLogger_Shared.Arashi256.Models;
using ShiftLogger_Shared.Arashi256.Classes;
using ShiftLogger_WebAPI.Arashi256.Services;
using ShiftLogger_WebAPI.Arashi256.Models;

namespace ShiftLogger_WebAPI.Arashi256.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerShiftController : ControllerBase
    {
        private readonly WorkerShiftService _workerShiftService;

        public WorkerShiftController(WorkerShiftService workerShiftService)
        {
            _workerShiftService = workerShiftService;
        }

        // GET: api/WorkerShift
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerShiftOutputDto>>> GetWorkerShifts()
        {
            // Call the service method to get WorkerShifts with DisplayId
            var response = await _workerShiftService.GetAllWorkerShiftsWithDisplayIdsAsync();
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    // Cast the response.Data to WorkerShift Collection.
                    var workerShifts = response.Data as List<WorkerShiftOutputDto>;
                    return Ok(workerShifts);
                case ResponseStatus.Failure:
                    return NotFound(response.Message);
                default:
                    return StatusCode(500, response.Message);
            }
        }

        // GET: api/WorkerShift/worker/{workerId}
        [HttpGet("worker/{workerId}")]
        public async Task<ActionResult<IEnumerable<WorkerShiftOutputDto>>> GetWorkerShiftsForWorker(int workerId)
        {
            // Call the service method to get all WorkerShifts with DisplayId for a given Worker
            var response = await _workerShiftService.GetAllWorkerShiftsForWorkerAsync(workerId);
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    // Cast the response.Data to WorkerShift Collection.
                    var workerShifts = response.Data as List<WorkerShiftOutputDto>;
                    return Ok(workerShifts);
                case ResponseStatus.Failure:
                    return NotFound(response.Message);
                default:
                    return StatusCode(500, response.Message);
            }
        }

        // POST: api/WorkerShift
        [HttpPost]
        public async Task<ActionResult<WorkerShift>> PostWorkerShift(WorkerShiftInputDto workerShiftDto)
        {
            // Call the service method to add a new WorkerShift
            var response = await _workerShiftService.AddWorkerShiftAsync(workerShiftDto);
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    // Cast the response.Data to WorkerShiftOutputDto
                    var workerShift = response.Data as WorkerShiftOutputDto;
                    if (workerShift != null)
                    {
                        return CreatedAtAction(nameof(GetWorkerShift), new { id = workerShift.Id }, workerShift);
                    }
                    else
                        return StatusCode(500, "Could not cast response to WorkerShift");
                case ResponseStatus.Failure:
                    return BadRequest(response.Message);
                default:
                    return StatusCode(500, response.Message);
            }
        }

        // PUT: api/WorkerShift/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkerShift(int id, WorkerShiftInputDto workerShiftDto)
        {
            // Call the service method to update an existing WorkerShift
            var response = await _workerShiftService.UpdateWorkerShiftAsync(id, workerShiftDto);
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.Failure:
                    return BadRequest(response.Message);
                default:
                    return StatusCode(500, "Unexpected error");
            }
        }

        // DELETE: api/WorkerShift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkerShift(int id)
        {
            // Call the service method to delete an existing WorkerShift
            var response = await _workerShiftService.DeleteWorkerShiftAsync(id);
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.Failure:
                    return BadRequest(response.Message);
                default:
                    return StatusCode(500, "Unexpected error");
            }
        }

        // GET: api/WorkerShift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerShiftOutputDto>> GetWorkerShift(int id)
        {
            // Call the service method to a single WorkerShift via it's ID
            var response = await _workerShiftService.GetWorkerShiftAsync(id);
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    // Cast the response.Data to WorkerShift.
                    var workerShift = response.Data as WorkerShiftOutputDto;
                    return Ok(workerShift);
                case ResponseStatus.Failure:
                    return NotFound(response.Message);
                default:
                    return StatusCode(500, "Unexpected error");
            }
        }
    }
}
