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

    internal static void InformUser(bool isSuccessStatusCode, string infoMessage)
    {
        Console.Clear();
        if (isSuccessStatusCode)
        {
            Console.WriteLine(infoMessage);
        }
        else
        {
            Console.WriteLine(infoMessage);
        }

        Console.WriteLine("Press [enter] to go back");

        ConfirmKey();
    }
}
