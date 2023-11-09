using ShiftLoggerConsole.Controllers;
using Spectre.Console;

namespace ShiftLoggerConsole.UI.Menu;

public class Menu
{
    internal static async Task MainMenu()
    {
        ShiftController shiftController = new();

        AnsiConsole.Status()
            .Start("Loading...", ctx =>
            {
                ctx.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(500);
            });

        bool AppIsRunningYet = true;
        while (AppIsRunningYet)
        {
            Clear();
            AnsiConsole.Write(new FigletText("Shift-Logger").LeftJustified().Color(Color.Blue));
            var options = new SelectionPrompt<MainMenuOptions>();

            options.AddChoices
                (
                    MainMenuOptions.Check_In_Out,
                    MainMenuOptions.Manage_Workers,
                    MainMenuOptions.Manage_Shifts,
                    MainMenuOptions.Quit
                );

            var r = AnsiConsole.Prompt(options);

            switch (r)
            {
                case MainMenuOptions.Manage_Shifts:
                    Clear();
                    await ManageShifts(shiftController);
                    break;

                case MainMenuOptions.Manage_Workers:
                    Clear();
                    await ManageWorker();
                    break;

                case MainMenuOptions.Check_In_Out:
                    await shiftController.Add();
                    Clear();
                    break; 
                case MainMenuOptions.Quit:
                    AppIsRunningYet = false;
                    break;
            }
        }

    }

    internal static async Task ManageShifts(ShiftController shiftController)
    {
        bool AppIsRunningYet = true;
        while (AppIsRunningYet)
        {
            Clear();
            AnsiConsole.Write(new FigletText("Shifts").LeftJustified().Color(Color.Blue));
            var options = new SelectionPrompt<ManageShiftOptions>();
            options.AddChoices
                (
                    ManageShiftOptions.All_Shifts,
                    ManageShiftOptions.Shifts_By_Worker,
                    ManageShiftOptions.Back
                );

            var result = AnsiConsole.Prompt(options);
            switch (result)
            {
                case ManageShiftOptions.All_Shifts:
                    await shiftController.GetAsync();
                    Clear();
                    break;

                case ManageShiftOptions.Shifts_By_Worker:
                    WriteLine("nothing yet");
                    Clear();
                    break;

                case ManageShiftOptions.Back:
                    AppIsRunningYet = false;
                    break;
            }
        }
    }

    internal static async Task ManageWorker()
    {
        WorkerController workerController = new();
        bool AppIsRunningYet = true;
        while (AppIsRunningYet)
        {
            AnsiConsole.Write(new FigletText("Workers").LeftJustified().Color(Color.Blue));
            var options = new SelectionPrompt<ManageWorkerOptions>();
            options.AddChoices
                (
                    ManageWorkerOptions.GetAllWorkers,
                    ManageWorkerOptions.GetWorkerById,
                    ManageWorkerOptions.AddWorker,
                    ManageWorkerOptions.UpdateWorker,
                    ManageWorkerOptions.DeleteWorker,
                    ManageWorkerOptions.Back
                );

            var result = AnsiConsole.Prompt(options);
            switch (result)
            {
                case ManageWorkerOptions.GetAllWorkers:
                    await workerController.Get();
                    Clear();
                    break;
                case ManageWorkerOptions.GetWorkerById:
                    await workerController.GetById();
                    Clear();
                    break;
                case ManageWorkerOptions.AddWorker:
                    await workerController.Add();
                    Clear();
                    break;
                case ManageWorkerOptions.UpdateWorker:
                    await workerController.Update();
                    Clear();
                    break;
                case ManageWorkerOptions.DeleteWorker:
                    await workerController.Delete();
                    Clear();
                    break;
                case ManageWorkerOptions.Back:
                    AppIsRunningYet=false;
                    break; 
            }
        }
    }

}

public enum MainMenuOptions
{
	Manage_Shifts,
    Check_In_Out,
    Manage_Workers,
	Quit,
}


public enum ManageShiftOptions
{
	All_Shifts,
	Shifts_By_Worker,
    Back
}

public enum ManageWorkerOptions
{
	GetAllWorkers,
	GetWorkerById,
    AddWorker,
	UpdateWorker,
	DeleteWorker,
    Back
}
