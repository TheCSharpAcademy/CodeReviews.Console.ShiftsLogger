using ShiftsLoggerUI;
using Spectre.Console;

namespace ShiftLoggerUI;

public class EmployeeMenuHandler
{
    public void Display()
    {
        MenuPresentation.MenuDisplayer<EmployeeMenuOptions>(() => "[blue]Employee Menu[/]", HandleMenuOptions);
    }

    private bool HandleMenuOptions(EmployeeMenuOptions option)
    {
        switch (option)
        {
            case EmployeeMenuOptions.Back:
                return false;
            case EmployeeMenuOptions.CreateEmployee:
                EmployeeService.CreateEmployee();
                break;
            case EmployeeMenuOptions.UpdateEmployee:
                EmployeeService.UpdateEmployee();
                break;
            case EmployeeMenuOptions.DeleteEmployee:
                EmployeeService.DeleteEmployee();
                break;
            case EmployeeMenuOptions.ShowEmployees:
                EmployeeService.ShowEmployees();
                break;
            default:
                AnsiConsole.WriteLine($"Unknow option: {option}");
                break;
        }

        return true;
    }
}