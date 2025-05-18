using Azure;

using Microsoft.AspNetCore.Mvc;

using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;

using Spectre.Console;

using System.Net.Http.Json;

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
				var response = await httpClient.GetAsync("api/Shift");

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
		var name = AnsiConsole.Ask<string>("Enter [green]Worker Name[/]:");
		var phone = AnsiConsole.Ask<string>("Enter [green]Phone[/]:");
		var email = AnsiConsole.Ask<string>("Enter [green]Email[/]:");

		var response = await httpClient.PostAsJsonAsync(
			"api/workers" ,
			new
			{
				Name = name ,
				Phone = phone ,
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

	static async Task AddLocation(HttpClient httpClient)
	{
		var name = AnsiConsole.Ask<string>("Enter [green]Location Name[/]:");
		var address = AnsiConsole.Ask<string>("Enter [green]Address[/]:");
		var city = AnsiConsole.Ask<string>("Enter [green]City[/]:");
		var state = AnsiConsole.Ask<string>("Enter [green]State[/]:");
		var zip = AnsiConsole.Ask<string>("Enter [green]Zip Code[/]:");
		var country = AnsiConsole.Ask<string>("Enter [green]Country[/]:");

		var response = await httpClient.PostAsJsonAsync(
			"api/shifts" ,
			new
			{
				Name = name ,
				Address = address ,
				City = city ,
				State = state ,
				Zip = zip ,
				Country = country ,
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
			"api/Shift" ,
			new
			{
				WorkerId = workerId ,
				LocationId = locationId ,
				StartTime = startTime ,
				EndTime = endTime ,
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
		var response = await httpClient.GetFromJsonAsync<List<dynamic>>("api/Workers");
		if (response != null)
		{
			var table = new Table()
				.AddColumn("ID")
				.AddColumn("Name")
				.AddColumn("Phone")
				.AddColumn("Email");
			foreach (var worker in response.Data)
			{
				table.AddRow(
					worker.WorkerId.ToString() ,
					worker.Name ,
					worker.Phone ,
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

	static async Task<ActionResult<Locations>> ViewLocations(HttpClient httpClient)
	{
		try
		{
			var response = await httpClient.GetFromJsonAsync Task<ActionResult<Locations<ApiResponseDto>>>(
				"api/Locations"
			);
			if (response != null && response.Data != null && !response.RequestFailed)
			{
				var table = new Table()
		.AddColumn("ID")
		.AddColumn("Name")
		.AddColumn("Address")
		.AddColumn("City")
		.AddColumn("State")
		.AddColumn("Zip")
		.AddColumn("Country");
				foreach (var location in response.Data)
				{
					table.AddRow(
						((int)location.LocationId).ToString() ,
						(string)location.Address ,
						(string)location.City ,
						(string)location.State ,
						(string)location.Zip ,
						(string)location.Country
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

		return null;
	}

	public class ShiftDto
	{
		public int ShiftId { get; set; }
		public int WorkerId { get; set; }
		public int LocationId { get; set; }
		public DateTimeOffset StartTime { get; set; }
		public DateTimeOffset EndTime { get; set; }
	}

	static async Task ViewShifts(HttpClient httpClient)
	{
		try
		{
			var response = await httpClient.GetFromJsonAsync<ApiResponseDto<List<ShiftDto>>>(
				"api/Shift"
			);
			if (response != null && response.Data != null && !response.RequestFailed)
			{
				var table = new Table()
					.AddColumn("ID")
					.AddColumn("Worker ID")
					.AddColumn("Location ID")
					.AddColumn("Start Time")
					.AddColumn("End Time");

				foreach (var shift in response.Data)
				{
					table.AddRow(
						shift.ShiftId.ToString() ,
						shift.WorkerId.ToString() ,
						shift.LocationId.ToString() ,
						shift.StartTime.ToString("yyyy-MM-DDTHH:mm:ss (en-GB)") ,
						shift.EndTime.ToString("u")
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
