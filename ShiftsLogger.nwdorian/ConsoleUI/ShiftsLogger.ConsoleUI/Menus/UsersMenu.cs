using ShiftsLogger.ConsoleUI.Controllers;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Menus;
public class UsersMenu : BaseMenu
{
	private readonly UsersController _usersController;

	public UsersMenu(UsersController usersController)
	{
		_usersController = usersController;
	}
	public override async Task DisplayAsync()
	{
		var exit = false;

		while (!exit)
		{
			AnsiConsole.Clear();

			var selection = AnsiConsole.Prompt(
					new SelectionPrompt<Options>()
					.Title("Select from the menu:")
					.AddChoices(Enum.GetValues<Options>()));

			switch (selection)
			{
				case Options.ViewAllUsers:
					await _usersController.GetAllUsers();
					break;
				case Options.AddUser:
					await _usersController.AddUser();
					break;
				case Options.DeleteUser:
					await _usersController.DeleteUser();
					break;
				case Options.UpdateUser:
					await _usersController.UpdateUser();
					break;
				case Options.MainMenu:
					exit = true;
					break;
			}
		}
	}

	private enum Options
	{
		ViewAllUsers,
		AddUser,
		DeleteUser,
		UpdateUser,
		MainMenu
	}
}
