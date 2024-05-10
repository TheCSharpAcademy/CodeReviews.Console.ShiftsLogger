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

    internal static void InformUser(string infoMessage)
    {
        Console.Clear();
        Console.WriteLine(infoMessage);
        Console.WriteLine("\nPress [enter] to go back");

        ConfirmKey();
    }
}
