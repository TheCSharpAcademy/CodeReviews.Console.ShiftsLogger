using Spectre.Console;

namespace ShiftLoggerUi;

internal class Utilities
{
    public static void DisplayMessage(string message, string color = "yellow")
    {
        AnsiConsole.MarkupLine($"[{color}]{message}[/]");
    }

    public static void ShowTable<T>(List<T> items, string[] columnNames, Func<T, string[]> rowSelector)
    {
        var table = new Table();
        foreach (var columnName in columnNames)
        {
            table.AddColumn(columnName);
        }

        foreach (var item in items)
        {
            table.AddRow(rowSelector(item));
        }

        AnsiConsole.Write(table);

        Utilities.DisplayMessage("Press Any Key to Return to Menu");
        Console.ReadKey();
        Console.Clear();
    }
}
