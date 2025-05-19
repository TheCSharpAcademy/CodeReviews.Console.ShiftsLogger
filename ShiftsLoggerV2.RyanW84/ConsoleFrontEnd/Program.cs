using System.Net.Http.Json;
using Azure;
using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using Spectre.Console;

class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7009") };

        if (!await CheckApiConnection(httpClient))
        {
            AnsiConsole.MarkupLine("[red]Exiting application due to API connection failure.[/]");
            return;
        }

        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        "Add Worker",
                        "Add Location",
                        "Add Shift",
                        "View Workers",
                        "View Locations",
                        "View Shifts",
                        "Exit"
                    )
            );

            switch (choice)
            {
                case "Add Worker":
                    await AddWorker(httpClient);
                    break;
                case "Add Location":
                    await AddLocation(httpClient);
                    break;
                case "Add Shift":
                    await AddShift(httpClient);
                    break;
                case "View Workers":
                    await ViewWorkers(httpClient);
                    break;
                case "View Locations":
                    await ViewLocations(httpClient);
                    break;
                case "View Shifts":
                    await ViewShifts(httpClient);
                    break;
                case "Exit":
                    return;
            }
        }
    }

    static async Task<bool> CheckApiConnection(HttpClient httpClient)
    {
        try
        {
            for (int i = 0; i <= 3; i++)
            {
                Console.WriteLine("Attempting to connect to the back end");
                await Task.Delay(1000);
                var response = await httpClient.GetAsync("api/shifts");

                if (response.IsSuccessStatusCode)
                {
                    AnsiConsole.MarkupLine("[green]Successfully connected to the API.[/]");
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    AnsiConsole.MarkupLine("[yellow]API is unavailable. Retrying...[/]");
                    await Task.Delay(5000); // Wait for 5 seconds before retrying
                    continue;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    AnsiConsole.MarkupLine("[red]API endpoint not found. Please check the URL.[/]");
                    return false;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    AnsiConsole.MarkupLine(
                        "[red]Unauthorized access. Please check your credentials.[/]"
                    );
                    return false;
                }
                else
                {
                    return false;
                }
            }
            AnsiConsole.MarkupLine("[red]Failed to connect to the API after multiple attempts.[/]");
            return false;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine(
                $"[red]Error connecting to the API after three attempts: {ex.Message}[/]"
            );
            Console.ReadKey();
            return false;
        }
    }

    static async Task AddWorker(HttpClient httpClient)
    {

        try
        {
            var name = AnsiConsole.Ask<string>("Enter [green]Worker Name[/]:");
            var phone = AnsiConsole.Ask<string>("Enter [green]Phone Number[/]:");
            var email = AnsiConsole.Ask<string>("Enter [green]Email Address[/]:");

            var response = await httpClient.PostAsJsonAsync(
                "api/workers" ,
                new
                {
                    Name = name ,
                    PhoneNumber = phone ,
                    Email = email ,
                }
            );
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine("[green]Worker added successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to add worker.[/]");
            }
        }
        catch (Exception ex)
        {
			Console.WriteLine($"Adding worker failed due to: {ex}");
            throw;
        }
    }

    static async Task AddLocation(HttpClient httpClient)
    {
        var name = AnsiConsole.Ask<string>("Enter [green]Location Name[/]:");
        var address = AnsiConsole.Ask<string>("Enter [green]Address[/]:");
        var city = AnsiConsole.Ask<string>("Enter [green]Town or City[/]:");
        var state = AnsiConsole.Ask<string>("Enter [green]State or County[/]:");
        var zip = AnsiConsole.Ask<string>("Enter [green]Zip Code or Post Code[/]:");
        var country = AnsiConsole.Ask<string>("Enter [green]Country[/]:");

        var response = await httpClient.PostAsJsonAsync(
            "api/locations",
            new
            {
                Name = name,
                Address = address,
                TownOrCity = city,
                StateorCounty = state,
                ZipOrPostCode = zip,
                Country = country,
            }
        );
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("[green]Location added successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed to add location.[/]");
        }
    }

    static async Task AddShift(HttpClient httpClient)
    {
        var workerId = AnsiConsole.Ask<int>("Enter [green]Worker ID[/]:");
        var locationId = AnsiConsole.Ask<int>("Enter [green]Location ID[/]:");
        var startTime = AnsiConsole.Ask<DateTime>("Enter [green]Start Time (yyyy-MM-DD HH:mm)[/]:");
        var endTime = AnsiConsole.Ask<DateTime>("Enter [green]End Time (yyyy-MM-DD HH:mm)[/]:");

        var response = await httpClient.PostAsJsonAsync(
            "api/shifts",
            new
            {
                WorkerId = workerId,
                LocationId = locationId,
                StartTime = startTime,
                EndTime = endTime,
            }
        );
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("[green]Shift added successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed to add location.[/]");
        }
    }

    static async Task ViewWorkers(HttpClient httpClient)
    {
        var response = await httpClient.GetFromJsonAsync<ApiResponseDto<List<WorkerDto>>>(
            "api/workers"
        );
        if (response != null && response.Data != null && !response.RequestFailed)
        {
            var table = new Table()
                .AddColumn("Name")
                .AddColumn("Phone")
                .AddColumn("Email");
            foreach (var worker in response.Data)
            {
                table.AddRow(
                    worker.Name,
                    worker.PhoneNumber,
                    worker.Email
                );
            }
            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed to retrieve workers.[/]");
        }
    }

    static async Task ViewLocations(HttpClient httpClient)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<ApiResponseDto<List<LocationsDto>>>(
                "api/locations"
            );
            if (response != null && response.Data != null && !response.RequestFailed)
            {
                var table = new Table()
                    .AddColumn("Name")
                    .AddColumn("Address")
                    .AddColumn("Town or City")
                    .AddColumn("State or County")
                    .AddColumn("Zip or Postcode")
                    .AddColumn("Country");
                foreach (var location in response.Data)
                {
                    table.AddRow(
                       location.Name,
                        location.Address,
                        location.TownOrCity,
                        location.StateorCounty ,
                        location.ZipOrPostCode ,
                        location.Country
                    );
                }
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine(
                    $"[red]Failed to retrieve shifts: {response?.Message ?? "Unknown error"}[/]"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[red]Get All Shifts failed, see Exception: {ex.Message}[/]");
        }
    }

    static async Task ViewShifts(HttpClient httpClient)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<ApiResponseDto<List<ShiftsDto>>>(
                "api/shifts"
            );
            if (response != null && response.Data != null && !response.RequestFailed)
            {
                var table = new Table()

                    .AddColumn("Worker ID")
                    .AddColumn("Location ID")
                    .AddColumn("Start Time")
                    .AddColumn("End Time");

                foreach (var shift in response.Data)
                {
                    table.AddRow(
                        shift.WorkerId.ToString() ,
                        shift.LocationId.ToString() ,
                        shift.StartTime.ToString("yyyy-MM-d HH:mm:ss (en-GB)") ,
                        shift.EndTime.ToString("yyyy-MM-d HH:mm:ss (en-GB)")
                    );
                }
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine(
                    $"[red]Failed to retrieve shifts: {response?.Message ?? "Unknown error"}[/]"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[red]Get All Shifts failed, see Exception: {ex.Message}[/]");
        }
    }
}
