using ShiftsLogger.ConsoleUI.Models;
using ShiftsLogger.ConsoleUI.Services;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Controllers;
public class ShiftsController
{
	private readonly ShiftsService _shiftsService;
	private readonly UsersService _usersService;

	public ShiftsController(ShiftsService shiftsService, UsersService usersService)
	{
		_shiftsService = shiftsService;
		_usersService = usersService;
	}
	public async Task GetAllShifts()
	{
		var shifts = await _shiftsService.GetAll();
		if (shifts.Count == 0)
		{
			return;
		}

		TableVisualization.DisplayShiftsTable(shifts);
		var shift = Helpers.GetShiftFromList(shifts);
		if (shift is null)
		{
			return;
		}

		var users = await _usersService.GetAll();
		if (users.Count == 0)
		{
			return;
		}

		var shiftUsers = await _shiftsService.GetUsersByShiftId(shift.Id);
		if (shiftUsers.Count > 0)
		{
			TableVisualization.DisplayShiftDetailsTable(shift, shiftUsers);
		}
		if (!AnsiConsole.Confirm($"Update users for [blue]{shift.ToString()}[/]?"))
		{
			return;
		}

		var usersToUpdate = UserInput.GetUsersToUpdate(users, shiftUsers);
		await _shiftsService.UpdateShiftUsers(shift.Id, usersToUpdate);
	}

	public async Task AddShift()
	{
		var shift = Helpers.CreateNewShift();
		TableVisualization.DisplayShiftTable(shift);
		if (!AnsiConsole.Confirm("Are you sure you want to create a new shift?"))
		{
			return;
		}

		await _shiftsService.CreateShift(shift);
	}

	public async Task DeleteShift()
	{
		var shifts = await _shiftsService.GetAll();
		if (shifts.Count == 0)
		{
			return;
		}

		TableVisualization.DisplayShiftsTable(shifts);
		var shift = Helpers.GetShiftFromList(shifts);
		if (shift is null)
		{
			return;
		}

		TableVisualization.DisplayShiftTable(shift);
		if (!AnsiConsole.Confirm("Are you sure you want delete this shift?"))
		{
			return;
		}

		await _shiftsService.DeleteShift(shift.Id);
	}

	public async Task UpdateShift()
	{
		var shifts = await _shiftsService.GetAll();
		if (shifts.Count == 0)
		{
			return;
		}

		TableVisualization.DisplayShiftsTable(shifts);
		var shift = Helpers.GetShiftFromList(shifts);
		if (shift is null)
		{
			return;
		}

		TableVisualization.DisplayShiftTable(shift);
		var shiftToUpdate = Helpers.CreateShiftToUpdate(shift);
		if (!HasChanges(shiftToUpdate, shift))
		{
			AnsiConsole.MarkupLine("[red]No changes to update![/]");
			UserInput.PromptAnyKeyToContinue();
			return;
		}

		if (!AnsiConsole.Confirm("Are you sure you want update this shift?"))
		{
			return;
		}

		await _shiftsService.UpdateShift(shift.Id, shiftToUpdate);
	}

	private static bool HasChanges(ShiftUpdate shiftToUpdate, Shift shift)
	{
		if (shift.StartTime.Equals(shift.StartTime) && shift.EndTime.Equals(shift.EndTime))
		{
			return false;
		}
		return true;
	}
}
