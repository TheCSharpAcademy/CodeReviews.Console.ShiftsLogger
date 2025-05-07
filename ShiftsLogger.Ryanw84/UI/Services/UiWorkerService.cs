using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using FrontEnd.Controllers;

using Newtonsoft.Json;
using ShiftsLogger.Ryanw84.Models;

using Spectre.Console;

namespace FrontEnd.Services;
internal class UiWorkerService
	{
	private readonly HttpClient _httpClient;

	// Constructor to initialize the HttpClient
	public UiWorkerService(HttpClient httpClient)
		{
		_httpClient = httpClient;
		}
	public async Task<Worker?> GetWorkersInput( )
		{
		try
			{
			var response = await _httpClient.GetAsync("worker");
			var workers = JsonConvert.DeserializeObject<List<Worker>>(await response.Content.ReadAsStringAsync());

			if(workers == null || !workers.Any())
				{
				AnsiConsole.MarkupLine("[yellow]No workers found.[/]");
				return null;
				}

			var workerArray = workers.Select(w => w.WorkerId).ToArray();
			var option = AnsiConsole.Prompt(
				new SelectionPrompt<int>()
					.Title("Select a worker:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more workers)[/]")
					.AddChoices(workerArray)
			);

			var selectedWorker = workers.FirstOrDefault(w => w.WorkerId == option);

			if(selectedWorker == null)
				{
				AnsiConsole.MarkupLine("[red]Selected worker not found.[/]");
				return null;
				}

			return selectedWorker;
			}
		catch(Exception ex)
			{
			AnsiConsole.MarkupLine($"[red]Error fetching workers: {ex.Message}[/]");
			return null;
			}
		}

	public async Task<List<Worker>> GetAllWorkers( )
		{
		var workers = WorkerController.GetAllWorkers(); 
		return workers;
		}
	}
