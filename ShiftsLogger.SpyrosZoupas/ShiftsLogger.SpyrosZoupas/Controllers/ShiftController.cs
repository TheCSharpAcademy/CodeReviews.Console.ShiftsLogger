using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet]
        public ActionResult<List<Shift>> GetAllShifts() =>
            Ok(_shiftService.GetAllShifts());

        [HttpGet("{id}")]
        public ActionResult<Shift> GetShiftById(int id) =>
            Ok(_shiftService.GetShiftById(id));

        [HttpPost]
        public ActionResult<Shift> CreateShift(Shift shift) =>
            Ok(_shiftService.CreateShift(shift));

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteShift(int id) =>
            Ok(_shiftService.DeleteShift(id));

        [HttpPut("{id}")]
        public ActionResult<Shift> UpdateShift(Shift shift) =>
            Ok(_shiftService.UpdateShift(shift));
    }
}
