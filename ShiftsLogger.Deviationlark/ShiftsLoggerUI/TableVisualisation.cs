using Spectre.Console;

namespace ShiftsLoggerUI
{
    public class TableVisualisation
    {
        public static void ShowShifts(List<ShiftModel> shifts)
        {
            var table = new Table();

            table.Title("Shifts");
            table.AddColumns("Id", "Date", "StartTime", "EndTime", "Duration");

            foreach (var shift in shifts)
            {
                table.AddRow($"{shift.id}", $"{shift.date.ToString("dd-MM-yyyy")}", $"{shift.startTime.ToString("HH:mm:ss")}", $"{shift.endTime.ToString("HH:mm:ss")}", $"{shift.duration}");
            }
            AnsiConsole.Write(table);
        }
    }
}