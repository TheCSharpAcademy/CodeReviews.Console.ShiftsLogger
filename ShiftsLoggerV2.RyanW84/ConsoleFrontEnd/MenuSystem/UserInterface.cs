using ConsoleFrontEnd.Models;
using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public class UserInterface
{
    // UI method: Handles user interaction
    // and displays the results of the operations

    public static Shifts CreateShiftUi()
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

    public static void DisplayAllShiftsTable(List<Shifts> shift)
    {
        Table table = new Table();
        table.AddColumn("Worker ID");
        table.AddColumn("Location ID");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (var s in shift)
        {
            table.AddRow(
                s.WorkerId.ToString(),
                s.LocationId.ToString(),
                s.StartTime.ToString("g"),
                s.EndTime.ToString("g"),
                (s.EndTime - s.StartTime).ToString(@"hh\:mm\:ss")
            );
        }

        AnsiConsole.Write(table);
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

        return new Locations
        {
            Name = name,
            Address = address,
            TownOrCity = city,
            StateOrCounty = state,
            ZipOrPostCode = zip,
            Country = country,
        };
    }
}
