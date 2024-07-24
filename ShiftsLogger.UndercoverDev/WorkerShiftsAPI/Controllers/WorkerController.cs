using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkerShiftsAPI.DTOs;
using WorkerShiftsAPI.Models;

namespace WorkerShiftsAPI.Controllers;

[Route("api/workers")]
[ApiController]
/// <summary>
/// Provides a controller for managing workers and their shifts in the WorkerShiftsAPI application.
/// </summary>
public class WorkerController : ControllerBase
{
    private readonly WorkerShiftContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkerController"/> class.
    /// </summary>
    /// <param name="context">The worker shift context.</param>
    public WorkerController(WorkerShiftContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a list of all workers and their associated shifts.
    /// </summary>
    /// <returns>A list of <see cref="WorkerDTO"/> objects representing the workers and their shifts.</returns>
    // GET: api/workers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkerDTO>>> GetWorkers()
    {
        var workers = await _context.Workers.Include(w => w.Shifts).ToListAsync();
        var workerDTOs = workers.Select(worker => new WorkerDTO
        {
            WorkerId = worker.WorkerId,
            Name = worker.Name,
            Shifts = worker.Shifts?.Select(ShiftController.ShiftToDTO).ToList() ?? []
        }).ToList();

        return workerDTOs;
    }

    /// <summary>
    /// Gets a specific worker and their associated shifts.
    /// </summary>
    /// <param name="id">The ID of the worker to retrieve.</param>
    /// <returns>A <see cref="WorkerDTO"/> object representing the worker and their shifts.</returns>
    // GET: api/workers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkerDTO>> GetWorker(int id)

    {
        var worker = await _context.Workers
            .Include(w => w.Shifts)
            .FirstOrDefaultAsync(w => w.WorkerId == id);

        if (worker == null)
        {
            return NotFound();
        }

        var workerDTO = new WorkerDTO
        {
            WorkerId = worker.WorkerId,
            Name = worker.Name,
            Shifts = worker.Shifts?.Select(ShiftController.ShiftToDTO).ToList() ?? []
        };

        return workerDTO;
    }

    /// <summary>
    /// Updates an existing worker and their associated shifts.
    /// </summary>
    /// <param name="id">The ID of the worker to update.</param>
    /// <param name="workerDTO">The updated worker information.</param>
    /// <returns>A no-content response if the update is successful.</returns>
    // PUT: api/workers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorker(int id, WorkerDTO workerDTO)
    {
        if (id != workerDTO.WorkerId)
        {
            return BadRequest();
        }

        var worker = await _context.Workers.FindAsync(id);
        if (worker == null)
        {
            return NotFound();
        }

        worker.Name = workerDTO.Name;
        worker.Shifts = workerDTO.Shifts?.Select(shiftDTO => new Shift
        {
            StartTime = shiftDTO.StartTime,
            EndTime = shiftDTO.EndTime,
            WorkerId = shiftDTO.WorkerId
        }).ToList() ?? [];

        _context.Entry(worker).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkerExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Creates a new worker and their associated shifts.
    /// </summary>
    /// <param name="workerDTO">The new worker information.</param>
    /// <returns>The created <see cref="WorkerDTO"/> object.</returns>
    // POST: api/workers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WorkerDTO>> PostWorker(WorkerDTO workerDTO)
    {
        var worker = new Worker
        {
            Name = workerDTO.Name,
            Shifts = workerDTO.Shifts?.Select(shiftDTO => new Shift
            {
                StartTime = shiftDTO.StartTime,
                EndTime = shiftDTO.EndTime,
                WorkerId = shiftDTO.WorkerId
            }).ToList() ?? []
        };

        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();

        workerDTO.WorkerId = worker.WorkerId;

        return CreatedAtAction(nameof(GetWorker), new { id = workerDTO.WorkerId }, WorkerToDTO(worker));
    }

    /// <summary>
    /// Deletes a worker and their associated shifts.
    /// </summary>
    /// <param name="id">The ID of the worker to delete.</param>
    /// <returns>A no-content response if the deletion is successful.</returns>
    // DELETE: api/workers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorker(int id)
    {
        var worker = await _context.Workers.FindAsync(id);
        if (worker == null)
        {
            return NotFound();
        }

        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkerExists(int id)
    {
        return _context.Workers.Any(e => e.WorkerId == id);
    }

    private static WorkerDTO WorkerToDTO(Worker worker) =>
    new()
    {
        WorkerId = worker.WorkerId,
        Name = worker.Name,
        Shifts = worker.Shifts?.Select(ShiftController.ShiftToDTO).ToList() ?? []
    };
}

