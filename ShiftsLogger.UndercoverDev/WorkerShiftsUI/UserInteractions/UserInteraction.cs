using System.Globalization;
using Spectre.Console;
using WorkerShiftsUI.Models;
using WorkerShiftsUI.Utilities;

namespace WorkerShiftsUI.UserInteractions;
public static class UserInteraction
{
    internal static void ShowWorkers(List<Worker> workers)
    {
        workers.Insert(0, new Worker { Name = "Back" });

        var workerSelector = new SelectionPrompt<Worker>
        {
            Title = "[bold][blue]Select a worker to view their shifts[/][/]",
        };
        workerSelector.AddChoices(workers);
        workerSelector.UseConverter(worker => worker?.Name ?? "Unknown");

        var workerSelected = AnsiConsole.Prompt(workerSelector);

        if (workerSelected == null || workerSelected.Name == "Back")
        {
            return;
        }

        if (workerSelected != null && workerSelected.Shifts.Count != 0)
        {
            ShowWorkerDetailsTable(workerSelected);
        }
        else
        {
            AnsiConsole.MarkupLine("[bold][red]No shifts found for this worker.[/][/]");
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

    internal static Worker GetWorkerDetails()
    {
        //prompt with validation
        var name = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Enter [green]Worker's Name[/][/]:")
                    .PromptStyle("blue")
                    .ValidationErrorMessage("[red]Name cannot be empty[/]")
                    .Validate(name =>
                    {
                        return !string.IsNullOrWhiteSpace(name);
                    })
        );

        var worker = new Worker { Name = name, Shifts = [] };

        if (AnsiConsole.Confirm("Would you like to add a shift for this worker?"))
        {
            var shift = GetShiftDetails();
            shift.WorkerId = worker.WorkerId;
            shift.WorkerName = worker.Name;
            
            worker.Shifts.Add(shift);
        }

        return worker;
    }

    internal static Worker GetWorkerOptionInput(List<Worker> workers)
    {
        workers.Insert(0, new Worker { Name = "Back" });
        
        var workerSelector = new SelectionPrompt<Worker>
        {
            Title = "[bold][blue]Select a worker to delete[/][/]",
        };
        workerSelector.AddChoices(workers);
        workerSelector.UseConverter(worker => worker?.Name?? "Unknown");

        var workerSelected = AnsiConsole.Prompt(workerSelector);

        return workerSelected;
    }

    private static Shift GetShiftDetails()
    {
        var now = DateTime.Now;
        const string timeFormat = "yyyy-MM-dd HH:mm";

        var startTimeString = Validations.GetValidatedTimeInput("Enter shift start time", timeFormat, now);
        var startTime = DateTime.ParseExact(startTimeString, timeFormat, CultureInfo.InvariantCulture);

        var endTimeString = Validations.GetValidatedTimeInput("Enter shift end time", timeFormat, now, startTime);
        var endTime = DateTime.ParseExact(endTimeString, timeFormat, CultureInfo.InvariantCulture);

        return new Shift { StartTime = startTime, EndTime = endTime };
    }
}
