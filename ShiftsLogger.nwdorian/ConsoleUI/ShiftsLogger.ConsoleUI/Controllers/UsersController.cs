using ShiftsLogger.ConsoleUI.Models;
using ShiftsLogger.ConsoleUI.Services;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Controllers;
public class UsersController
{
	private readonly UsersService _usersService;
	private readonly ShiftsService _shiftsService;

	public UsersController(UsersService usersService, ShiftsService shiftsService)
	{
		_usersService = usersService;
		_shiftsService = shiftsService;
	}

	public async Task GetAllUsers()
	{
		var users = await _usersService.GetAll();
		if (users.Count == 0)
		{
			return;
		}

		TableVisualization.DisplayUsersTable(users);
		var user = Helpers.GetUserFromList(users);
		if (user is null)
		{
			return;
		}

		var shifts = await _shiftsService.GetAll();
		if (shifts.Count == 0)
		{
			return;
		}

		var userShifts = await _usersService.GetShiftsByUserId(user.Id);
		if (userShifts.Count > 0)
		{
			TableVisualization.DisplayUserDetailsTable(user, userShifts);
		}
		if (!AnsiConsole.Confirm($"Update shifts for [blue]{user.ToString()}[/]?"))
		{
			return;
		}

		var shiftsToUpdate = UserInput.GetShiftsToUpdate(shifts, userShifts);
		await _usersService.UpdateUserShifts(user.Id, shiftsToUpdate);
	}

	public async Task AddUser()
	{
		var user = Helpers.CreateNewUser();
		TableVisualization.DisplayUserTable(user);
		if (!AnsiConsole.Confirm("Are you sure you want to create a new user?"))
		{
			return;
		}

		await _usersService.CreateUser(user);
	}

	public async Task DeleteUser()
	{
		var users = await _usersService.GetAll();
		if (users.Count == 0)
		{
			return;
		}

		TableVisualization.DisplayUsersTable(users);
		var user = Helpers.GetUserFromList(users);
		if (user is null)
		{
			return;
		}

		TableVisualization.DisplayUserTable(user);
		if (!AnsiConsole.Confirm("Are you sure you want delete this user?"))
		{
			return;
		}

		await _usersService.DeleteUser(user.Id);

	}

	public async Task UpdateUser()
	{
		var users = await _usersService.GetAll();
		if (users.Count == 0)
		{
			return;
		}

		TableVisualization.DisplayUsersTable(users);
		var user = Helpers.GetUserFromList(users);
		if (user is null)
		{
			return;
		}

		TableVisualization.DisplayUserTable(user);
		var userToUpdate = Helpers.CreateUserToUpdate();
		if (!HasChanges(userToUpdate))
		{
			AnsiConsole.MarkupLine("[red]No changes to update![/]");
			UserInput.PromptAnyKeyToContinue();
			return;
		}

		if (!AnsiConsole.Confirm("Are you sure you want update this user?"))
		{
			return;
		}

		await _usersService.UpdateUser(user.Id, userToUpdate);
	}

	private static bool HasChanges(UserUpdate user)
	{
		if (string.IsNullOrWhiteSpace(user.FirstName) &&
				string.IsNullOrWhiteSpace(user.LastName) &&
				string.IsNullOrWhiteSpace(user.Email))
		{
			return false;
		}
		return true;
	}
}
