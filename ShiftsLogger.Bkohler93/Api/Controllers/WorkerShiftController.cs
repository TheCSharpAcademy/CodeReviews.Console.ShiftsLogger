using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerShiftController: ControllerBase {
    private readonly WorkerShiftService service;
    public WorkerShiftController(WorkerShiftService workerShiftService)
    {
        service = workerShiftService;
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetWorkerShiftDto>>> GetWorkerShifts()
    {
        var workerShifts = await service.GetWorkerShifts();

        return Ok(workerShifts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetWorkerShiftDto>> GetWorkerShift(int id)
    {
       var workerShift = await service.GetWorkerShift(id); 

       if (workerShift == null) {
            return NotFound();
       }

        return Ok(workerShift); 
    }

    [HttpPost]
    public async Task<ActionResult<WorkerShift>> PostWorker(PostWorkerShiftDto dto)
    {
        var workerShift = await service.CreateWorkerShift(dto); 

        return CreatedAtAction("GetWorkerShift", new { id = workerShift.Id }, workerShift); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkerShift(int id, PutWorkerShiftDto dto)
    {
        var workerShift = await service.FindWorkerShiftEntity(id);

        if (workerShift == null)
        {
            return NotFound();
        }

        await service.UpdateWorkerShift(workerShift, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkerShift(int id)
    {
        var workerShift = await service.FindWorkerShiftEntity(id);
        if (workerShift == null)
        {
            return NotFound();
        }

        await service.DeleteWorkerShift(workerShift);

        return NoContent();
    }
}