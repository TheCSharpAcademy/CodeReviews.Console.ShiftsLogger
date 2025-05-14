using System.Net.Http.Json;
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
            // Attempt to send a GET request to the base address or a health-check endpoint
            var response = await httpClient.GetAsync("api/Shift");
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine("[green]Successfully connected to the API.[/]");
                return true;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Failed to connect to the API. Status Code: {response.StatusCode}[/]");
                return false;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error connecting to the API: {ex.Message}[/]");
            return false;
        }
    }

    static async Task AddWorker(HttpClient httpClient)
    {
        var name = AnsiConsole.Ask<string>("Enter [green]Worker Name[/]:");
        var phone = AnsiConsole.Ask<string>("Enter [green]Phone[/]:");
        var email = AnsiConsole.Ask<string>("Enter [green]Email[/]:");

        var response = await httpClient.PostAsJsonAsync(
            "api/workers",
            new
            {
                Name = name,
                Phone = phone,
                Email = email,
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

    static async Task AddLocation(HttpClient httpClient)
    {
        var name = AnsiConsole.Ask<string>("Enter [green]Location Name[/]:");
        var address = AnsiConsole.Ask<string>("Enter [green]Address[/]:");
        var city = AnsiConsole.Ask<string>("Enter [green]City[/]:");
        var state = AnsiConsole.Ask<string>("Enter [green]State[/]:");
        var zip = AnsiConsole.Ask<string>("Enter [green]Zip Code[/]:");
        var country = AnsiConsole.Ask<string>("Enter [green]Country[/]:");

        var response = await httpClient.PostAsJsonAsync(
            "api/locations",
            new
            {
                Name = name,
                Address = address,
                City = city,
                State = state,
                Zip = zip,
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
        var startTime = AnsiConsole.Ask<DateTime>("Enter [green]Start Time (yyyy-MM-dd HH:mm)[/]:");
        var endTime = AnsiConsole.Ask<DateTime>("Enter [green]End Time (yyyy-MM-dd HH:mm)[/]:");

        var response = await httpClient.PostAsJsonAsync(
            "api/Shift",
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
            AnsiConsole.MarkupLine("[red]Failed to add shift.[/]");
        }
    }

    static async Task ViewWorkers(HttpClient httpClient)
    {
        var workers = await httpClient.GetFromJsonAsync<List<dynamic>>("api/Worker");
        if (workers != null)
        {
            var table = new Table()
                .AddColumn("ID")
                .AddColumn("Name")
                .AddColumn("Phone")
                .AddColumn("Email");
            foreach (var worker in workers)
            {
                table.AddRow(
                    ((int)worker.Id).ToString(),
                    (string)worker.Name,
                    (string)worker.Phone,
                    (string)worker.Email
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
        var locations = await httpClient.GetFromJsonAsync<List<dynamic>>("api/Location");
        if (locations != null)
        {
            var table = new Table()
                .AddColumn("ID")
                .AddColumn("Name")
                .AddColumn("Address")
                .AddColumn("City")
                .AddColumn("State")
                .AddColumn("Zip")
                .AddColumn("Country");
            foreach (var location in locations)
            {
                table.AddRow(
                    ((int)location.Id).ToString(),
                    (string)location.Name,
                    (string)location.Address,
                    (string)location.City,
                    (string)location.State,
                    (string)location.Zip,
                    (string)location.Country
                );
            }
            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed to retrieve locations.[/]");
        }
    }

	static async Task ViewShifts(HttpClient httpClient)
	{
		try
		{
			var shifts = await httpClient.GetFromJsonAsync<List<dynamic>>("api/Shift");
			if (shifts != null)
			{
				var table = new Table()
					.AddColumn("ID")
					.AddColumn("Worker ID")
					.AddColumn("Location ID")
					.AddColumn("Start Time")
					.AddColumn("End Time");
				foreach (var shift in shifts)
				{
					table.AddRow(
						((int)shift.Id).ToString() ,
						((int)shift.WorkerId).ToString() ,
						((int)shift.LocationId).ToString() ,
						((DateTime)shift.StartTime).ToString("yyyy-MM-dd HH:mm") ,
						((DateTime)shift.EndTime).ToString("yyyy-MM-dd HH:mm")
					);
				}
				AnsiConsole.Write(table);
			}
			else
			{
				AnsiConsole.MarkupLine("[red]Failed to retrieve shifts.[/]");
			}
		}
		catch (Exception ex)
		{
			AnsiConsole.MarkupLine($"[red]Error retrieving shifts: {ex.Message}[/]");
		}
	}
}
