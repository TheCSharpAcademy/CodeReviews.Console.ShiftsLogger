using Spectre.Console;
using ShiftLogger.Mefdev.ShiftLoggerUI.Models;
using ShiftLogger.Mefdev.ShiftLoggerUI.Controllers;

namespace ShiftLogger.Mefdev.ShiftLoggerUI
{
    public class UserInterface : WorkerShiftBase
    {
        private readonly WorkerShiftController _workerShiftController;

        public UserInterface(WorkerShiftController workerShiftController)
        {
            _workerShiftController = workerShiftController;
        }

        public async Task MainMenu()
        {
            while (true)
            {
                    AnsiConsole.Write(new FigletText("Shift Logger").Color(Color.DodgerBlue1).Centered());

                    AnsiConsole.Clear();

                    var mainChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<Options.UserOptions>()
                            .Title("Choose an option:")
                            .AddChoices(Enum.GetValues(typeof(Options.UserOptions)).Cast<Options.UserOptions>())
                    );

                    switch (mainChoice)
                    {
                        case Options.UserOptions.Quit:
                            DisplayMessage("Exiting the app...", "red");
                            Environment.Exit(0);
                            break;
                        case Options.UserOptions.CreateShift:
                            await _workerShiftController.CreateShift();
                            break;
                        case Options.UserOptions.UpdateShift:
                            await _workerShiftController.UpdateShift();
                            break;
                        case Options.UserOptions.DeleteShift:
                            await _workerShiftController.DeleteShift();
                            break;
                        case Options.UserOptions.ViewShift:
                            await _workerShiftController.GetShift();
                            break;
                        case Options.UserOptions.ViewShifts:
                            await _workerShiftController.GetShifts();
                            break;

                        default:
                            DisplayMessage("Invalid choice. Please select a valid option.", "red");
                            break;
                    }
            }
        }
    }
}