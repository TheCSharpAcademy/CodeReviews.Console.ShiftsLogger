using ShiftsLogger.Client.Models;
using ShiftsLogger.Client.Services;
using Spectre.Console;

namespace ShiftsLogger.Client.UI;

public static class ShiftsUI
{
    public static List<string> MainMenuOptions = [
        "View shifts",
        "Create shifts",
        "Delete shifts",
        "Update shifts",
        "Quit"
    ];

    public static async Task StartupMenu()
    {
        while (true)
        {
            Console.Clear();
            var result = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What do you want to do?")
                .AddChoices(MainMenuOptions)
            );

            switch (result)
            {
                case "View shifts":
                    await ShiftsService.ListShiftsAsync();
                    break;
                case "Create shifts":
                    break;
                case "Delete shifts":
                    break;
                case "Update shifts":
                    break;
                default:
                    return;
            }
        }
    }

    public static void ListEntries(List<ShiftsEntry> entries)
    {
        var table = new Table();
        table.AddColumns("ID", "Shift Start", "Shift End", "Total time");

        foreach (var entry in entries)
        {
            table.AddRow(
                entry.Id.ToString(),
                entry.Start.ToString("f"),
                entry.End.ToString("f"),
                (entry.End - entry.Start).ToString(@"h\:mm")
            );
        }

        AnsiConsole.Write(table);
    }
}