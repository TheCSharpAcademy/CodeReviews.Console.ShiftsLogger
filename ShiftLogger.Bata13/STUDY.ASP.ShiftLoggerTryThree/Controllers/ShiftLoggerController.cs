using Microsoft.AspNetCore.Mvc;
using STUDY.ASP.ShiftLoggerTryThree.Models;
using STUDY.ASP.ShiftLoggerTryThree.Services;

namespace STUDY.ASP.ShiftLoggerTryThree.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ShiftLoggerController : ControllerBase
    {
        private readonly IShiftLoggerService _shiftLoggerService;
        public ShiftLoggerController(IShiftLoggerService shiftLoggerService)
        {
            _shiftLoggerService = shiftLoggerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShiftLogger>>> GetAllShiftLogs()
        {
            return await _shiftLoggerService.GetAllShiftLogs();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftLogger>> GetSingleShiftLog(int id)
        {
            var result = await _shiftLoggerService.GetSingleShiftLog(id);
            if (result is null)
                return NotFound("Shift not found");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<ShiftLogger>>> AddShift(ShiftLogger shift)
        {
            var result = await _shiftLoggerService.AddShift(shift);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<ShiftLogger>>> UpdateShift(int id, ShiftLogger request)
        {
            var result = await _shiftLoggerService.UpdateShift(id, request);
            if (result is null)
                return NotFound("Shift not found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ShiftLogger>>> DeleteShift(int id)
        {
            var result = await _shiftLoggerService.DeleteShift(id);
            if (result is null)
                return NotFound("Shift not found");
            return Ok(result);
        }
    }
}
