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
				.Title("From wich employee would you like to see the shift")
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
	}

}
