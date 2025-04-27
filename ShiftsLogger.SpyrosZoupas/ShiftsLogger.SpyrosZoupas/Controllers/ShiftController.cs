using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.SpyrosZoupas.DAL.Model;
using ShiftsLogger.SpyrosZoupas.Services;

namespace ShiftsLogger.SpyrosZoupas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;
        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpPost]
        public ActionResult<Shift> CreateShift(Shift shift) =>
            Ok(_shiftService.CreateShift(shift));

        [HttpGet]
        public ActionResult<List<Shift>> GetAllShifts() =>
            Ok(_shiftService.GetAllShifts());

        [HttpGet("{id}")]
        public ActionResult<Shift> GetShiftById(int id)
        {
            Shift? result = _shiftService.GetShiftById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteShift(int id)
        {
            Shift? result = _shiftService.GetShiftById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<Shift> UpdateShift(Shift shift)
        {
            Shift? result = _shiftService.GetShiftById(shift.ShiftId);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
