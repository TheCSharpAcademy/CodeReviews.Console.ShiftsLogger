using Spectre.Console;
using ShiftLogger.ShiftTrack.Models;

namespace ShiftLogger.ShiftTrack.Views;

internal static class Display
{
    public static string GetSelection(string title, string[] choices)
    {
        var selectedCategory = AnsiConsole.Prompt(new SelectionPrompt<string>().Title(title).AddChoices(choices).HighlightStyle(new Style(foreground: Color.Blue)));
        return selectedCategory;
    }

    public static void DisplayWorkers(List<Worker> workers, string[] columns, string title = "Worker List")
    {
        var table = new Table();
        table.Title = new TableTitle(title);
        foreach (var column in columns)
        {
            table.AddColumn(column);
        }
        foreach (var worker in workers)
        {
            table.AddRow(worker.WorkerId.ToString(), worker.Name, worker.EmailId);
        }
        AnsiConsole.Write(table);
    }
}