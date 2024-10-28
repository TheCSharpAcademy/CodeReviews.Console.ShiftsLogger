using ShiftsLoggerCLI.Models;
using Spectre.Console;

namespace ShiftsLoggerCLI;

public static class VisualisationEngine
{
    
    public static void DisplayWorkers(List<WorkerRead> workers)
    {
        var table = new Table().Title("[yellow]--Workers--[/]");
        table.AddColumns("[yellow]Id[/]", "[yellow]Name[/]", "[yellow]Email[/]", "[yellow]Position[/]", "[yellow]HireDate(dd/mm/yyyy)[/]");
        table.Alignment(Justify.Center);
        table.Expand();
        foreach (var worker in workers)
        {
            table.AddRow($"[darkturquoise]{worker.Id.ToString()}[/]",$"[darkturquoise]{worker.Name}[/]", $"[darkturquoise]{worker.Email}[/]",
                $"[darkturquoise]{worker.Position}[/]", $"[darkturquoise]{worker.HireDate:dd/MM/yyyy}[/]");
        }
        AnsiConsole.Write(table);
        MenuBuilder.EnterButtonPause();
    }

    public static void DisplayWorker(WorkerRead worker,List<ShiftRead> shifts)
    {
        var tree = new Tree($"[yellow]{worker}[/]").Guide(TreeGuide.Line);
        foreach (var shift in shifts)
        {
            tree.AddNode(shift.ToString());
        }
        AnsiConsole.Write(tree);
        MenuBuilder.EnterButtonPause();
    }
    
}