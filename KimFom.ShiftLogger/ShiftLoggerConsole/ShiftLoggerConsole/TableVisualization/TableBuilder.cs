using ConsoleTableExt;
using ShiftLoggerConsole.Models;

namespace ShiftLoggerConsole.TableVisualization;

public class TableBuilder : ITableBuilder
{
    public void DisplayTable(List<Shift>? shifts)
    {
        ConsoleTableBuilder
            .From(shifts)
            .WithTitle("Shifts")
            .ExportAndWriteLine();
    }
}