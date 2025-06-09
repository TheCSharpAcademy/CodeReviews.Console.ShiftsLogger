using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;
using ConsoleFrontEnd.Services;
using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public class UserInterface
{
    private readonly ShiftService shiftService;

	// UI method: Handles user interaction
	// and displays the results of the operations

	// Helpers
	public void ContinueAndClearScreen( )
	{
		{
			Console.WriteLine("\nPress any key to continue");
			Console.ReadKey();
			Console.Clear();
		}
	}

	// Shifts
	public ShiftFilterOptions FilterShiftsUi()
    {
        var filterOptions = new ShiftFilterOptions
        {
            WorkerId = null,
            LocationId = null,
            StartTime = null,
            EndTime = null,
        };
        // 1. Gather user input (UI Layer)
        AnsiConsole.WriteLine(
            "\nPlease enter filter criteria for SelectedShift (leave blank to skip):"
        );
        var filterCriteria = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Dp you wish to apply any Filters?:[/]")
                .AddChoices("Yes", "No")
        );

        if (filterCriteria == "No")
        {
            AnsiConsole.MarkupLine("[green]No filters applied.[/]");
            return filterOptions; // Return default filter options with null values
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]Choose which filters...[/]");
            filterOptions.WorkerId = AnsiConsole.Ask<int?>(
                "Enter [green]Worker ID[/] (or leave blank):",
                defaultValue: null
            );
            filterOptions.LocationId = AnsiConsole.Ask<int?>(
                "Enter [green]Location ID[/] (or leave blank):",
                defaultValue: null
            );
            filterOptions.StartTime = AnsiConsole.Ask<DateTime?>(
                "Enter [green]Start Time[/] (or leave blank):",
                defaultValue: null
            );
            filterOptions.EndTime = AnsiConsole.Ask<DateTime?>(
                "Enter [green]End Time[/] (or leave blank):",
                defaultValue: null
            );

            return filterOptions; // Return the filter options with user input
        }
    }

    public Shifts CreateShiftUi()
    {
        // 1. Gather user input (UI Layer)
        AnsiConsole.WriteLine("\nPlease enter the following details for the shift:");
        var startTime = AnsiConsole.Ask<DateTime>("Enter [green]Start Time[/]:");
        var endTime = AnsiConsole.Ask<DateTime>("Enter [green]End Time[/]:");
        var locationId = AnsiConsole.Ask<int>("Enter [green]Location ID[/]:");
        var workerId = AnsiConsole.Ask<int>("Enter [green]Worker ID[/]:");

        var createdShift = new Shifts
        {
            StartTime = startTime,
            EndTime = endTime,
            LocationId = locationId,
            WorkerId = workerId,
        };

        return createdShift;
    }

    public void DisplayShiftsTable(IEnumerable<Shifts> shifts)
    {
        Table table = new Table();
        table.AddColumn("Worker ID");
        table.AddColumn("Location ID");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (var shift in shifts)
        {
            table.AddRow(
                shift.WorkerId.ToString(),
                shift.LocationId.ToString(),
                shift.StartTime.ToString("g"),
                shift.EndTime.ToString("g"),
                (shift.EndTime - shift.StartTime).ToString(@"hh\:mm\:ss")
            );
        }

        AnsiConsole.Write(table);
    }

    public int GetShiftByIdUi()
    {
        // 1. Gather user input (UI Layer)
        var shiftId = AnsiConsole.Ask<int>($"Enter [green]Shift ID:[/] ");

        return shiftId;
    }

    public Shifts UpdateShiftUi(List<Shifts> existingShift)
    {
        var startTime = AnsiConsole.Ask<DateTime?>(
            "Enter [green]Start Time[/] (leave blank to keep current):",
            existingShift[0].StartTime.DateTime
        );
        var endTime = AnsiConsole.Ask<DateTime?>(
            "Enter [green]End Time[/] (leave blank to keep current):",
            existingShift[0].EndTime.DateTime
        );
        var locationId = AnsiConsole.Ask<int?>(
            "Enter [green]Location ID[/] (leave blank to keep current):",
            existingShift[0].LocationId
        );
        var workerId = AnsiConsole.Ask<int?>(
            "Enter [green]Worker ID[/] (leave blank to keep current):",
            existingShift[0].WorkerId
        );

        var updatedShift = new Shifts
        {
            StartTime = startTime ?? existingShift[0].StartTime,
            EndTime = endTime ?? existingShift[0].EndTime,
            LocationId = locationId ?? existingShift[0].LocationId,
            WorkerId = workerId ?? existingShift[0].WorkerId,
        };

        return updatedShift;
    }

    // Workers
    public WorkerFilterOptions FilterWorkersUi()
    {
        var filterOptions = new WorkerFilterOptions
        {
            WorkerId = null,
            Name = null,
            PhoneNumber = null,
            Email = null,
        };
        // 1. Gather user input (UI Layer)
        AnsiConsole.WriteLine("\nPlease enter filter criteria for Workers (leave blank to skip):");
        var filterCriteria = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Do you wish to apply any Filters?:[/]")
                .AddChoices("Yes", "No")
        );
        if (filterCriteria == "No")
        {
            AnsiConsole.MarkupLine("[green]No filters applied.[/]");
            return filterOptions; // Return default filter options with null values
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]Choose which filters...[/]");
            filterOptions.WorkerId = AnsiConsole.Ask<int?>(
                "Enter [green]Worker ID[/] (or leave blank):",
                defaultValue: null
            );
            filterOptions.Name = AnsiConsole.Ask<string?>(
                "Enter [green]Name[/] (or leave blank):",
                defaultValue: null
            );
            filterOptions.PhoneNumber = AnsiConsole.Ask<string?>(
                "Enter [green]Phone Number[/] (or leave blank):",
                defaultValue: null
            );
            filterOptions.Email = AnsiConsole.Ask<string?>(
                "Enter [green]Email[/] (or leave blank):",
                defaultValue: null
            );
            return filterOptions; // Return the filter options with user input
        }
    }

    public Workers CreateWorkerUi()
    {
        // 1. Gather user input (UI Layer)
        AnsiConsole.WriteLine("\nPlease enter the following details for the worker:");
        var name = AnsiConsole.Ask<string>("Enter [green]Name[/]:");
        var email = AnsiConsole.Ask<string>("Enter [green]Email[/]:");
        var phoneNumber = AnsiConsole.Ask<string>("Enter [green]Phone Number[/]:");
        var createdWorker = new Workers
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
        };

        return createdWorker;
    }

    public void DisplayWorkersTable(IEnumerable<Workers?> workers)
    {
        Table table = new Table();
        table.AddColumn("Worker ID");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");

        foreach (var worker in workers)
        {
            if (worker != null)
            {
                table.AddRow(
                    worker.WorkerId.ToString(),
                    worker.Name,
                    worker.Email,
                    worker.PhoneNumber
                );
            }
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        Console.Clear();
    }

    public int GetWorkerByIdUi()
    {
        // 1. Gather user input (UI Layer)
        var workerId = AnsiConsole.Ask<int>($"Enter [green]Worker ID:[/] ");
        return workerId;
    }

    public Workers UpdateWorkerUi(List<Workers> existingWorker)
    {
        var name = AnsiConsole.Ask<string>(
            "Enter [green]Name[/] (leave blank to keep current):",
            existingWorker[0].Name
        );
        var email = AnsiConsole.Ask<string>(
            "Enter [green]Email[/] (leave blank to keep current):",
            existingWorker[0].Email
        );
        var phoneNumber = AnsiConsole.Ask<string>(
            "Enter [green]Phone Number[/] (leave blank to keep current):",
            existingWorker[0].PhoneNumber
        );
        var updatedWorker = new Workers
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
        };
        return updatedWorker;
    }
}
