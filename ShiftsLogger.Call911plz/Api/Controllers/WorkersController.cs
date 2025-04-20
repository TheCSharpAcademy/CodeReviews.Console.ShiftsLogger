using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WorkersController : ControllerBase
{
    private readonly IWorkerService _workerService;    
    public WorkersController(IWorkerService WorkerService)
    {
        _workerService = WorkerService;
    }

    [HttpGet]
    public ActionResult<List<Worker>> GetAllWorkers()
    {
        var result = _workerService.GetAllWorkers();
        return Ok(result);
    }

    [HttpGet("{workerId}")]
    public ActionResult<Worker> GetWorkerById(int workerId)
    {
        var result = _workerService.GetWorkerById(workerId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Worker> CreateWorker(WorkerDto workerDto)
    {
        Worker worker = new() {
            WorkerName = workerDto.WorkerName,
            WorkerId = workerDto.WorkerId,
        };
        var result = _workerService.CreateWorker(worker);
        return Ok(result);
    }

    [HttpPut]
    public ActionResult<Worker> UpdateWorker(WorkerDto workerDto)
    {
        Worker worker = new() {
            WorkerName = workerDto.WorkerName,
            WorkerId = workerDto.WorkerId,
        };

        var result = _workerService.UpdateWorker(worker);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{workerId}")]
    public ActionResult<string> DeleteWorker(int workerId)
    {
        var result = _workerService.DeleteWorker(workerId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}