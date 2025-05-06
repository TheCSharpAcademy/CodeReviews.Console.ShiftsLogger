using FrontEnd.Controllers;

using ShiftsLogger.Ryanw84.Models;

using Spectre.Console;

namespace FrontEnd.Menus;

public static class UserInterface
	{
	public static void MainMenu( )
		{
		bool isRunning = true;

		while(isRunning)
			{
			// Display the menu
			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]What would you like to do?[/]")
					.AddChoices("Add Shift" , "View Shifts" , "Exit")
			);

			// Handle the user's choice
			switch(choice)
				{
				//case "Add Shift":
				//UiShiftService.GetAllShifts();
				//break;
				case "View Worker":
					WorkerController.GetAllWorkers();
					break;
				case "Exit":
					isRunning = false;
					AnsiConsole.MarkupLine("[green]Exiting the application. Goodbye![/]");
					break;
				}
			}
		}

	public static void ShowWorker(Worker worker)
		{
		Panel panel = new Panel(
			$"[green]Worker ID:[/] {worker.WorkerId} [green]Name:[/] {worker.Name}"
		)
			.Border(BoxBorder.Rounded)
			.Header("[bold yellow]Worker Details[/]")
			.HeaderAlignment(Justify.Left)
			.BorderStyle(new Style(Color.Gold1 , Color.Black));
		}
	}
