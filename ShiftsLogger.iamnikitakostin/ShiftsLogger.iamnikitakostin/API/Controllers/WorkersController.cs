using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/worker/[controller]")]
public class WorkersController : ControllerBase
{
    private readonly IWorkerService _workerService;

    public WorkersController(IWorkerService workerService)
    {
        _workerService = workerService;
    }

    [HttpGet]
    public ActionResult<List<Worker>> GetAllWorkers()
    {
        return Ok(_workerService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Worker> GetWorker(int id)
    {
        var result = _workerService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Worker> CreateWorker(Worker worker)
    {
        var result = _workerService.Add(worker);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    public ActionResult<Worker> UpdateWorker(Worker worker)
    {
        var result = _workerService.Update(worker);

        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult<Worker> DeleteWorker(int id)
    {
        var result = _workerService.Delete(id);

        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }
}
