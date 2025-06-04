using ShiftsLogger.ConsoleUI.Services;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Menus;
public class SeedingMenu : BaseMenu
{
	private readonly SeedingService _seedingService;

	public SeedingMenu(SeedingService seedingService)
	{
		_seedingService = seedingService;
	}
	public override async Task DisplayAsync()
	{
		var selection = AnsiConsole.Prompt(
						new SelectionPrompt<Options>()
						.Title("Would you like to seed the database?")
						.AddChoices(Enum.GetValues<Options>()));

		switch (selection)
		{
			case Options.Yes:
				await _seedingService.SeedDatabase();
				break;
			case Options.No:
				break;
		}
	}

	private enum Options
	{
		Yes,
		No
	}
}
