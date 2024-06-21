using System.Diagnostics.CodeAnalysis;
using Shiftlogger.UI.DTOs;
using Spectre.Console;
namespace Shiftlogger.UI;

internal class VisualizationEngine
{

    internal static void DisplayContinueMessage()
    {
        AnsiConsole.Markup($"\n[yellow]Press [blue]Enter[/] To Continue[/]\n");
        Console.ReadLine();
    }

    public static Table CreateTable(string title, string footer)
    {
        var table = new Table();
        table.Title($"[yellow]{title}[/]");
        table.Caption(footer);
        table.Border = TableBorder.Square;
        table.ShowRowSeparators = true;
        return table;
    }

    internal static void DisplayWorkers(List<WorkerRequestDto>? workers, [AllowNull] string title)
    {
        if (title == null)
        {
            title = "";
        }
        var table = CreateTable(title, $"[yellow]Displaying [blue]{workers.Count()}[/] records[/]");
        table.AddColumns(["[green]Id[/]", "[green]Name[/]", "[green]Email[/]", "[green]Phone Number[/]"]);
        foreach (var worker in workers)
        {
            table.AddRow(worker.id.ToString(), worker.name, worker.email, worker.phoneNumber);
        }
        AnsiConsole.Write(table);
    }

    internal static void DisplayShifts(List<ShiftReqestDto> shifts, [AllowNull] string title)
    {
        if (title == null)
        {
            title = "";
        }
        var table = CreateTable(title, $"[yellow]Displaying [blue]{shifts.Count()}[/] records[/]");
        table.AddColumns(["[green]Id[/]", "[green]Start DateTime[/]", "[green]End DateTime[/]", "[green]Duration[/]", "Worker Name"]);
        foreach (var shift in shifts)
        {
            table.AddRow(shift.id.ToString(), shift.startDateTime.ToString(), shift.endDateTime.ToString(), shift.duration, shift.worker.name);
        }
        AnsiConsole.Write(table);
    }

    internal static void DisplayCancelOperation()
    {
        AnsiConsole.Markup("[maroon]The Operation is Canceled by the User.[/]\n");
        DisplayContinueMessage();
    }

    internal static void DisplayFailureMessage(string message)
    {
        AnsiConsole.Markup($"[maroon]{message}[/]");
        DisplayContinueMessage();
    }

    internal static void DisplaySuccessMessage(string message)
    {
        AnsiConsole.Markup($"[green]{message}[/]");
        DisplayContinueMessage();
    }

}