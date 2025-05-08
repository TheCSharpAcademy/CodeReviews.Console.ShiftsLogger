using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiftsLogger.Ryanw84.Models;
using Spectre.Console;

namespace FrontEnd.Services;

internal class TableVisualisationEngine
{
    public TableVisualisationEngine() { }

    public void DisplayTable<T>(List<T> items)
    {
        if (items == null || !items.Any())
        {
            AnsiConsole.MarkupLine("[yellow]No data available to display.[/]");
            return;
        }

        var table = new Table();

        // Determine the type of the object and configure the table
        if (typeof(T) == typeof(Worker))
        {
            table.AddColumn("Worker ID");
            table.AddColumn("Name");
            table.AddColumn("Number of Shifts");

            foreach (var item in items.Cast<Worker>())
            {
                table.AddRow(
                    item.WorkerId.ToString(),
                    item.Name,
					item.Shifts?.Count.ToString() ?? "0"
                );
            }
        }
        else if (typeof(T) == typeof(Location))
        {
            table.AddColumn("Location ID");
            table.AddColumn("Name");
            table.AddColumn("Number of Shifts");

            foreach (var item in items.Cast<Location>())
            {
                table.AddRow(
                    item.LocationId.ToString(),
                    item.Name ?? "N/A",
                    item.Shifts?.Count.ToString() ?? "0"
                );
            }
        }
        else if (typeof(T) == typeof(Shift))
        {
            table.AddColumn("Shift ID");
            table.AddColumn("Shift Name");
            table.AddColumn("Worker Name");
            table.AddColumn("Location Name");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");

            foreach (var item in items.Cast<Shift>())
            {
                table.AddRow(
                    item.Id.ToString(),
                    item.Worker?.Name ?? "N/A",
                    item.Location?.Name ?? "N/A",
                    item.StartTime.ToString("yyyy-MM-dd HH:mm"),
                    item.EndTime.ToString("yyyy-MM-dd HH:mm")
                );
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Unsupported type provided.[/]");
            return;
        }

        // Render the table
        AnsiConsole.Write(table);
    }
}
