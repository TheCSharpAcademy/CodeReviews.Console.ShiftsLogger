using ConsoleFrontEnd.Controller;
using ConsoleFrontEnd.MenuSystem;
using Spectre.Console;

namespace ConsoleFrontEnd.UserInterface;

public class WorkerMenu
{
    public static async Task DisplayWorkerMenu() // Changed from 'void' to 'Task' to properly handle async
    {
        Console.Clear();
        var workerController = new WorkerController(); // Added an instance of WorkerController
        while (true)
        {
            AnsiConsole.Write(
                new Rule("[bold yellow]Worker Menu[/]").RuleStyle("yellow").Centered()
            );

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select an option:[/]")
                    .AddChoices(
                        "Create Worker",
                        "View Workers",
                        "View Worker By ID",
                        "Update Worker",
                        "Delete Worker",
                        "Back to Main Menu"
                    )
            );
            switch (choice)
            {
                case "Create Worker":
                    await workerController.CreateWorker(); // Use the instance of WorkerController
                    break;
                case "View Workers":
                    await workerController.GetAllWorkers();
                    break;
                case "View Worker By ID":
					await workerController.GetWorkerById();
                    break;
                case "Update Worker":
                    await workerController.UpdateWorker();
                    break;
                case "Delete Worker":
                    await workerController.DeleteWorker();
                    break;
                case "Back to Main Menu":
                    await MainMenu.DisplayMainMenu();
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid choice, please try again.[/]");
                    break;
            }
        }
    }
}
