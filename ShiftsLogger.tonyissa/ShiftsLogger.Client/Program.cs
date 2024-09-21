using ShiftsLogger.UI.Display;
using Spectre.Console;

static void StartProgram()
{
    while (true)
    {
        try
        {
            UserInterfaceHelper.StartupMenu();
            break;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            AnsiConsole.MarkupLine("\nUnhandled error thrown. Press any key to continue...");
            Console.ReadKey();
            continue;
        }
    }
}

StartProgram();