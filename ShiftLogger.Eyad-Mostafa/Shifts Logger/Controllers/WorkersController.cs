using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shifts_Logger.DTOs;
using Shifts_Logger.Models;
using Shifts_Logger.Services;

namespace Shifts_Logger.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkersController : ControllerBase
{
    private readonly IWorkerService _workerService;
    private readonly IMapper _mapper;

    public WorkersController(IWorkerService context, IMapper mapper)
    {
        _workerService = context;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult GetWorkers()
    {
        var workers = _workerService.GetWorkers();
        return Ok(workers);
    }

    [HttpGet("{id}")]
    public ActionResult GetWorker(int id)
    {
        var worker = _workerService.GetWorker(id);
        if (worker == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<WorkerDto>(worker));
    }

    [HttpPost]
    public ActionResult AddWorker([FromBody] CreateWorkerDto workerDto)
    {
        var worker = _mapper.Map<Worker>(workerDto);
        _workerService.AddWorker(worker.Name);
        return CreatedAtAction(nameof(GetWorker), new { id = worker.Id }, worker);
    }


    [HttpPut]
    public ActionResult UpdateWorker(int id, [FromBody] CreateWorkerDto workerDto)
    {
        var worker = _mapper.Map<Worker>(workerDto);
        _workerService.UpdateWorker(id, worker.Name);
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeleteWorker(int id)
    {
        _workerService.DeleteWorker(id);
        return Ok();
    }
}
