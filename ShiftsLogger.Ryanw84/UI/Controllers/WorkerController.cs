using FrontEnd.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Ryanw84.Data;
using ShiftsLogger.Ryanw84.Models;
using ShiftsLogger.Ryanw84.Services;

namespace FrontEnd.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("worker")]
public class WorkerController(IShiftService shiftService) : ControllerBase
{
    private readonly UiWorkerService _workerService;

    [HttpGet]
    public async Task<IActionResult> GetAllWorkers()
    {
        var workers = await _workerService.GetAllWorkersAsync();
        return Ok(workers);
    }
}
