using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Dejmenek.API.Data.Interfaces;
using ShiftsLogger.Dejmenek.API.Models;

namespace ShiftsLogger.Dejmenek.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : Controller
{
    private readonly IShiftsRepository _shiftsRepository;
    private readonly IEmployeesRepository _employeesRepository;

    public ShiftsController(IShiftsRepository shiftsRepository, IEmployeesRepository employeesRepository)
    {
        _shiftsRepository = shiftsRepository;
        _employeesRepository = employeesRepository;
    }

    //GET: /api/shifts
    [HttpGet]
    public async Task<IActionResult> GetShiftsAsync()
    {
        var shifts = await _shiftsRepository.GetShiftsAsync();

        return Ok(shifts);
    }

    //DELETE: /api/shifts/5
    [HttpDelete("{shiftId}")]
    public async Task<IActionResult> DeleteShiftAsync(int shiftId)
    {
        var result = await _shiftsRepository.DeleteShiftAsync(shiftId);

        if (result == -1)
        {
            return NotFound();
        }

        return NoContent();
    }

    //PUT: /api/shifts/5
    [HttpPut("{shiftId}")]
    public async Task<IActionResult> PutShiftAsync(int shiftId, [FromBody] ShiftUpdateDTO shift)
    {
        var result = await _shiftsRepository.UpdateShiftAsync(shiftId, shift);

        if (result == -1)
        {
            return NotFound();
        }

        return NoContent();
    }

    //POST: /api/shifts
    [HttpPost]
    public async Task<IActionResult> PostShiftAsync([FromBody] ShiftCreateDTO shift)
    {
        if (!await _employeesRepository.EmployeeExists(shift.EmployeeId))
        {
            return NotFound();
        }

        await _shiftsRepository.AddShiftAsync(shift);

        return NoContent();
    }

}
