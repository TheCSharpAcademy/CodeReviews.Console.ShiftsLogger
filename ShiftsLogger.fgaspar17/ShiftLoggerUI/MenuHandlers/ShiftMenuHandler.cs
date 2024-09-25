using ShiftsLoggerUI;
using Spectre.Console;

namespace ShiftLoggerUI;

public class ShiftMenuHandler
{
    public void Display()
    {
        MenuPresentation.MenuDisplayer<ShiftMenuOptions>(() => "[blue]Shift Menu[/]", HandleMenuOptions);
    }

    private bool HandleMenuOptions(ShiftMenuOptions option)
    {
        switch (option)
        {
            case ShiftMenuOptions.Quit:
                return false;
            case ShiftMenuOptions.CreateShift:
                ShiftService.CreateShift();
                break;
            case ShiftMenuOptions.UpdateShift:
                ShiftService.UpdateShift();
                break;
            case ShiftMenuOptions.DeleteShift:
                ShiftService.DeleteShift();
                break;
            case ShiftMenuOptions.ShowShifts:
                ShiftService.ShowShifts();
                break;
            case ShiftMenuOptions.ShowShiftsByEmployee:
                ShiftService.ShowShiftsByEmployee();
                break;
            case ShiftMenuOptions.ManageEmployees:
                EmployeeMenuHandler employeeMenuHandler = new();
                employeeMenuHandler.Display();
                break;
            default:
                AnsiConsole.WriteLine($"Unknow option: {option}");
                break;
        }

        return true;
    }
}