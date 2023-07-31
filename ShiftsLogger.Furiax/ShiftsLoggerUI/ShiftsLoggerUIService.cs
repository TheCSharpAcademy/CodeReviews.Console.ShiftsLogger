namespace ShiftsLoggerUI
{
	internal class ShiftsLoggerUIService
	{
		internal static async Task GetShifts()
		{
			var shifts = await ShiftLoggersUIController.GetShifts();
			UserInterface.DisplayShifts(shifts);
			Console.ReadKey();
		}
	}
}
