using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using ShiftsLoggerV2.RyanW84.Services;
using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public class UserInterface
{
    // UI method: Handles user interaction
    // and displays the results of the operations

    public async Task CreateLocationUI(LocationService locationService)
    {
        // 1. Gather user input (UI Layer)
        var name = AnsiConsole.Ask<string>("Enter [green]Location Name[/]:");
        var address = AnsiConsole.Ask<string>("Enter [green]Address[/]:");
        var city = AnsiConsole.Ask<string>("Enter [green]Town or City[/]:");
        var state = AnsiConsole.Ask<string>("Enter [green]State or County[/]:");
        var zip = AnsiConsole.Ask<string>("Enter [green]Zip Code or Post Code[/]:");
        var country = AnsiConsole.Ask<string>("Enter [green]Country[/]:");

        // 2. Create DTO for the service (acts like a controller would)
        var newLocation = new LocationApiRequestDto
        {
            Name = name,
            Address = address,
            TownOrCity = city,
            StateOrCounty = state,
            ZipOrPostCode = zip,
            Country = country,
        };

        // 3. Call the service (business logic)
        var result = await locationService.CreateLocation(newLocation);

        // 4. Handle the result (UI Layer)
        if (!result.RequestFailed)
            AnsiConsole.MarkupLine("[green]Location created successfully![/]");
        else
            AnsiConsole.MarkupLine($"[red]{result.Message}[/]");
    }

    public async Task GetLocationUI(LocationService locationService)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Fetching locations...[/]");
        var locationOptions = new LocationFilterOptions
        {
            LocationId = AnsiConsole.Confirm("Filter by [green]Location ID[/]?", false)
                ? AnsiConsole.Ask<int>("Enter [green]Location ID[/]:")
                : 0, // or use a nullable int? and set to null if not filtering
            SortBy = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select [green]Sort By[/]:")
                    .AddChoices(
                        "None",
                        "location_id",
                        "name",
                        "address",
                        "town_or_city",
                        "state_or_county",
                        "zip_or_post_code",
                        "country"
                    )
            ),
            SortOrder = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select [green]Sort Order[/]:")
                    .AddChoices("None", "asc", "desc")
            ),
        };
        var result = await locationService.GetAllLocations(locationOptions);
        if (result.Data != null && result.Data.Count > 0)
        {
            AnsiConsole.MarkupLine("[green]Locations found:[/]");
            foreach (var location in result.Data)
            {
                AnsiConsole.MarkupLine($"[green]{location.Name}[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]No locations found.[/]");
        }
    }

    public async Task UpdateLocationUI(LocationService locationService, int id)
    {
        // 1. Gather user input (UI Layer)
        var name = AnsiConsole.Confirm("Do you want to update the [green]Location Name[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Location Name[/]:")
            : null;
        var address = AnsiConsole.Confirm("Do you want to update the [green]Address[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Location Name[/]:")
            : null;
        var city = AnsiConsole.Confirm("Do you want to update the [green]Town or City[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Town or City[/]:")
            : null;
        var state = AnsiConsole.Confirm(
            "Do you want to update the [green]State or County[/]?",
            true
        )
            ? AnsiConsole.Ask<string>("Enter [green]State or County[/]:")
            : null;
        var zip = AnsiConsole.Confirm(
            "Do you want to update the [green]Zip Code or Post Code[/]?",
            true
        )
            ? AnsiConsole.Ask<string>("Enter [green]Zip Code or Post Code[/]:")
            : null;

        var country = AnsiConsole.Confirm("Do you want to update the [green]Country[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Country[/]:")
            : null;

        var existing = await locationService.GetLocationById(id);
        if (existing.Data == null)
        {
            AnsiConsole.MarkupLine("[red]Location not found.[/]");
            return;
        }

        // 2. Create DTO for the service (acts like a controller would)
        var updatedLocation = new LocationApiRequestDto
        {
            Name = name ?? existing.Data.Name,
            Address = address ?? existing.Data.Address,
            TownOrCity = city ?? existing.Data.TownOrCity,
            StateOrCounty = state ?? existing.Data.StateOrCounty,
            ZipOrPostCode = zip ?? existing.Data.ZipOrPostCode,
            Country = country ?? existing.Data.Country,
        };
        // 3. Call the service (business logic)
        var result = await locationService.UpdateLocation(id, updatedLocation);
        if (!result.RequestFailed)
            AnsiConsole.MarkupLine("[green]Location updated successfully![/]");
        else
            AnsiConsole.MarkupLine($"[red]{result.Message}[/]");
    }

    public async Task DeleteLocationUI(LocationService locationService, int id)
    {
        var result = await locationService.DeleteLocation(id);
        if (!result.RequestFailed)
            AnsiConsole.MarkupLine("[green]Location deleted successfully![/]");
        else
            AnsiConsole.MarkupLine($"[red]{result.Message}[/]");
    }

    // --- Worker Methods ---

    public async Task CreateWorkerUI(WorkerService workerService)
    {
        Console.Clear();
        var name = AnsiConsole.Ask<string>("Enter [green]Worker Name[/]:");
        var phone = AnsiConsole.Ask<string>("Enter [green]Phone Number[/]:");
        var email = AnsiConsole.Ask<string>("Enter [green]Email Address[/]:");

        var dto = new WorkerApiRequestDto
        {
            Name = name,
            PhoneNumber = phone,
            Email = email,
        };

        var result = await workerService.CreateWorker(dto);
        if (!result.RequestFailed)
            AnsiConsole.MarkupLine("[green]Worker created successfully![/]");
        else
            AnsiConsole.MarkupLine($"[red]{result.Message}[/]");
    }

    public async Task GetWorkerUI(WorkerService workerService)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Fetching workers...[/]");

        var workerOptions = new WorkerFilterOptions
        {
            WorkerId = AnsiConsole.Confirm("Filter by [green]Worker ID[/]?", false)
                ? AnsiConsole.Ask<int>("Enter [green]Worker ID[/]:")
                : null,
            Name = AnsiConsole.Confirm("Filter by [green]Name[/]?", false)
                ? AnsiConsole.Ask<string>("Enter [green]Name[/]:")
                : null,
            Phone = AnsiConsole.Confirm("Filter by [green]Phone Number[/]?", false)
                ? AnsiConsole.Ask<string>("Enter [green]Phone Number[/]:")
                : null,
            Email = AnsiConsole.Confirm("Filter by [green]Email[/]?", false)
                ? AnsiConsole.Ask<string>("Enter [green]Email[/]:")
                : null,
            SortBy = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select [green]Sort By[/]:")
                    .AddChoices("None", "worker_id", "name", "phone", "email")
            ),
            SortOrder = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select [green]Sort Order[/]:")
                    .AddChoices("None", "asc", "desc")
            ),
            Search = AnsiConsole.Confirm("Search by [green]keyword[/]?", false)
                ? AnsiConsole.Ask<string>("Enter [green]Search keyword[/]:")
                : null,
        };

        var result = await workerService.GetAllWorkers(workerOptions);
        if (result.Data != null && result.Data.Count > 0)
        {
            var table = new Table()
                .AddColumn("Worker ID")
                .AddColumn("Name")
                .AddColumn("Phone Number")
                .AddColumn("Email");

            foreach (var worker in result.Data)
            {
                table.AddRow(
                    worker.WorkerId.ToString(),
                    worker.Name ?? "N/A",
                    worker.PhoneNumber ?? "N/A",
                    worker.Email ?? "N/A"
                );
            }

            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine("[red]No workers found.[/]");
        }
    }

    public async Task UpdateWorkerUI(WorkerService workerService, int id)
    {
        // 1. Gather user input (UI Layer)
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Updating a worker...[/]");

        var name = AnsiConsole.Confirm("Do you want to update the [green]Worker Name[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Worker Name[/]:")
            : null;
        var phone = AnsiConsole.Confirm("Do you want to update the [green]Phone Number[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Phone Number[/]:")
            : null;
        var email = AnsiConsole.Confirm("Do you want to update the [green]Email Address[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Email Address[/]:")
            : null;

        var existing = await workerService.GetWorkerById(id);
        if (existing.Data == null)
        {
            AnsiConsole.MarkupLine("[red]Worker not found.[/]");
            return;
        }

        // Use WorkerApiRequestDto as required by the service
        var updatedWorker = new WorkerApiRequestDto
        {
            WorkerId = id,
            Name = name ?? existing.Data.Name ?? string.Empty,
            PhoneNumber = phone ?? existing.Data.PhoneNumber ?? string.Empty,
            Email = email ?? existing.Data.Email ?? string.Empty,
        };

        var result = await workerService.UpdateWorker(id, updatedWorker);
        if (!result.RequestFailed)
            AnsiConsole.MarkupLine("[green]Worker updated successfully![/]");
        else
            AnsiConsole.MarkupLine($"[red]{result.Message}[/]");
    }

    public async Task DeleteWorkerUI(WorkerService workerService, int id)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Deleting a worker...[/]");
        var result = await workerService.DeleteWorker(id);
        if (!result.RequestFailed)
            AnsiConsole.MarkupLine("[green]Worker deleted successfully![/]");
        else
            AnsiConsole.MarkupLine($"[red]{result.Message}[/]");
    }

    // --- Shift Methods ---
    public async Task CreateShiftUI(ShiftService shiftService)
    {
        // 1. Gather user input (UI Layer)
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Creating a new shift...[/]");
        var startTime = AnsiConsole.Ask<DateTime>("Enter [green]Start Time[/]:");
        var endTime = AnsiConsole.Ask<DateTime>("Enter [green]End Time[/]:");
        var locationId = AnsiConsole.Ask<int>("Enter [green]Location ID[/]:");
        var workerId = AnsiConsole.Ask<int>("Enter [green]Worker ID[/]:");
        var newShift = new ShiftApiRequestDto
        {
            StartTime = startTime,
            EndTime = endTime,
            LocationId = locationId,
            WorkerId = workerId,
        };
        var result = await shiftService.CreateShift(newShift);
        if (!result.RequestFailed)
            AnsiConsole.MarkupLine("[green]Shift created successfully![/]");
        else
            AnsiConsole.MarkupLine($"[red]{result.Message}[/]");
    }
}
