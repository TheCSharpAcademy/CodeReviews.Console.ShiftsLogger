using Spectre.Console;

namespace WorkerShiftsUI.Views
{
    public class MainMenu
    {
        private readonly IWorkersView? _workerView;
        private readonly IShiftView? _shiftView;

        public MainMenu(IWorkersView workerView, IShiftView shiftView)
        {
            _workerView = workerView;
            _shiftView = shiftView;
        }

        public async Task ShowMainMenu()
        {
            var appIsRunning = true;
            while (appIsRunning)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Worker Shift Logger Main Menu")
                    .AddChoices(
                        "Manage Workers",
                        "Manage Shifts",
                        "Exit")
                    .UseConverter(x => x.ToString())
                );

                switch (choice)
                {
                    case "Manage Workers":
                        await _workerView!.WorkersMenu();
                        break;
                    case "Manage Shifts":
                        await _shiftView!.ShiftsMenu();
                        break;
                    case "Exit":
                        appIsRunning = false;
                        Console.Clear();
                        AnsiConsole.MarkupLine("[bold][red]Exiting...[/][/]");
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}