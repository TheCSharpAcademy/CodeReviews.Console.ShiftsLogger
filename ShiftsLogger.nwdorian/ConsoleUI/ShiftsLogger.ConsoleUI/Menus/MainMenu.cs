using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Menus;
public class MainMenu : BaseMenu
{
	private readonly UsersMenu _usersMenu;
	private readonly ShiftsMenu _shiftsMenu;
	private readonly SeedingMenu _seedingMenu;

	public MainMenu(UsersMenu usersMenu, ShiftsMenu shiftsMenu, SeedingMenu seedingMenu)
	{
		_usersMenu = usersMenu;
		_shiftsMenu = shiftsMenu;
		_seedingMenu = seedingMenu;
	}
	public override async Task DisplayAsync()
	{
		var exit = false;

		while (!exit)
		{
			AnsiConsole.Clear();

			AnsiConsole.Write(
					new FigletText("Shifts Logger")
							.LeftJustified()
							.Color(Color.Yellow));

			var selection = AnsiConsole.Prompt(
					new SelectionPrompt<Options>()
					.Title("Select from the menu:")
					.AddChoices(Enum.GetValues<Options>()));

			switch (selection)
			{
				case Options.ManageUsers:
					await _usersMenu.DisplayAsync();
					break;
				case Options.ManageShifts:
					await _shiftsMenu.DisplayAsync();
					break;
				case Options.SeedData:
					await _seedingMenu.DisplayAsync();
					break;
				case Options.Exit:
					if (AnsiConsole.Confirm("Are you sure you want to exit?"))
					{
						Console.WriteLine("Goodbye!");
						exit = true;
					}
					else
					{
						exit = false;
					}
					break;
			}
		}
	}
	private enum Options
	{
		ManageUsers,
		ManageShifts,
		SeedData,
		Exit
	}
}
