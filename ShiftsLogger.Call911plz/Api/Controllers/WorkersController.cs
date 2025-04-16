using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WorkersController : ControllerBase
{
    private readonly IWorkerService _WorkerService;    
    public WorkersController(IWorkerService WorkerService)
    {
        _WorkerService = WorkerService;
    }

    [HttpGet]
    public ActionResult<List<Worker>> GetAllWorkers()
    {
        var result = _WorkerService.GetAllWorkers();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<Worker> GetWorkerById(int id)
    {
        var result = _WorkerService.GetWorkerById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Worker> CreateWorker(Worker Worker)
    {
        var result = _WorkerService.CreateWorker(Worker);
        return Ok(result);
    }

    [HttpPut]
    public ActionResult<Worker> UpdateWorker(Worker Worker)
    {
        var result = _WorkerService.UpdateWorker(Worker);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteWorker(int id)
    {
        var result = _WorkerService.DeleteWorker(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}