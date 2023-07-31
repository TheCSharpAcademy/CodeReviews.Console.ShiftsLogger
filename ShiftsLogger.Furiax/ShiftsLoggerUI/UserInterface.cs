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
		internal static void DisplayShift(Shift shift)
		{
			var panel = new Panel($@"Employee: {shift.EmployeeName}
Start Time: {shift.StartOfShift}
End Time: {shift.EndOfShift}
Time worked: {shift.Duration}");
			panel.Header = new PanelHeader("Employee shift info");
			panel.Padding = new Padding(2, 2, 2, 2);

			AnsiConsole.Write(panel);
            Console.WriteLine("Press any key to continue");
			Console.ReadKey();
			Console.Clear();
        }

		internal static async Task MainMenu()
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
						await ShiftsLoggerUIService.GetShifts();
						break;
					case MenuOptions.ShowShift:
						await ShiftsLoggerUIService.GetShift();
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
