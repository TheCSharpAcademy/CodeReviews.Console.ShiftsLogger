using ShiftsLoggerUI.Models;
using Spectre.Console;
using ShiftsLoggerUI.Services;
using Newtonsoft.Json;
using ConsoleTableExt;
using AutoMapper;

namespace ShiftsLoggerUI.Menus;

internal class ShiftsMenu : BaseMenu
{
    private readonly TableVisualisationEngine<Object> _tableVisualisationEngine;
    private readonly ShiftsService _shiftsService;
    private readonly IMapper _mapper;

    public ShiftsMenu(ShiftsService shiftsService, TableVisualisationEngine<Object> tableVisualisationEngine, IMapper mappingProfiles)
    {
        _shiftsService = shiftsService;
        _tableVisualisationEngine = tableVisualisationEngine;
        _mapper = mappingProfiles;
    }

    private List<string> _menuOptions =
        [
            "1.Add A Shift",
            "2.View All Shifts",
            "3.Delete A Shift",
            "4.Update A Shift",
            "5.Main Menu",
        ];
    public override async Task ShowMenuAsync()
    {
        while (true)
        {
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[teal]Shifts Menu[/]")
                .AddChoices(_menuOptions));

            switch (selectedOption)
            {
                case string selection when selection.Contains("1."):
                    await AddShift();
                    break;
                case string selection when selection.Contains("2."):
                    await GetAllShifts();
                    break;
                case string selection when selection.Contains("3."):
                    await DeleteShift();
                    break;
                case string selection when selection.Contains("4."):
                    await UpdateShift();
                    break;
                case string selection when selection.Contains("5."):
                    return;
            }
        }
    }

    private async Task UpdateShift()
    {
        Console.Clear();
        var shiftId = AnsiConsole.Ask<int>("Enter Shift Id: ");

        var updateChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to update?")
                .AddChoices("Start Time Only", "End Time Only", "Both","Cancel")
        );

        if (updateChoice == "Cancel")
        {
            return;
        }

        DateTime? startTime = null, endTime = null;

        if (updateChoice == "Start Time Only" || updateChoice == "Both")
        {
            startTime = AnsiConsole.Ask<DateTime>("Enter Shift Start Time: ");
        }

        if (updateChoice == "End Time Only" || updateChoice == "Both")
        {
            endTime = AnsiConsole.Ask<DateTime>("Enter Shift End Time: ");
        }

        var updatedShift = new Shift()
        {
            ShiftStartTime = startTime ?? default,  
            ShiftEndTime = endTime ?? default       
        };

        await _shiftsService.Update(shiftId, updatedShift);
        PressAnyKeyToContinue();
    }


    private async Task DeleteShift()
    {
        var shiftId = AnsiConsole.Ask<int>("Enter Shift Id: ");
        await _shiftsService.Delete(shiftId);
    }

    private async Task GetAllShifts()
    {
        var shifts = await _shiftsService.GetAll();
        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No Employees Found![/]");
            PressAnyKeyToContinue();
            return;
        }
        TableVisualisationEngine<Shift>.ViewAsTable(shifts, TableAligntment.Center, new List<string> {"ShiftId" ,"EmpId", "Shift start time", "Shift end time","Duration (Hrs)"}, "Shifts");

        PressAnyKeyToContinue();
    }

    private async Task AddShift()
    {
        var newShift = new Shift()
        {
            EmpId = AnsiConsole.Ask<int>("Enter Employee Id: "),
            ShiftStartTime = AnsiConsole.Ask<DateTime>("Enter Shift Start Time: "),
            ShiftEndTime = AnsiConsole.Ask<DateTime>("Enter Shift End Time: ")
        };
        var createdShift = await _shiftsService.Create(newShift);
        Console.WriteLine(JsonConvert.SerializeObject(createdShift));
        PressAnyKeyToContinue();
    }
}