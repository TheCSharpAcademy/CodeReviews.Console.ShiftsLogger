using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Backend.Data;
using ShiftsLogger.Domain;
using System.Threading;

namespace ShiftsLogger.Backend.Controller;

[ApiController]
[Route("/api/shifts")]
public class ShiftController : ControllerBase
{
    private readonly ILogger<ShiftController> _log;
    private readonly IRepository<Shift> _repository;

    public ShiftController(ILogger<ShiftController> log, IRepository<Shift> repository)
    {
        _log = log;
        _repository = repository;
    }


    [HttpGet()]
    public async Task<ActionResult<List<Shift>>> GetAllShifts(CancellationToken cancellationToken)
    {
        var shifts = await _repository.GetAllAsync(cancellationToken);

        return shifts;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Shift>> GetShift(int id,CancellationToken cancellationToken)
    {
        var shift = await _repository.GetByIdAsync(id,cancellationToken);
        if (shift == null)
        {
            return NotFound();
        }
        return shift;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShift(Shift shift, CancellationToken cancellationToken)
    {
         await _repository.CreateAsync(shift, cancellationToken);
        return Ok(shift);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateShift(Shift shift,CancellationToken cancellationToken)
    {
        bool result = await _repository.UpdateAsync(shift, cancellationToken);
        if (!result)
        {
            _log.LogWarning("Shift with {Id} was not found!", shift.Id);

            return NotFound($"Shift with {shift.Id} was not found!");
        }

        return Ok(shift);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id, CancellationToken cancellationToken)
    {
        bool result = await _repository.DeleteAsync(id, cancellationToken);
        if (!result)
        {
            _log.LogWarning("Shift with {Id} was not found!", id);
            return NotFound($"Shift with {id} was not found!");
        }

        return Ok(result);
    }
}
