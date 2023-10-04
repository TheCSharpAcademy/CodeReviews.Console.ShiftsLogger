namespace ShiftsLoggerConsole.UI
{
    public class Helpers
    {
        internal static Style HighLightStyle => new(
            Color.LightSlateBlue,
            Color.Black,
            Decoration.None);

        internal static void RenderTitle(string message)
        {
            AnsiConsole.Write(new Rule($"[lightslateblue]{message}[/]"));
        }
        internal static void RenderMessage(string message)
        {
            AnsiConsole.WriteLine($"{message}");
        }
    }
}