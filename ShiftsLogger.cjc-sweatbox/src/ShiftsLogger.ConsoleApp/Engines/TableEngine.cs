using ShiftsLogger.ConsoleApp.Constants;
using ShiftsLogger.ConsoleApp.Models;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.Engines;

/// <summary>
/// Engine for Spectre.Table generation.
/// </summary>
internal class TableEngine
{
    #region Methods

    internal static Table GetShiftTable(ShiftDto shift)
    {
        var table = new Table
        {
            Expand = true,
        };

        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration (Hours)");

        table.AddRow(
                shift.StartTime.ToString(StringFormat.DateTime),
                shift.EndTime.ToString(StringFormat.DateTime),
                shift.DurationInHours.ToString("F2"));

        return table;
    }

    internal static Table GetShiftsTable(IReadOnlyList<ShiftDto> shifts)
    {
        var table = new Table
        {
            Caption = new TableTitle($"{shifts.Count} shifts found."),
            Expand = true,
        };

        table.AddColumn("ID");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration (Hours)");

        foreach (var x in shifts)
        {
            table.AddRow(
                x.Id.ToString(),
                x.StartTime.ToString(StringFormat.DateTime),
                x.EndTime.ToString(StringFormat.DateTime),
                x.DurationInHours.ToString("F2"));
        }

        return table;
    }

    #endregion
}
