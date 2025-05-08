using FrontEnd.Menus;

using ShiftsLogger.Ryanw84.Services;


namespace FrontEnd;

public class Program
	{
	public static void Main(IShiftService shiftService)
		{
		UserInterface.MainMenu(shiftService);
		Console.ReadKey();
		}
	}
