using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Lonchanick.Models;
using ShiftsLogger.Lonchanick.Services;

namespace ShiftsLogger.Lonchanick.Controllers;

[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
    protected readonly IWorkerService workerService;

    public WorkerController(IWorkerService workerService)
    {
        this.workerService = workerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkers()
    {
        return Ok(await workerService.GetWorkers());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorker(int id)
    {
        var response = await workerService.GetWorker(id);
        if(response!=null) return Ok(response);
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> NewWorker([FromBody] Worker worker)
    {
        await workerService.SaveWorker(worker);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Worker worker)
    {
        await workerService.UpdateWorker(id,worker);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await workerService.DeleteWorker(id);
        return Ok();
    }

}
