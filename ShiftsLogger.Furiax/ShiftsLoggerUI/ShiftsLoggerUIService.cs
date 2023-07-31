namespace ShiftsLoggerUI
{
	internal class ShiftsLoggerUIService
	{
		internal static async void GetShift()
		{
			var shifts = await ShiftLoggersUIController.GetShifts();
			if (shifts.Count == 0)
			{
				Console.WriteLine("No shiftslogs found");
			}
			else
			{

			}
		}

		internal static async Task GetShifts()
		{
			var shifts = await ShiftLoggersUIController.GetShifts();
			UserInterface.DisplayShifts(shifts);
		}
	}

}
