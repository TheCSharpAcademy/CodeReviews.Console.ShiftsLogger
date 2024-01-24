using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient.UI;

public class MainUI
{
    public static void DisplayUserMenu()
    {
        Console.Clear();
        Console.WriteLine("1) Start a new shift\n"+
        "2) Finish the current shift\n"+
        "3) Display the last 5 shifts\n");
    }

    public static void DisplayLoginMenu(string? errorMessage)
    {
        Console.Clear();
        if(errorMessage != null)
            Console.WriteLine($"Error: {errorMessage}.");
        Console.WriteLine("Please enter you employee ID or \"C\" to exit:\n");
    }

    public static void DisplayUIMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }

    public static void DisplayList<T>(List<T>? table) where T: class
    {
        Console.Clear();
        TableUI.PrintTable(table);
        Console.WriteLine("Press any key to continue");
    }
}