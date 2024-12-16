using Microsoft.AspNetCore.Mvc;
using WokersAPI.Services.WorkerShiftServices;

namespace WokersAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkerController : ControllerBase
{
    private readonly IWorkerShiftService _workerShift;
    public WorkerController(IWorkerShiftService workerShift)
    {
        _workerShift = workerShift;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkerShift>>> GetAll()
    {
        return await _workerShift.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkerShift>> GetBySuperHeroId(int id)
    {
        var results = await _workerShift.GetBySuperHeroId(id);
        if (results is null)
            return NotFound();
        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult<List<WorkerShift>>> AddWorker(WorkerShift worker)
    {
        var results = await _workerShift.AddWorker(worker);
        if (results is null)
            return NotFound();
        return Ok(results);
    }

    [HttpPut]
    public async Task<ActionResult<List<WorkerShift>>> UpdateWorker(WorkerShift request)
    {
        var results = await _workerShift.UpdateWorker(request);
        if (results is null)
            return NotFound();
        return Ok(results);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<WorkerShift>>> Delete(int id)
    {
        var results = await _workerShift.Delete(id);
        if (results is null)
            return NotFound();
        return Ok(results);
    }

}
