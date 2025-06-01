using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : ControllerBase
    {
        public readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateShift([FromBody] Shift shift)
        {
            await _shiftService.AddShift(shift);
            return Ok("Added succesfully!");
        }

        [HttpPut]
        public async Task<ActionResult> EditShift([FromBody] Shift shift)
        {
            await _shiftService.UpdateShift(shift);
            return Ok("Edited Succesfully!");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteShiftAsync(int id)
        {
            await _shiftService.DeleteShift(id);
            return Ok("Removed Succesfully");
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var shifts = await _shiftService.GetShifts();
            return Ok(shifts);
        }
    }
}