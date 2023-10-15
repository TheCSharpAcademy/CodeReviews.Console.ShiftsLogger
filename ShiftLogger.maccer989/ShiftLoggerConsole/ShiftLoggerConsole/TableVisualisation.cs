using ConsoleTableExt;
using System.Diagnostics.CodeAnalysis;

namespace ShiftLogger
{
    public class TableVisualisation
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T : class
        {
            Console.Clear();

            if (tableName == null)
                tableName = "";

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle("Shift Logger")
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}
