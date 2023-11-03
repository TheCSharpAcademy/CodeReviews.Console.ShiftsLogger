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

    //[Route("[action]")]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(workerService.GetWorkers());
    }

    [HttpPost]
    public IActionResult NewWorker([FromBody] Worker worker)
    {
        workerService.SaveWorker(worker);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] Worker worker)
    {
        workerService.UpdateWorker(id,worker);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        workerService.DeleteWorker(id);
        return Ok();
    }

}
