using Spectre.Console;
using WorkerShiftsUI.Models;

namespace WorkerShiftsUI.UserInteractions;
public static class UserInteraction
{
    internal static void ShowWorkers(List<Worker> workers)
    {
        // Show prompt to select a worker

        var workerSelector = new SelectionPrompt<Worker>
        {
            Title = "[bold][blue]Select a worker to view their shifts[/][/]",
        };
        workerSelector.AddChoices(workers);
        workerSelector.UseConverter(worker => worker?.Name ?? "Unknown");

        var workerSelected = AnsiConsole.Prompt(workerSelector);

        if (workerSelected != null)
        {
            ShowWorkerDetailsTable(workerSelected);
        }
    }

    internal static void ShowWorkerDetailsTable(Worker worker)
    {
        var table = new Table()
            .AddColumn("Shift ID")
            .AddColumn("Start Time")
            .AddColumn("End Time")
            .Title("[bold][blue]Worker Details[/][/]");

        if (worker.Shifts != null)
        {
            var count = 1;
            foreach (var shift in worker.Shifts)
            {
                table.AddRow(count.ToString(), shift.StartTime.ToString(), shift.EndTime.ToString());
                count++;
            }
        }

        AnsiConsole.Write(table);
    }
}
