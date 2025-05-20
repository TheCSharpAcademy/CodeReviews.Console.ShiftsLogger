using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Services;

using Spectre.Console;

using System.Net.Http.Json;

class Program
{
	private static async Task Main(string[] args)
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
						"Add Worker" ,
						"Add Location" ,
						"Add Shift" ,
						"View Workers" ,
						"View Locations" ,
						"View Shifts" ,
						"Exit"
					)
			);

			switch (choice)
			{
				case "Add Worker":
					await WorkerService.FrontEndAddWorker(httpClient);
					break;
				case "Add Location":
					await FrontEndAddLocation(httpClient);
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

	private static async Task<bool> CheckApiConnection(HttpClient httpClient)
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
					await Task.Delay(3000); // Wait for 2 seconds before retrying
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


}
