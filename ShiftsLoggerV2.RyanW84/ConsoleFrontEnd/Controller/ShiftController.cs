using ConsoleFrontEnd.ApiShiftService;
using ConsoleFrontEnd.MenuSystem;
using ConsoleFrontEnd.Services;

using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using ShiftsLoggerV2.RyanW84.Services;
using Spectre.Console;

public class ShiftController()
{
    private static ShiftService _shiftService = new ShiftService();

	public async Task CreateShift()
    {
        ShiftsDto shift = UserInterface.CreateShiftUi();
        UserInterface.DisplayAllShiftsTable(shift);

        // Map ShiftsDto to ShiftApiRequestDto
        var shiftApiRequest = new ShiftApiRequestDto
        {
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            WorkerId = shift.WorkerId,
            LocationId = shift.LocationId,
        };

        var response = await _frontEndShiftService.CreateShift(shiftApiRequest);
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
                WorkerId = response.Data.WorkerId,
                LocationId = response.Data.LocationId,
                StartTime = response.Data.StartTime,
                EndTime = response.Data.EndTime,
            };

            UserInterface.DisplayShiftTable(shiftDto);
        }
        else
        {
            AnsiConsole.MarkupLine("[green]Shift added successfully![/]");
        }
        Console.ReadKey();
    }

    public async Task GetAllShifts()
    {
        var shiftOptions = new ShiftFilterOptions
        {
            WorkerId = 0,
            LocationId = 0,
            StartTime = DateTimeOffset.MinValue,
            EndTime = DateTimeOffset.MaxValue,
        };
        ApiResponseDto<ShiftsDto>? response = await _frontEndShiftService.GetAllShifts(
            shiftOptions
        );
        if (response.RequestFailed)
        {
            AnsiConsole.MarkupLine("[red]Failed to retrieve shifts.[/]");
            AnsiConsole.MarkupLine($"[red]{response.Message}[/]");
            return;
        }
        if (response.Data is not null)
        {
            // Map ShiftsDto to Shifts before passing to DisplayShiftTable
            ShiftsDto shifts = response.Data;

                UserInterface.DisplayShiftTable(shifts);
           
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]No shifts found.[/]");
        }
    }
}
