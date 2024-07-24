using Spectre.Console;

namespace WorkerShiftsUI.Views
{
    public class MainMenu
    {
        private readonly IWorkersView? _workerView;

        public MainMenu(IWorkersView workerView)
        {
            _workerView = workerView;
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
                        await ShiftsView.ShiftsMenu();
                        break;
                    case "Exit":
                        appIsRunning = false;
                        AnsiConsole.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}