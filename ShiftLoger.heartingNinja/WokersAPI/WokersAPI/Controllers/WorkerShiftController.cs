using Microsoft.AspNetCore.Mvc;

namespace WokersAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkerController : ControllerBase
{
    private readonly DataContext context;

    public WorkerController(DataContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkerShift>>> Get()
    {
        return Ok(await context.WorkerShift.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkerShift>> GetBySuperHeroId(int id)
    {
        var worker = await context.WorkerShift
            .Where(w => w.SuperHeroId == id)
            .OrderByDescending(w => w.Id)
            .FirstOrDefaultAsync();

        if (worker == null)
            return BadRequest("Worker not found.");

        return Ok(worker);
    }

    [HttpPost]
    public async Task<ActionResult<List<WorkerShift>>> AddWorker(WorkerShift worker)
    {
        context.WorkerShift.Add(worker);
        await context.SaveChangesAsync();

        return Ok(await context.WorkerShift.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<WorkerShift>>> UpdateWorker(WorkerShift request)
    {
        var dbWorker = await context.WorkerShift.FindAsync(request.Id);
        if (dbWorker == null)
            return BadRequest("Worker not found.");

        dbWorker.LoginTime = request.LoginTime;
        dbWorker.LogoutTime = request.LogoutTime;

        await context.SaveChangesAsync();

        return Ok(await context.WorkerShift.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<WorkerShift>>> Delete(int id)
    {
        var workers = await context.WorkerShift
            .Where(w => w.SuperHeroId == id)
            .ToListAsync();

        if (workers.Count == 0)
            return BadRequest("No workers found.");

        context.WorkerShift.RemoveRange(workers);
        await context.SaveChangesAsync();

        return Ok(await context.WorkerShift.ToListAsync());
    }

}
