using ConsoleFrontEnd.MenuSystem;

using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using ShiftsLoggerV2.RyanW84.Services;

using Spectre.Console;

public class ShiftController
{
	private readonly IShiftService _shiftService;

	public ShiftController(IShiftService shiftService)
	{
		_shiftService = shiftService ?? throw new ArgumentNullException(nameof(shiftService));
	}

	public async Task CreateShift( )
	{
		var shift = UserInterface.CreateShiftUi();
		UserInterface.DisplayShiftTable(shift);

		// Map ShiftsDto to ShiftApiRequestDto
		var shiftApiRequest = new ShiftApiRequestDto
		{
			StartTime = shift.StartTime ,
			EndTime = shift.EndTime ,
			WorkerId = shift.WorkerId ,
			LocationId = shift.LocationId ,
		};

		var response = await _shiftService.CreateShift(shiftApiRequest);
		if (!response.RequestFailed)
		{
			AnsiConsole.MarkupLine("[red]Failed to add shift.[/]");
			AnsiConsole.MarkupLine($"[red]{response.Message}[/]");
		}
		else if (response.Data is not null)
		{
			// Map Shifts to ShiftsDto before passing to DisplayShiftTable
			var shiftDto = new ShiftsDto
			{
			
				WorkerId = response.Data.WorkerId ,
				LocationId = response.Data.LocationId ,
				StartTime = response.Data.StartTime ,
				EndTime = response.Data.EndTime ,
			};

			UserInterface.DisplayShiftTable(shiftDto);
		}
		else
		{
			AnsiConsole.MarkupLine("[green]Shift added successfully![/]");
		}
		Console.ReadKey();
	}

	public async Task GetAllShifts( )
	{
		var shiftOptions = new ShiftFilterOptions
		{
			WorkerId = 0 ,
			LocationId = 0 ,
			StartTime = DateTimeOffset.MinValue ,
			EndTime = DateTimeOffset.MaxValue ,
		};
		var response = await _shiftService.GetAllShifts(shiftOptions);
		if (response.RequestFailed)
		{
			AnsiConsole.MarkupLine("[red]Failed to retrieve shifts.[/]");
			AnsiConsole.MarkupLine($"[red]{response.Message}[/]");
			return;
		}
		if (response.Data != null)
		{
			foreach (var shift in response.Data)
			{
				var shiftDto = new ShiftsDto
				{
					
					WorkerId = shift.WorkerId ,
					LocationId = shift.LocationId ,
					StartTime = shift.StartTime ,
					EndTime = shift.EndTime
				};
				UserInterface.DisplayShiftTable(shiftDto);
			}
		}
	}
}
