using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Model;
using ShiftsLogger.Service.Common;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly IUserService _userService;
	private readonly IMapper _mapper;

	public UsersController(IUserService userService, IMapper mapper)
	{
		_userService = userService;
		_mapper = mapper;
	}

	[EndpointDescription("Retrieves all users from the database.")]
	[ProducesResponseType<List<UserRead>>(StatusCodes.Status200OK, "application/json")]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var response = await _userService.GetAllAsync();

		if (response.Success)
		{
			var users = _mapper.Map<List<UserRead>>(response.Data);
			return Ok(users);
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Retrieves a single user from the database by Id.")]
	[ProducesResponseType<UserRead>(StatusCodes.Status200OK, "application/json")]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([Description("Id of the user.")] Guid id)
	{
		var response = await _userService.GetByIdAsync(id);

		if (response.Success)
		{
			var user = _mapper.Map<UserRead>(response.Data);
			return Ok(user);
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Creates a new user.")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpPost]
	public async Task<IActionResult> Create([Description("User to create.")] UserCreate userCreate)
	{
		var user = _mapper.Map<User>(userCreate);

		var response = await _userService.CreateAsync(user);

		if (response.Success)
		{
			var userRead = _mapper.Map<UserRead>(response.Data);
			return CreatedAtAction(nameof(GetById), new { Id = user.Id }, userRead);
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Deletes an existing user.")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete([Description("Id of the user.")] Guid id)
	{
		var response = await _userService.DeleteAsync(id);

		if (response.Success)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Updates an existing user.")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpPut("{id}")]
	public async Task<IActionResult> Update([Description("Id of the user.")] Guid id, [Description("User to update.")] UserUpdate userUpdate)
	{
		var user = _mapper.Map<User>(userUpdate);

		var response = await _userService.UpdateAsync(id, user);

		if (response.Success)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Updates shifts assigned to a user.")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpPut("{id}/shifts")]
	public async Task<IActionResult> UpdateShifts([Description("Id of the user.")] Guid id, [Description("A list of shifts that will be assigned to the user after update.")] List<ShiftRead> shiftsRead)
	{
		var shifts = _mapper.Map<List<Shift>>(shiftsRead);

		var response = await _userService.UpdateShiftsAsync(id, shifts);

		if (response.Success)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[EndpointDescription("Retrieves shifts assigned to a user.")]
	[ProducesResponseType<List<ShiftRead>>(StatusCodes.Status200OK, "application/json")]
	[ProducesResponseType<string>(StatusCodes.Status400BadRequest, "text/plain")]
	[HttpGet("{id}/shifts")]
	public async Task<IActionResult> GetShifts([Description("Id of the user.")] Guid id)
	{
		var response = await _userService.GetShiftsByUserIdAsync(id);

		if (response.Success)
		{
			var shifts = _mapper.Map<List<ShiftRead>>(response.Data);
			return Ok(shifts);
		}

		return BadRequest(response.Message);
	}
}
