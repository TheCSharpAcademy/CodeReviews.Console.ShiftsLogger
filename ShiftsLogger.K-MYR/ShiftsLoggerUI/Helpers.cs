using Spectre.Console;

namespace ShiftsLoggerUI
{
    internal class Helpers
    {
        internal static void WriteAndWait(string message)
        {
            AnsiConsole.Write(new Panel($"{message}")
                                .BorderColor(Color.DarkOrange3_1));
            Console.ReadKey();
        }
    }
}
