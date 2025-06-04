using ShiftsLogger.ConsoleUI.Controllers;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Menus;
public class ShiftsMenu : BaseMenu
{
	private readonly ShiftsController _shiftsController;

	public ShiftsMenu(ShiftsController shiftsController)
	{
		_shiftsController = shiftsController;
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
				case Options.ViewAllShifts:
					await _shiftsController.GetAllShifts();
					break;
				case Options.AddShift:
					await _shiftsController.AddShift();
					break;
				case Options.DeleteShift:
					await _shiftsController.DeleteShift();
					break;
				case Options.UpdateShift:
					await _shiftsController.UpdateShift();
					break;
				case Options.MainMenu:
					exit = true;
					break;
			}
		}
	}

	private enum Options
	{
		ViewAllShifts,
		AddShift,
		DeleteShift,
		UpdateShift,
		MainMenu
	}
}
