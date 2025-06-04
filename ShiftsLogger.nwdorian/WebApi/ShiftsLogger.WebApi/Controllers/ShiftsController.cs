using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Model;
using ShiftsLogger.Service.Common;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
	private readonly IShiftService _shiftService;
	private readonly IMapper _mapper;

	public ShiftsController(IShiftService shiftService, IMapper mapper)
	{
		_shiftService = shiftService;
		_mapper = mapper;
	}

	[EndpointDescription("Retrieves all shifts from the database.")]
	[ProducesResponseType<List<ShiftRead>>(StatusCodes.Status200OK, "application/json")]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var response = await _shiftService.GetAllAsync();

		if (response.Success)
		{
			var shifts = _mapper.Map<List<ShiftRead>>(response.Data);
			return Ok(shifts);
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Retrieves a single shift from the database by Id.")]
	[ProducesResponseType<ShiftRead>(StatusCodes.Status200OK, "application/json")]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([Description("Id of the shift.")] Guid id)
	{
		var response = await _shiftService.GetByIdAsync(id);

		if (response.Success)
		{
			var shift = _mapper.Map<ShiftRead>(response.Data);
			return Ok(shift);
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Creates a new shift.")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpPost]
	public async Task<IActionResult> Create([Description("Shift to create.")] ShiftCreate shiftCreate)
	{
		var shift = _mapper.Map<Shift>(shiftCreate);

		var response = await _shiftService.CreateAsync(shift);

		if (response.Success)
		{
			var shiftRead = _mapper.Map<ShiftRead>(response.Data);
			return CreatedAtAction(nameof(GetById), new { Id = shift.Id }, shiftRead);
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Deletes an existing shift.")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete([Description("Id of the shift.")] Guid id)
	{
		var response = await _shiftService.DeleteAsync(id);

		if (response.Success)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Updates an existing shift.")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpPut("{id}")]
	public async Task<IActionResult> Update([Description("Id of the shift.")] Guid id, [Description("Shift to update.")] ShiftUpdate shiftUpdate)
	{
		var shift = _mapper.Map<Shift>(shiftUpdate);

		var response = await _shiftService.UpdateAsync(id, shift);

		if (response.Success)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Updates users assigned to a shift.")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpPut("{id}/users")]
	public async Task<IActionResult> UpdateUsers([Description("Id of the shift.")] Guid id, [Description("A list of users that will be assigned to the shift after update.")] List<UserRead> usersRead)
	{
		var users = _mapper.Map<List<User>>(usersRead);

		var response = await _shiftService.UpdateUsersAsync(id, users);

		if (response.Success)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Retrieves users assigned to a shift.")]
	[ProducesResponseType<List<UserRead>>(StatusCodes.Status200OK, "application/json")]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpGet("{id}/users")]
	public async Task<IActionResult> GetUsers([Description("Id of the shift.")] Guid id)
	{
		var response = await _shiftService.GetUsersByShiftIdAsync(id);

		if (response.Success)
		{
			var shifts = _mapper.Map<List<UserRead>>(response.Data);
			return Ok(shifts);
		}

		return BadRequest(response.Message);
	}
}
