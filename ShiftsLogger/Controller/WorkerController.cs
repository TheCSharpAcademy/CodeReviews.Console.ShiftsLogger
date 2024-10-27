using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerAPI.Models;
using ShiftsLoggerAPI.Services;
using System.Text.RegularExpressions;

namespace ShiftsLoggerAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;
        public readonly Regex EmailValidation = new(@"^[a-zA-Z0-9._%+-]{2,}@[a-zA-Z]+\.[a-zA-Z]{2,}$");
        private bool ValidateEmail(string email)
        {
            return EmailValidation.IsMatch(email);
        }
        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }
        [HttpPost]
        public async Task<IActionResult> AddWorkerAsync([FromBody] WorkerCreate worker)
        {
            if (worker == null)
                return BadRequest("Worker data is null. Check worker data");
            if (worker.HireDate == DateTime.MinValue || worker.HireDate > DateTime.Now)
                return BadRequest("Please Check the Hire Date");
            if (!ValidateEmail(worker.Email))
                return BadRequest("Invalid Email address. please check email has correct domain and top level domain name and atleast 2 characters before @ sign ");
            await _workerService.AddWorkerAsync(worker);

            // Optionally return CreatedAtAction to return the created worker's URL
            return Created();
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllWorkersAsync()
        {
            var workers = await _workerService.GetAllWorkersAsync();
            return Ok(workers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkerAsync(int id)
        {
            Worker worker = await _workerService.GetWorkerAsync(id);
            if (worker == null)
            {
                return NotFound($"Couldn't Find Worker with ID: {id}");
            }
            return Ok(worker);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateWorkerAsync([FromBody] WorkerUpdate newWorker)
        {
            if (!string.IsNullOrEmpty(newWorker.Email) && !ValidateEmail(newWorker.Email))
            {
                return ValidationProblem("Invalid Email");
            }
            
            bool success = await _workerService.UpdateWorkerAsync(newWorker);
            return success ? NoContent() : NotFound($"Couldnt Find worker of ID: {newWorker.Id} to update");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkerAsync(int id)
        {
            bool success = await _workerService.RemoveWorkerAsync(id);

            return success ? NoContent() : NotFound($"Couldnt Find worker of ID: {id} to update");
        }
    }
}
