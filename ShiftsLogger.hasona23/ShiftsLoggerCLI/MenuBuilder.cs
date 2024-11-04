namespace ShiftsLoggerCLI;

using ShiftsLoggerCLI.Enums;
using Spectre.Console;
internal static class MenuBuilder
{
    public static Option GetOption()
    {
        return AnsiConsole.Prompt(new SelectionPrompt<Option>()
            .Title("[yellow]--MainMenu--[/]")
            .AddChoices(Enum.GetValues<Option>())
            .HighlightStyle(new Style(Color.DarkTurquoise)));
    }
    public static Crud DisplayShiftsWorkerOptions(bool workers)
    {
        var operations = Enum.GetValues<Crud>().ToList();
        return AnsiConsole.Prompt(new SelectionPrompt<Crud>()
            .Title($"[yellow]--{(workers ? "Workers" : "Shifts")}--[/]")
            .AddChoices(operations)
            .HighlightStyle(new Style(Color.DarkTurquoise)));
        
    }
    public static void DisplayHelpMenu()
    {
        AnsiConsole.MarkupLine("[yellow]--HelpMenu--[/]");
        AnsiConsole.MarkupLine("[white]Workers[/] -> [darkturquoise]Add, Delete, Edit , Get Workers[/]");
        AnsiConsole.MarkupLine("[white]Shifts [/] -> [darkturquoise]Add, Delete, Edit , Get Shifts [/]");
        AnsiConsole.MarkupLine("[white]Help   [/] -> [darkturquoise]Get Help[/]");
        AnsiConsole.MarkupLine("[white]Exit   [/] -> [darkturquoise]Clean Close of app[/]");
        Console.WriteLine();
        EnterButtonPause();
    }
    public static void EnterButtonPause()
    {
        AnsiConsole.MarkupLine("[White]press enter to continue...[/]");
        Console.ReadLine();
    }
}

