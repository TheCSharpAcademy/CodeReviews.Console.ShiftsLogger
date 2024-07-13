﻿namespace ShiftLoggerUI.UI;

using SharedLibrary.DTOs;
using SharedLibrary.Models;
using ShiftLoggerUI.Enums;
using Spectre.Console;

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

    public static void DisplayAllShifts(ICollection<ShiftDto> shifts)
    {
        Header();

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Expand();
        table.BorderColor(Color.DarkTurquoise);

        table.AddColumn(new TableColumn("[bold]Id[/]").Centered());
        table.AddColumn(new TableColumn("[bold]EmployeeId[/]"));
        table.AddColumn(new TableColumn("[bold]StartTime[/]").Centered());
        table.AddColumn(new TableColumn("[bold]EndTime[/]"));
        table.AddColumn(new TableColumn("[bold]Duration[/]"));

        bool isAlternate = false;
        foreach (var item in shifts)
        {
            var rowColor = isAlternate ? "grey" : "white";
            table.AddRow(
                $"[{rowColor}]{item.Id}[/]",
                $"[{rowColor}]{item.EmployeeId}[/]",
                $"[{rowColor}]{item.StartTime}[/]",
                $"[{rowColor}]{item.EndTime}[/]",
                $"[{rowColor}]{item.Duration}[/]"
            );
            isAlternate = !isAlternate;
        }

        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("\n[italic]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    public static void DisplayShift(ShiftDto shift)
    {
        Header();

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Expand();
        table.BorderColor(Color.DarkTurquoise);

        table.AddColumn(new TableColumn("[bold]Id[/]").Centered());
        table.AddColumn(new TableColumn("[bold]EmployeeId[/]"));
        table.AddColumn(new TableColumn("[bold]StartTime[/]").Centered());
        table.AddColumn(new TableColumn("[bold]EndTime[/]"));
        table.AddColumn(new TableColumn("[bold]Duration[/]"));

        var rowColor = "grey";
        table.AddRow(
            $"[{rowColor}]{shift.Id}[/]",
            $"[{rowColor}]{shift.EmployeeId}[/]",
            $"[{rowColor}]{shift.StartTime}[/]",
            $"[{rowColor}]{shift.EndTime}[/]",
            $"[{rowColor}]{shift.Duration}[/]"
        );

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

    private static (string name, DateTime dob, string number, string email) CollectEmployeeInfo()
    {
        var name = AnsiConsole.Ask<string>("Name:");
        var date = GetDOB();
        var dob = new DateTime(day: date.Day, month: date.Month, year: date.Year);
        var number = AnsiConsole.Ask<string>("Phone Number:");
        var email = AnsiConsole.Ask<string>("E-Mail:");

        return (name, dob, number, email);
    }

    public static CreateEmployeeDto CreateEmployee()
    {
        var (name, dob, number, email) = CollectEmployeeInfo();

        var employee = new CreateEmployeeDto
        {
            Name = name,
            DateOfBirth = dob,
            PhoneNumber = number,
            EmailAddress = email
        };

        return employee;
    }

    public static UpdateEmployeeDto UpdateEmployee()
    {
        var (name, dob, number, email) = CollectEmployeeInfo();

        var employee = new UpdateEmployeeDto
        {
            Name = name,
            DateOfBirth = dob,
            PhoneNumber = number,
            EmailAddress = email
        };

        return employee;
    }

    private static (DateTime StartTime, DateTime EndTime) CollectShiftInfo()
    {
        var StartTime = AnsiConsole.Ask<DateTime>("Start Time? (mm-dd-yyyy HH:mm:ss)");
        var EndTime = AnsiConsole.Ask<DateTime>("End Time? (mm-dd-yyyy HH:mm:ss)");

        return (StartTime, EndTime);
    }

    public static UpdateShiftDto UpdateShift()
    {
        var (startTime, endTime) = CollectShiftInfo();

        var shift = new UpdateShiftDto
        {
            StartTime = startTime,
            EndTime = endTime
        };

        return shift;
    }

    public static CreateShiftDto CreateShift(int employeeId)
    {
        var (startTime, endTime) = CollectShiftInfo();

        var shift = new CreateShiftDto
        {
            EmployeeId = employeeId,
            StartTime = startTime,
            EndTime = endTime
        };

        return shift;
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

