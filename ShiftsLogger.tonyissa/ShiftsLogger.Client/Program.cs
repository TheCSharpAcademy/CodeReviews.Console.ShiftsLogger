using ShiftsLogger.Client.UI;
using Spectre.Console;

static async Task StartProgram()
{
    while (true)
    {
        try
        {
            await ShiftsUI.StartupMenu();
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

await StartProgram();