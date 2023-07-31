using ShiftsLoggerUI.Models;
using Spectre.Console;

namespace ShiftsLoggerUI
{
	internal class UserInterface
	{
		internal static void DisplayShifts(List<Shift> shifts)
		{
			var table = new Table();
			table.AddColumn("Id");
			table.AddColumn("Name");
			table.AddColumn("StartOfShift");
			table.AddColumn("EndOfShift");
			table.AddColumn("Duration");

			foreach (var shift in shifts)
			{
				table.AddRow(shift.Id.ToString(),
					shift.EmployeeName,
					shift.StartOfShift.ToString(),
					shift.EndOfShift.ToString(),
					shift.Duration.ToString());
			}
			AnsiConsole.Write(table);
			Console.WriteLine("Press any key to continue");
			Console.ReadKey();
			Console.Clear();
		}

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
