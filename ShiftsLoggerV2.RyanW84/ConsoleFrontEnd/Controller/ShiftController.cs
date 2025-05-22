using System.Net.Http.Json;
using ConsoleFrontEnd.MenuSystem;

using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Services;

using Spectre.Console;

public class ShiftController
{
	public async Task CreateShift( )
	{
		// Fix for CS1526 and CS7036: Provide required arguments to the ShiftService constructor
		var dbContext = ShiftsDbContext; /* Initialize or retrieve your ShiftsDbContext instance */
		var mapper = /* Initialize or retrieve your IMapper instance */
		var shiftService = new ShiftService(dbContext , mapper);

		var shift = UserInterface.CreateShiftUi();
		UserInterface.DisplayShiftTable(shift);

		// Call the service layer to add the shift (Controller Layer)
		try
		{
			var response = await shiftService.CreateShift(shift);
			if (response.IsSuccessStatusCode)
			{
				AnsiConsole.MarkupLine("[green]Shift added successfully![/]");
			}
			else
			{
				AnsiConsole.MarkupLine("[red]Failed to add shift.[/]");
			}
			Console.ReadKey();
		}
		catch (Exception)
		{
			throw;
		}
	}
}
