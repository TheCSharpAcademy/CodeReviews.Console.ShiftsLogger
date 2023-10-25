using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.API.Data;
using ShiftsLogger.API.DTOs.Worker;
using ShiftsLogger.API.Models;
using ShiftsLogger.API.Services;

namespace ShiftsLogger.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly WorkersService _workersService;

    public WorkersController(ApplicationDbContext context, WorkersService workersService)
    {
        _context = context;
        _workersService = workersService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWorkers()
    {
        var workers = await _workersService.GetAllWorkersAsync();

        return Ok(workers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkerById(int id)
    {
        var workerDTO = await _workersService.GetWorkerByIdAsync(id);
        if (workerDTO == null) return NotFound();

        return Ok(workerDTO);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorker(AddWorkerDTO newWorker)
    {
        Worker? worker = await _workersService.CreateWorkerAsync(newWorker);
        if (worker == null) return BadRequest();

        return CreatedAtAction(nameof(GetWorkerById), new { Id = worker.Id }, worker);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkerById(int id, UpdateWorkerDTO updateWorker)
    {
        var worker = await _workersService.UpdateWorkerAsync(id, updateWorker);
        if (worker == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkerById(int id)
    {
        var worker = await _workersService.DeleteWorkerAsync(id);
        if (worker == null) return NotFound();

        return NoContent();
    }

    // Get all shifts by worker id
    [HttpGet("{id}/shifts")]
    public async Task<IActionResult> GetWorkerShiftsById(int id)
    {
        var shifts = await _workersService.GetWorkerShiftsAsync(id);

        return Ok(shifts);
    }
}
