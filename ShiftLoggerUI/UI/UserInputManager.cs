namespace ShiftLoggerUI.UI;

using ShiftLoggerUI.Enums;
using Spectre.Console;
using System.Net.Mail;

internal static class UserInputManager
{
    private static void Header()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("Shift Tracker")
                .Centered()
                .Color(Color.Aqua));
    }

    public static MenuOptions GetMenuOption()
    {
        Header();
        return AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
        .Title("Please choose a [green]API call[/]?")
        .AddChoices(Enum.GetValues<MenuOptions>())
        .PageSize(15));
    }

    public static void DisplayAllEmployees(ICollection<EmployeeDto> employees)
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

    public static void DisplayEmployee(EmployeeDto employee)
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

    public static int GetId() => AnsiConsole.Ask<int>("Type an ID:");

    public static void Error(string error)
    {
        Header();
        Console.Beep();
        AnsiConsole.Markup("[red]Error occured: [/]");
        AnsiConsole.WriteLine(error);
        Console.ReadKey(true);
    }

    public static bool Retry()
    {
        return AnsiConsole.Ask<bool>("Would you like to try again <True/False?");
    }

    public static CreateEmployeeDto CreateEmployee()
    {
        var name = AnsiConsole.Ask<string>("Name:");

        var date = GetDOB();
        var dob = new DateTime(day: date.Day, month: date.Month, year: date.Year);

        var number = AnsiConsole.Ask<string>("Phone Number:");
        var email = AnsiConsole.Ask<string>("E-Mail:");
        

        var employee = new CreateEmployeeDto
        {
            Name = name,
            DateOfBirth = dob,
            PhoneNumber = number,
            EmailAddress = email
        };

        return employee;
    }

    private static DateOnly GetDOB()
    {
        DateOnly date;
        do
        {
            date = AnsiConsole.Ask<DateOnly>("Date of Birth? (mm-dd-yyyy)");
        } while (!AnsiConsole.Confirm($"Is {date.Month}-{date.Day}-{date.Year} (mm/dd/yyyy) correct?"));
        return date;
    }
}

