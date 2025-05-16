using ShiftsLogger.KamilKolanowski.Enums;
using ShiftsLoggerUI.Services;
using Spectre.Console;

namespace ShiftsLoggerUI.Controllers;

internal class ShiftController
{
    private readonly ShiftService _shiftService = new();

    internal async Task Operate()
    {
        var selectOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose an option:")
                .AddChoices(ShiftsLoggerMenu.ShiftMenuType.Values)
        );

        var selectedOption = ShiftsLoggerMenu
            .ShiftMenuType.FirstOrDefault(o => o.Value == selectOption)
            .Key;

        switch (selectedOption)
        {
            case ShiftsLoggerMenu.ShiftMenu.AddShift:
                await AddShift();
                break;
            case ShiftsLoggerMenu.ShiftMenu.EditShift:
                await UpdateShift();
                break;
            case ShiftsLoggerMenu.ShiftMenu.DeleteShift:
                await DeleteShift();
                break;
            case ShiftsLoggerMenu.ShiftMenu.ViewShifts:
                await ViewShifts();
                break;
            case ShiftsLoggerMenu.ShiftMenu.GoBack:
                return;
        }
    }

    private async Task AddShift()
    {
        await _shiftService.CreateShift();
        
        Console.ReadKey();
    }

    private async Task UpdateShift()
    {
        await _shiftService.EditShift();
        
        Console.ReadKey();
    }

    private async Task DeleteShift()
    {
        await _shiftService.DeleteShift();
        
        Console.ReadKey();
    }

    private async Task ViewShifts()
    {
        await _shiftService.CreateShiftsTable();
        
        AnsiConsole.MarkupLine("Press any key to continue...");
        Console.ReadKey();
    }
}
