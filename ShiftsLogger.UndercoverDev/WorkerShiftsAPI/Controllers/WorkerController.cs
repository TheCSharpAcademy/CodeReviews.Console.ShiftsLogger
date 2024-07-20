using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkerShiftsAPI.DTOs;
using WorkerShiftsAPI.Models;

namespace WorkerShiftsAPI.Controllers
{
    [Route("api/workers")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerShiftContext _context;

        public WorkerController(WorkerShiftContext context)
        {
            _context = context;
        }

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

        // PUT: api/workers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(int id, WorkerDTO workerDTO)
        {
            if (id != workerDTO.WorkerId)
            {
                return BadRequest();
            }

            var worker = new Worker
            {
                WorkerId = workerDTO.WorkerId,
                Name = workerDTO.Name,
                Shifts = workerDTO.Shifts?.Select(shiftDTO => new Shift
                {
                    ShiftId = shiftDTO.ShiftId,
                    StartTime = shiftDTO.StartTime,
                    EndTime = shiftDTO.EndTime,
                    WorkerId = shiftDTO.WorkerId
                }).ToList() ?? []
            };

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
            Name = worker.Name
        };
    }
}
