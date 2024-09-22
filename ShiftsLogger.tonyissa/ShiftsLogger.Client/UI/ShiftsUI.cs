﻿using ShiftsLogger.Client.Models;
using ShiftsLogger.Client.Services;
using Spectre.Console;

namespace ShiftsLogger.Client.UI;

public static class ShiftsUI
{
    public static List<string> MainMenuOptions = [
        "View shifts",
        "Create shifts",
        "Update shifts",
        "Delete shifts",
        "Quit"
    ];

    public static async Task LaunchStartMenu()
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
                    await ShiftsService.SubmitShiftAsync();
                    break;
                case "Update shifts":
                    await ShiftsService.ChangeShiftAsync();
                    break;
                case "Delete shifts":
                    await ShiftsService.RemoveShiftAsync();
                    break;
                default:
                    return;
            }
        }
    }

    public static void ListEntries(List<ShiftsEntry> entries)
    {
        var table = new Table();
        table.AddColumns("ID", "Shift Start", "Shift End", "Total hours");

        foreach (var entry in entries)
        {
            table.AddRow(
                entry.Id.ToString(),
                entry.Start.ToString("f"),
                entry.End.ToString("f"),
                (entry.End - entry.Start).TotalHours.ToString("f")
            );
        }

        AnsiConsole.Write(table);
    }

    public static ShiftsEntry GetShiftInformation()
    {
        var start = AnsiConsole.Ask<DateTime>("Enter a start date");
        var end = AnsiConsole.Ask<DateTime>("Enter an end date");
        
        if (start >= end)
        {
            AnsiConsole.MarkupLine("[red]Start date must be before the end date[/]");
            return GetShiftInformation();
        }

        return new ShiftsEntry { Start = start, End = end };
    }

    public static int GetShiftId(List<ShiftsEntry> entries)
    {
        var id = AnsiConsole.Ask<int>("Select the ID of the entry");

        if (!entries.Exists(s => s.Id == id))
        {
            AnsiConsole.MarkupLine("[red]Invalid selection[/]");
            return GetShiftId(entries);
        }

        return id;
    }

    public static bool ConfirmSelection()
    {
        return AnsiConsole.Confirm("Are you sure?");
    }
}