using Microsoft.AspNetCore.Mvc;
using ShiftLoggerApi.Data;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;

        public ShiftController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // GET: api/Shift
        [HttpGet]
        public async Task<IEnumerable<Shift>> GetShiftsAsync()
        {
            return await _dataAccess.GetShiftsAsync();
        }

        // GET: api/Shift/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Shift> GetShiftByIdAsync(int id)
        {
            return await _dataAccess.GetShiftByIdAsync(id);
        }

        // POST: api/Shift
        [HttpPost]
        public async Task PostAsync([FromBody] Shift shift)
        {
            await _dataAccess.AddShiftAsync(shift);
        }

        // PUT: api/Shift/5
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] Shift shift)
        {
            await _dataAccess.UpdateShiftAsync(id, shift);
        }

        // DELETE: api/Shift/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _dataAccess.DeleteShiftAsync(id);
        }
    }
}