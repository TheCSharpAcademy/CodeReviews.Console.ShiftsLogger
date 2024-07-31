using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerShiftController: ControllerBase {
    private readonly AppDbContext db;
    public WorkerShiftController(AppDbContext appDbContext)
    {
        db = appDbContext; 
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetWorkerShiftDto>>> GetWorkerShifts()
    {
        var workerShifts = await db.WorkerShifts
            .Include(ws => ws.Worker)
            .Include(ws => ws.Shift)
            .ToListAsync();

        return workerShifts.Select(ws => new GetWorkerShiftDto{
            Id = ws.Id,
            WorkerId = ws.WorkerId,
            ShiftId = ws.ShiftId,
            ShiftDate = ws.ShiftDate,
            Worker = new GetWorkerDto{
                FirstName = ws.Worker!.FirstName,
                LastName = ws.Worker.LastName,
                Position = ws.Worker.Position,
                Id = ws.Worker.Id,
            },
            Shift = new GetShiftDto{
                Id = ws.Shift!.Id,
                Name = ws.Shift.Name,
                StartTime = ws.Shift.StartTime,
                EndTime = ws.Shift.EndTime,
            },
        }).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetWorkerShiftDto>> GetWorkerShift(int id)
    {
        var workerShift = await db.WorkerShifts.Include(ws => ws.Shift).Include(ws => ws.Worker).FirstOrDefaultAsync(ws => ws.Id == id);
        
        if (workerShift == null) {
            return NotFound(); 
        }

        return new GetWorkerShiftDto {
            Id = workerShift.Id,
            WorkerId = workerShift.WorkerId,
            ShiftId = workerShift.ShiftId,
            ShiftDate = workerShift.ShiftDate,
            Worker = new GetWorkerDto{
                Id = workerShift.Worker!.Id,
                FirstName = workerShift.Worker.FirstName,
                LastName = workerShift.Worker.LastName,
                Position = workerShift.Worker.Position,
            },
            Shift = new GetShiftDto{
                Id = workerShift.Shift!.Id,
                Name = workerShift.Shift.Name,
                StartTime = workerShift.Shift.StartTime,
                EndTime = workerShift.Shift.EndTime,
            }
        };
    }

    [HttpPost]
    public async Task<ActionResult<WorkerShift>> PostWorker(PostWorkerShiftDto dto)
    {
        var workerShift = new WorkerShift {
            WorkerId = dto.WorkerId,
            ShiftId = dto.ShiftId,
            ShiftDate = dto.ShiftDate,
        };

        db.WorkerShifts.Add(workerShift);
        await db.SaveChangesAsync();

        return CreatedAtAction("GetWorkerShift", new { id = workerShift.Id }, workerShift); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkerShift(int id, PutWorkerShiftDto dto)
    {
        var workerShift = await db.WorkerShifts.FindAsync(id);

        if (workerShift == null)
        {
            return NotFound();
        }

        workerShift.ShiftDate = dto.ShiftDate;
        workerShift.ShiftId = dto.ShiftId;
        workerShift.WorkerId = dto.WorkerId;

        db.Entry(workerShift).State = EntityState.Modified;
        await db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkerShift(int id)
    {
        var workerShift = await db.WorkerShifts.FindAsync(id);
        if (workerShift == null)
        {
            return NotFound();
        }

        db.WorkerShifts.Remove(workerShift);
        await db.SaveChangesAsync();

        return NoContent();
    }
}