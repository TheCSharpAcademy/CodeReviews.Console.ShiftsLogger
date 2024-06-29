namespace ShiftLoggerUI.UI;

using ShiftLoggerUI.Enums;
using Spectre.Console;

internal class UserInputManager
{
    public void Header()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("Shift Tracker")
                .Centered()
                .Color(Color.Aqua));
    }

    public MenuOptions GetMenuOption()
    {
        Header();
        return AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
        .Title("Please choose a [green]API call[/]?")
        .AddChoices(Enum.GetValues<MenuOptions>())
        .PageSize(15));
    }

    public void DisplayAllEmployees(ICollection<EmployeeDto> employees)
    {
        Header();

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Expand();
        table.BorderColor(Color.DarkTurquoise);

        table.AddColumn(new TableColumn("[bold]Id[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Name[/]"));
        table.AddColumn(new TableColumn("[bold]Age[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Phone Number[/]"));
        table.AddColumn(new TableColumn("[bold]Email Address[/]"));

        bool isAlternate = false;
        foreach (var item in employees)
        {
            var rowColor = isAlternate ? "grey" : "white";
            table.AddRow(
                $"[{rowColor}]{item.Id}[/]",
                $"[{rowColor}]{item.Name}[/]",
                $"[{rowColor}]{item.Age}[/]",
                $"[{rowColor}]{item.PhoneNumber}[/]",
                $"[{rowColor}]{item.EmailAddress}[/]"
            );
            isAlternate = !isAlternate;
        }

        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("\n[italic]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    public void DisplayEmployee(EmployeeDto employee)
    {
        Header();

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Expand();
        table.BorderColor(Color.DarkTurquoise);

        table.AddColumn(new TableColumn("[bold]Id[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Name[/]"));
        table.AddColumn(new TableColumn("[bold]Age[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Phone Number[/]"));
        table.AddColumn(new TableColumn("[bold]Email Address[/]"));

        table.AddRow(employee.Id.ToString(), employee.Name, employee.Age.ToString(), employee.PhoneNumber, employee.EmailAddress);
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("\n[italic]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    public int GetId() => AnsiConsole.Ask<int>("Type an ID:");

    public void Error(string error)
    {
        Header();
        Console.Beep();
        AnsiConsole.Markup("[red]Error occured: [/]");
        AnsiConsole.WriteLine(error);
        Console.ReadKey(true);
    }

    public bool Retry()
    {
        return AnsiConsole.Ask<bool>("Would you like to try again <True/False?");
    }
}

