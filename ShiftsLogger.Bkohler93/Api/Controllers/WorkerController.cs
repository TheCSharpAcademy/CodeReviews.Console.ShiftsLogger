using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController: ControllerBase {
    private readonly AppDbContext db;
    public WorkerController(AppDbContext appDbContext)
    {
        db = appDbContext; 
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetWorkerDto>>> GetWorkers()
    {
        var workers = await db.Workers.ToListAsync();

        return workers.Select(w => new GetWorkerDto{
            Id = w.Id,
            FirstName = w.FirstName,
            LastName = w.LastName,
            Position = w.Position,
        }).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetWorkerDto>> GetWorker(int id)
    {
        var worker = await db.Workers.FindAsync(id);

        if (worker == null) {
            return NotFound(); 
        }

        return new GetWorkerDto{
            Id = worker.Id,
            FirstName = worker.FirstName,
            LastName = worker.LastName,
            Position = worker.Position
        };
    }

    [HttpPost]
    public async Task<ActionResult<GetWorkerDto>> PostWorker(PostWorkerDto dto)
    {
        var worker = new Worker {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Position = dto.Position,
            WorkerShifts = [],
        };

        db.Workers.Add(worker);
        await db.SaveChangesAsync();

        var returnDto = new GetWorkerDto{
            Id = worker.Id,
            FirstName = worker.FirstName,
            LastName = worker.LastName,
            Position = worker.Position,
        };

        return CreatedAtAction("GetWorker", new { id = worker.Id }, returnDto); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorker(int id, PutWorkerDto dto)
    {
        var worker = await db.Workers.FindAsync(id);

        if (worker == null)
        {
            return NotFound();
        }

        worker.FirstName = dto.FirstName;
        worker.LastName = dto.LastName;
        worker.Position = dto.Position;

        db.Entry(worker).State = EntityState.Modified;
        await db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorker(int id)
    {
        var worker = await db.Workers.FindAsync(id);
        if (worker == null)
        {
            return NotFound();
        }

        db.Workers.Remove(worker);
        await db.SaveChangesAsync();

        return NoContent();
    }
}