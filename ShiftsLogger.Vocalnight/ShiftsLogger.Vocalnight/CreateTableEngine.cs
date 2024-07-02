using ConsoleTableExt;
using System.Diagnostics.CodeAnalysis;

namespace ShiftLoggerConsole;

public class CreateTableEngine
{
    public static void ShowTable<T>( List<T> tableData, [AllowNull] string tableName ) where T : class
    {
        Console.Clear();
        if (tableName == null)
            tableName = "";

        Console.WriteLine("\n\n");

        ConsoleTableBuilder
            .From(tableData)
            .ExportAndWriteLine();
        Console.WriteLine("\n\n");
    }
}
