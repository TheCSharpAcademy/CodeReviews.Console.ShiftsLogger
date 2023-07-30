using Spectre.Console;

namespace ShiftsLoggerUI
{
	internal class UserInterface
	{
		internal static void MainMenu()
		{
			bool isAppAlive = true;
			while (isAppAlive)
			{
				Console.Clear();
				var option = AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
					.Title("What would you like to do ?")
					.AddChoices(
						MenuOptions.AddShift,
						MenuOptions.ShowAllShifts,
						MenuOptions.ShowShift,
						MenuOptions.UpdateShift,
						MenuOptions.DeleteShift,
						MenuOptions.Quit));
				switch (option)
				{
					case MenuOptions.AddShift:
						break;
					case MenuOptions.ShowAllShifts:
						ShiftsLoggerUIService.GetShifts();
						break;
					case MenuOptions.ShowShift:
						break;
					case MenuOptions.UpdateShift:
						break;
					case MenuOptions.DeleteShift:
						break;
					case MenuOptions.Quit:
						isAppAlive = false;
						break;
				}
			}
		}
		enum MenuOptions
		{
			AddShift,
			ShowAllShifts,
			ShowShift,
			UpdateShift,
			DeleteShift,
			Quit
		}
	}
}
