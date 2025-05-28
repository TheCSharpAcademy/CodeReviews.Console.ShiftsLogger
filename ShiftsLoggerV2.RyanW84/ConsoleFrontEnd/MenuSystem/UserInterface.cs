using ConsoleFrontEnd.Models;
using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public class UserInterface
{
    // UI method: Handles user interaction
    // and displays the results of the operations

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
        AnsiConsole.WriteLine("\nPlease enter filter criteria for shifts (leave blank to skip):");
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
        var shiftId = AnsiConsole.Ask<int>(
            $"Enter [green]Shift ID[/] to view details of a single Shift:"
        );
        return shiftId;
    }

    public static Locations CreateLocationUI()
    {
        // 1. Gather user input (UI Layer)
        var name = AnsiConsole.Ask<string>("Enter [green]Location Name[/]:");
        var address = AnsiConsole.Ask<string>("Enter [green]Address[/]:");
        var city = AnsiConsole.Ask<string>("Enter [green]Town or City[/]:");
        var state = AnsiConsole.Ask<string>("Enter [green]State or County[/]:");
        var zip = AnsiConsole.Ask<string>("Enter [green]Zip Code or Post Code[/]:");
        var country = AnsiConsole.Ask<string>("Enter [green]Country[/]:");

        var createdLocation = new Locations
        {
            Name = name,
            Address = address,
            TownOrCity = city,
            StateOrCounty = state,
            ZipOrPostCode = zip,
            Country = country,
        };

        return createdLocation;
    }
}
