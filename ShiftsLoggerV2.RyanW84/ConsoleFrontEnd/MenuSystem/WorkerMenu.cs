using ConsoleFrontEnd.Controller;
using Spectre.Console;

namespace ConsoleFrontEnd.UserInterface;

internal static class WorkerMenu
{
    public static void DisplayWorkerMenu()
    {
        var workerController = new WorkerController();

        AnsiConsole.Clear();
        while (true)
        {
            AnsiConsole.Write(
                new Rule("[bold yellow]Worker Menu[/]").RuleStyle("yellow").Centered()
            );
            AnsiConsole.WriteLine("Please select an option from the menu below:");
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select an option:[/]")
                    .AddChoices(
                        "Create Worker",
                        "View Workers",
                        "Edit Worker",
                        "Delete Worker",
                        "Back to Main Menu"
                    )
            );
            switch (choice)
            {
                case "Create Worker":
                    workerController.CreateWorker();
                    break;
                case "View Workers":
                    workerController.GetAllWorkers();
                    break;
                case "Edit Worker":
                    workerController.UpdateWorker();
                    break;
                case "Delete Worker":
                    workerController.DeleteWorker();
                    break;
                case "Back to Main Menu":
                    return;
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid choice, please try again.[/]");
                    break;
            }
        }
    }
}
