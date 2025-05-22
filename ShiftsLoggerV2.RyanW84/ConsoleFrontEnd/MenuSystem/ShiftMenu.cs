using ShiftsLoggerV2.RyanW84.Services;
using ConsoleFrontEnd.Services;

using Spectre.Console;
using AutoMapper;
using ShiftsLoggerV2.RyanW84.Mappings;

namespace ConsoleFrontEnd.MenuSystem;

public class ShiftMenu
{
	private static HttpClient _httpClient = new HttpClient();
	private static readonly Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
	private static IShiftService _shiftService = new ConsoleFrontEnd.Services.ShiftService(_httpClient,_mapper);

	public static async Task DisplayShiftMenu( )
	{
		// Initialize the ShiftController with the ShiftService
		var shiftController = new ShiftController(_shiftService);

		AnsiConsole.Clear();
		while (true)
		{
			AnsiConsole.Write(
				new Rule("[bold yellow]Shift Menu[/]").RuleStyle("yellow").Centered()
			);
			AnsiConsole.WriteLine("Please select an option from the menu below:");
			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]Select an option:[/]")
					.AddChoices(
						"Create Shift" ,
						"View Shifts" ,
						"Edit Shift" ,
						"Delete Shift" ,
						"Back to Main Menu"
					)
			);
			switch (choice)
			{
				case "Create Shift":
					await shiftController.CreateShift();
					break;
			}
		}
	}
}
