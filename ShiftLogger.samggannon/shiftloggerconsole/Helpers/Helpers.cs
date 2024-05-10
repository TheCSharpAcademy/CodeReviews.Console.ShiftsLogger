namespace shiftloggerconsole.Helpers;

internal class Helpers
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

    internal static void DevelopersNote()
    {
        Console.Clear();
        Console.WriteLine("Disclaimer:");
        Console.WriteLine("This application is not intended to be a robust time shift management application.");
        Console.WriteLine("It is a streamlined and simplified console project designed to demonstrate knowledge on building controller-based .NET Web APIs");
        Console.WriteLine("and interacting with said API with a front-end console project.");
        Console.WriteLine("\nPress [enter] to go back to the main menu");

        ConfirmKey();
    }

    internal static void InformUser(string infoMessage)
    {
        Console.Clear();
        Console.WriteLine(infoMessage);
        Console.WriteLine("\nPress [enter] to go back");

        ConfirmKey();
    }
}
