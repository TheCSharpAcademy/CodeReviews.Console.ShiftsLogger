using Spectre.Console;

namespace ShiftsLoggerUI.Utilities;

public static class Util
{
    internal static void AskUserToContinue()
    {
        AnsiConsole.MarkupLine("Press and key to continue...");
        Console.ReadKey();
    }

    internal static bool ReturnToMenu()
    {
        AnsiConsole.MarkupLine($"Type [green]'0'[/] to return to menu or any other key to continue:");
        string input = Console.ReadLine();
        return input == "0";
    }
}