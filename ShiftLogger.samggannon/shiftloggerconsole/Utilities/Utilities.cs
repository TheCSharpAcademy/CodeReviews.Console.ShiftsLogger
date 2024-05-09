namespace shiftloggerconsole.Utilities;

internal class Utilities
{
    internal static void ConfirmKey()
    {
        ConsoleKeyInfo keyPress;
        do
        {
            keyPress = Console.ReadKey();
            Console.WriteLine(": Please press [enter]");
        } while (keyPress.Key != ConsoleKey.Enter);
    }
}
