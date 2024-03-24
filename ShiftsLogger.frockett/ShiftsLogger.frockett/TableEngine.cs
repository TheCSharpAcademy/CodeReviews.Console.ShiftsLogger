using ShiftsLogger.frockett.UI.Dtos;
using Spectre.Console;

namespace ShiftsLogger.frockett.UI;

public class TableEngine
{
    public void PrintShifts(List<ShiftDto> shifts)
    {

        if(shifts?.Any() != true)
        {
            AnsiConsole.MarkupLine("[red]No shifts found[/]");
            return;
        }

        Table table = new Table();
        table.Alignment(Justify.Center);
        table.Title("Shifts");
        table.AddColumns(new[] { "System ID", "Employee", "Start", "End", "Duration" });

        foreach(ShiftDto shift in shifts)
        {
            table.AddRow(shift.Id.ToString(), shift.EmployeeName, shift.StartTime.ToString(), shift.EndTime.ToString(), shift.Duration.ToString());
        }

        AnsiConsole.Write(table);
    }

    public EmployeeDto SelectEmployeeFromList(List<EmployeeDto> employees)
    {
        AnsiConsole.Clear();

        if(!employees.Any())
        {
            AnsiConsole.MarkupLine("[red]No Employees Found.[/]");
            return null;
        }

        List<string> employeeNames = new();
        foreach(EmployeeDto employee in employees)
        {
            employeeNames.Add(employee.Name);
        }

        var employeeSelection = new SelectionPrompt<string>();
        employeeSelection.AddChoices(employeeNames);
        employeeSelection.Title("Select Employee");

        string selectedEmployeeName = AnsiConsole.Prompt(employeeSelection);

        EmployeeDto selectedEmployee = employees.FirstOrDefault(e => e.Name == selectedEmployeeName);

        return selectedEmployee;
    }
}
