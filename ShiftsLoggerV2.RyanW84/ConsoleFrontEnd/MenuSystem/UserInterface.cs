using ShiftsLoggerV2.RyanW84.Dtos;
using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public class UserInterface
{
    // UI method: Handles user interaction
    // and displays the results of the operations

    public static ShiftsDto CreateShiftUi()
    {
        // 1. Gather user input (UI Layer)
        AnsiConsole.WriteLine("\nPlease enter the following details for the shift:");
        var startTime = AnsiConsole.Ask<DateTime>("Enter [green]Start Time[/]:");
        var endTime = AnsiConsole.Ask<DateTime>("Enter [green]End Time[/]:");
        var locationId = AnsiConsole.Ask<int>("Enter [green]Location ID[/]:");
        var workerId = AnsiConsole.Ask<int>("Enter [green]Worker ID[/]:");

        // Corrected instantiation of ShiftsDto
        var newShift = new ShiftsDto
        {
            StartTime = startTime,
            EndTime = endTime,
            LocationId = locationId,
            WorkerId = workerId,
        };

        return newShift;
    }

    public static void DisplayAllShiftsTable(ShiftsDto shiftsDto)
    {
        Table table = new Table();
        table.AddColumn("Worker ID");
        table.AddColumn("Location ID");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        table.AddRow(
            shiftsDto.WorkerId.ToString(),
            shiftsDto.LocationId.ToString(),
            shiftsDto.StartTime.ToString(),
            shiftsDto.EndTime.ToString(),
            (shiftsDto.EndTime - shiftsDto.StartTime).ToString()
        );
        AnsiConsole.Write(table);
    }

    public static LocationsDto CreateLocationUI()
    {
        // 1. Gather user input (UI Layer)
        var name = AnsiConsole.Ask<string>("Enter [green]Location Name[/]:");
        var address = AnsiConsole.Ask<string>("Enter [green]Address[/]:");
        var city = AnsiConsole.Ask<string>("Enter [green]Town or City[/]:");
        var state = AnsiConsole.Ask<string>("Enter [green]State or County[/]:");
        var zip = AnsiConsole.Ask<string>("Enter [green]Zip Code or Post Code[/]:");
        var country = AnsiConsole.Ask<string>("Enter [green]Country[/]:");

        // 2. Create DTO for the service (acts like a controller would)
        var newLocation = new LocationsDto
        {
            Name = name,
            Address = address,
            TownOrCity = city,
            StateOrCounty = state,
            ZipOrPostCode = zip,
            Country = country,
        };

        return newLocation;
    }
}
