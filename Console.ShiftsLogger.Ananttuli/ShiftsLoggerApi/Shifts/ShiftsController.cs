using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi;
using ShiftsLoggerApi.Shifts.Models;
using ShiftsLoggerApi.Util;

namespace ShiftsLoggerApi.Shifts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly ShiftsLoggerContext Db;
        private readonly ShiftsService ShiftsService;

        public ShiftsController(ShiftsLoggerContext dbContext, ShiftsService shiftsService)
        {
            Db = dbContext;
            ShiftsService = shiftsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
        {
            return await Db.Shifts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDto>> GetShift(int id)
        {
            var shift = await Db.Shifts
                .Where(s => s.ShiftId == id)
                .Where(s => s.Employee != null)
                .Select(s =>
                    new ShiftDto(
                        s.ShiftId,
                        s.StartTime,
                        s.EndTime,
                        s.Employee == null ? null :
                            new Employees.Models.EmployeeDto(s.Employee!.EmployeeId, s.Employee.Name, null)
                    )
                )
                .FirstOrDefaultAsync();

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Shift>> PutShift(int id, ShiftUpdateDto shiftUpdateDto)
        {
            if (id != shiftUpdateDto.ShiftId)
            {
                return BadRequest(
                    new Error(
                        ErrorType.BusinessRuleValidation,
                        "Param ID does not match payload ID"
                    )
                );
            }

            var (updatedShift, error) = await ShiftsService.UpdateShift(shiftUpdateDto);

            if (error == null && updatedShift != null)
            {
                return updatedShift;
            }

            return error?.Type switch
            {
                Util.ErrorType.BusinessRuleValidation => BadRequest(error),
                Util.ErrorType.DatabaseNotFound => NotFound(),
                _ => StatusCode(StatusCodes.Status500InternalServerError, error)
            };

        }

        [HttpPost]
        public async Task<ActionResult<Shift>> PostShift(ShiftCreateDto shiftCreateDto)
        {
            var (createdShift, error) = await ShiftsService.CreateShift(shiftCreateDto);

            if (error == null && createdShift != null)
            {
                return CreatedAtAction("GetShift", new { id = createdShift.ShiftId }, createdShift);
            }

            if (error?.Type == Util.ErrorType.BusinessRuleValidation)
            {
                return BadRequest(error);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, error?.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var shift = await Db.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            Db.Shifts.Remove(shift);
            await Db.SaveChangesAsync();

            return NoContent();
        }
    }
}
