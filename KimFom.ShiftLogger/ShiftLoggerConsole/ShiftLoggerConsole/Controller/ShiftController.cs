using ShiftLoggerConsole.Dtos;
using ShiftLoggerConsole.Models;
using ShiftLoggerConsole.Services;
using ShiftLoggerConsole.TableVisualization;
using ShiftLoggerConsole.UserInput;

namespace ShiftLoggerConsole.Controller;

public class ShiftController : IShiftController
{
    private readonly IApiConnectionService _apiConnectionService;
    private readonly Menus _menus;
    private readonly IInput _input;
    private readonly ITableBuilder _builder;

    public ShiftController(
        IApiConnectionService apiConnectionService,
        Menus menus,
        IInput input,
        ITableBuilder builder)
    {
        _apiConnectionService = apiConnectionService;
        _menus = menus;
        _input = input;
        _builder = builder;
    }

    public async Task Start()
    {
        _menus.DisplayMainMenu();
        var choice = _input.GetInput();
        while (choice != "c")
        {
            switch (choice)
            {
                case "a":
                    await DisplayAllShifts();
                    break;
                case "v":
                    await DisplaySingleShift();
                    break;
                case "s":
                    await StartShift();
                    break;
                case "e":
                    await EndShift();
                    break;
                case "d":
                    await DeleteShift();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice!");
                    Continue();
                    break;
            }

            _menus.DisplayMainMenu();
            choice = _input.GetInput();
        }
    }

    private async Task DisplayAllShifts()
    {
        var shifts = await _apiConnectionService.GetAllShifts();

        _builder.DisplayTable(shifts);

        Continue();
    }

    private async Task DisplaySingleShift()
    {
        Console.WriteLine("You need to enter an id to get the shift");
        var id = _input.GetId();
        Shift? shift = await _apiConnectionService.GetShiftById(id);

        if (shift.Name == null)
        {
            Continue();
            return;
        }

        var shifts = new List<Shift> { shift };
        _builder.DisplayTable(shifts);
        Continue();
    }

    private async Task StartShift()
    {
        Console.WriteLine("To record a shift we need a name of the staff: ");
        var name = _input.GetName();
        var shift = new ShiftAddDto { Name = name, StartTime = DateTime.Now };
        await _apiConnectionService.AddShift(shift);

        Continue();
    }

    private async Task EndShift()
    {
        var shifts = await _apiConnectionService.GetAllShifts();
        _builder.DisplayTable(shifts);

        Console.WriteLine("To end a shift, we need an id");
        var id = _input.GetId();

        if (shifts != null)
        {
            if (!shifts.Any(x => x.Id == id))
            {
                Console.WriteLine("Selected id does not exist!");
                Continue();
                return;
            }

            var startTime = shifts.FirstOrDefault(x => x.Id == id)!.StartTime;
            var shift = new ShiftUpdateDto
            {
                EndTime = DateTime.Now,
                Duration = GetDuration(startTime, DateTime.Now)
            };
            await _apiConnectionService.UpdateShift(id, shift);
        }

        Continue();
    }

    private async Task DeleteShift()
    {
        var shifts = await _apiConnectionService.GetAllShifts();
        _builder.DisplayTable(shifts);

        Console.WriteLine("To delete a shift, we need an id");
        var id = _input.GetId();
        await _apiConnectionService.DeleteShift(id);

        Continue();
    }

    private string GetDuration(DateTime startTime, DateTime endTime)
    {
        var timeSpan = endTime - startTime;
        var hours = timeSpan.Hours + (timeSpan.Days * 24);
        var minutes = timeSpan.Minutes;
        return $"{hours} hrs, {minutes} mins";
    }

    private void Continue()
    {
        Console.WriteLine("Press any Enter to continue");
        Console.ReadLine();
    }
}