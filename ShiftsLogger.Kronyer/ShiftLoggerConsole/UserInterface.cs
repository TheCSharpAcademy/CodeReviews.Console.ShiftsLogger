using Spectre.Console;
using static ShiftLoggerConsole.Enums;

namespace ShiftLoggerConsole;

internal class UserInterface
{
    public static void RunMenu()
    {
        var option = AnsiConsole.Prompt(new SelectionPrompt<Options>().Title("Select an option:").AddChoices(Options.RegisterNewWorker, Options.StartShift, Options.EndShift, Options.ViewAllWorkers, Options.ViewWorker));

        switch (option)
        {
            case Options.RegisterNewWorker:
                DataAccess.RegisterNewWorker();
                break;
            case Options.StartShift:
                DataAccess.StartShift();
                break;
            case Options.EndShift:
                DataAccess.EndShift();
                break;
            case Options.ViewWorker:
                DataAccess.ViewWorker();
                break;
            case Options.ViewAllWorkers:
                DataAccess.ViewAllWorkers();
                break;
        }
    }
}
