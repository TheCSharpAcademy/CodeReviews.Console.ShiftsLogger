using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController: ControllerBase {
    private readonly WorkerService service; 
    public WorkerController(WorkerService workerService)
    {
        service = workerService;
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetWorkerDto>>> GetWorkers()
    {
        var workers = await service.GetWorkers();

        return Ok(workers);     
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetWorkerDto>> GetWorker(int id)
    {
        var worker = await service.GetWorkerById(id);

        if (worker == null) {
            return NotFound();
        } 

        return worker;
    }

    [HttpPost]
    public async Task<ActionResult<GetWorkerDto>> PostWorker(PostWorkerDto dto)
    {
        var worker = await service.CreateWorker(dto); 

        return CreatedAtAction("GetWorker", new { id = worker.Id }, worker); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorker(int id, PutWorkerDto dto)
    {
        var worker = await service.FindWorker(id);

        if (worker == null)
        {
            return NotFound();
        }

        await service.UpdateWorker(dto, worker); 
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorker(int id)
    {
        var worker = await service.FindWorker(id);
        if (worker == null)
        {
            return NotFound();
        }

        await service.DeleteWorker(worker); 

        return NoContent();
    }
}