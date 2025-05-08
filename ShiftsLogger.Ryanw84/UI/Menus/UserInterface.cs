using FrontEnd.Controllers;
using FrontEnd.Services;
using ShiftsLogger.Ryanw84.Models;
using ShiftsLogger.Ryanw84.Services;

using Spectre.Console;

namespace FrontEnd.Menus;

public static class UserInterface
{
    public static void MainMenu(IShiftService shiftService)
    {
        bool isRunning = true;

        while (isRunning)
        {
            // Display the menu
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]What would you like to do?[/]")
                    .AddChoices("Add Shift", "View Shifts", "View All Workers", "Exit")
            );

            switch (choice)
            {
                case "View All Workers":
                    WorkerController workerController = new WorkerController(shiftService);
                    TableVisualisationEngine tableVisualisationEngine =
                        new TableVisualisationEngine();
                    tableVisualisationEngine.DisplayTable<Worker>(
						(List<Worker>)workerController.GetAllWorkers().Result
					);

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
            .BorderStyle(new Style(Color.Gold1, Color.Black));
    }
}
