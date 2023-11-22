using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.K_MYR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftsController(UserManager<ApplicationUser> userManager, DataContext dataContext, IShiftsService shiftsService) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;

        private readonly UserManager<ApplicationUser> _userManager = userManager;

        private readonly IShiftsService _shiftsService = shiftsService;

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShiftDTO>> PostShift(ShiftInsertModel shiftDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user is null)
                    return NotFound("User Claim Not Found");

                var shift = await _shiftsService.AddShiftAsync(shiftDTO, user);

                return CreatedAtAction(nameof(GetShift), new { id = shift.Id }, shift);
            }
            else
            {
                return BadRequest();

            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShiftDTO>> GetShift(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound("User Claim Not Found");

            var shift = _userManager.Users
                                .Include(x => x.Shifts)
                                .Single(u => u.Id == user.Id)
                                .Shifts
                                .FirstOrDefault(s => s.Id == id)?.GetDTO();

            return shift is null ? NotFound() : Ok(shift);
        }

        [Authorize]
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ShiftDTO>>> GetShifts()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound("User Claim Not Found");

            var shifts = _userManager.Users
                                .Include(x => x.Shifts)
                                .Single(u => u.Id == user.Id)
                                .Shifts
                                .Select(x => x.GetDTO())
                                .ToList();

            return user.Shifts.Count != 0 ? Ok(shifts) : NotFound();
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutShift(int id, ShiftInsertModel shift)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user is null)
                    return NotFound("User Claim Not Found");

                return await _shiftsService.UpdateShiftAsync(id, shift, user) ? NoContent() : NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteShift(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound("User Claim Not Found");

            return await _shiftsService.DeleteShiftAsync(id, user) ? NoContent() : NotFound();
        }
    }
}
