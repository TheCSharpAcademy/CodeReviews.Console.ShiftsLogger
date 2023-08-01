using ShiftsLoggerUI.Models;
using Spectre.Console;

namespace ShiftsLoggerUI
{
	internal class ShiftsLoggerUIService
	{
		internal static async Task GetShift()
		{
			var shifts = await ShiftLoggersUIController.GetShifts();
			if (shifts.Count == 0)
			{
				Console.WriteLine("No shiftslogs found");
				Console.ReadKey();
			}
			else
			{
				var shift = await GetShiftOption();
				UserInterface.DisplayShift(shift);
			}
		}

		private static async Task<Shift> GetShiftOption()
		{
			var shifts = await ShiftLoggersUIController.GetShifts();
			var shiftsArray = shifts.Select(x => x.EmployeeName).ToArray();
			var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
				.Title("Select the employee")
				.AddChoices(shiftsArray));
			var id = shifts.Single(x => x.EmployeeName == option).Id;
			var shift = await ShiftLoggersUIController.GetEmployeeShiftById(id);
			return shift;
		}

		internal static async Task GetShifts()
		{
			var shifts = await ShiftLoggersUIController.GetShifts();
			UserInterface.DisplayShifts(shifts);
		}

		internal static async Task InsertShift()
		{
			var shift = new Shift();

			string name = AnsiConsole.Ask<string>("Enter the name of the employee:");
			shift.EmployeeName = name;

			DateTime start = AnsiConsole.Ask<DateTime>("Enter the start of the shift (format yyyy-mm-dd hh:mm): ");
			shift.StartOfShift = start;

			DateTime end = AnsiConsole.Ask<DateTime>("Enter the end of the shift: (format yyyy-mm-dd hh:mm) ");
			shift.EndOfShift = end;

			ShiftLoggersUIController.AddShift(shift);
			Console.Clear();
			
		}

		internal static async Task UpdateShift()
		{
			var shifts = await ShiftLoggersUIController.GetShifts();
			if (shifts.Count == 0)
			{
				Console.WriteLine("No shiftlogs found");
				Console.ReadKey();
			}
			else
			{
				var shift = await GetShiftOption();

				shift.EmployeeName = AnsiConsole.Confirm("Update name?") ?
					AnsiConsole.Ask<string>("Enter the new employee name:")
					: shift.EmployeeName;

				shift.StartOfShift = AnsiConsole.Confirm("Update start of shift ?") ?
					AnsiConsole.Ask<DateTime>("Enter a new start time (format yyyy-mm-dd hh:mm)")
					: shift.StartOfShift;

				shift.EndOfShift = AnsiConsole.Confirm("Update end of shift ?") ?
					AnsiConsole.Ask<DateTime>("Enter a new end time (format yyyy-mm-dd hh:mm)")
					: shift.EndOfShift;

				await ShiftLoggersUIController.UpdateShift(shift);
				Console.Clear();
			}
		}

		internal static async Task DeleteShift()
		{
			var shifts = await ShiftLoggersUIController.GetShifts();
			if (shifts.Count == 0) 
			{
				Console.WriteLine("No shiftlogs found");
				Console.ReadKey();
			}
			else
			{
				var shift = await GetShiftOption();
				await ShiftLoggersUIController.DeleteShift(shift.Id);
			}
		}
	}

}
