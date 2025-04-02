using Spectre.Console;

namespace ShiftsLoggerUI.Menus;

internal abstract class BaseMenu
{
    public abstract Task ShowMenuAsync();

    public  void PressAnyKeyToContinue()
    {
        AnsiConsole.MarkupLine("[green]Press Any Key To Continue[/]");
        Console.ReadKey();
        Console.Clear();
    }
}
