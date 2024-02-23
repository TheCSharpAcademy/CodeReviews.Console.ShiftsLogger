namespace ShiftsLoggerConsoleUI;

using Spectre.Console;
using static ShiftsLoggerConsoleUI.Enums;

internal static class UserInterface
{
	internal static void ShowMainMenu()
	{
		var isAppRunning = true;
		while (isAppRunning)
		{
			Console.Clear();
			var option = AnsiConsole.Prompt(
				new SelectionPrompt<MenuOptions>()
				.Title("What would you like to do?")
				.AddChoices(
					MenuOptions.AddShift,
					MenuOptions.DeleteShift,
					MenuOptions.UpdateShift,
					MenuOptions.ViewAllShifts,
					MenuOptions.ViewShift,
					MenuOptions.Quit));

			switch (option)
			{
				case MenuOptions.ViewAllShifts:
					Task task = DataAccess.GetAllShiftsAsync();
					task.Wait();
					break;
				case MenuOptions.ViewShift:
					// prompt user for id; if that is not acceptable, will make api request to fetch all valid ids
					Console.WriteLine("Enter an id of the shift you want to fetch info of");
					int id = Int32.Parse(Console.ReadLine());
					task = DataAccess.GetShiftByIdAsync(id);
					task.Wait();
					break;
				case MenuOptions.Quit:
					Console.WriteLine("Goodbye");
					isAppRunning = false;
					break;
			}
		}
	}
}
