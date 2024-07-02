using ConsoleTableExt;

namespace ShiftTrackerUI
{
    internal class TableVisualisation
    {
        public static void ShowTable<T>(List<T> tableData) where T : class
        {            
            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn("ID", "Name", "Start Date", "Start Time", "End Time", "Duration")
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine(TableAligntment.Center);
        }
    }
}
