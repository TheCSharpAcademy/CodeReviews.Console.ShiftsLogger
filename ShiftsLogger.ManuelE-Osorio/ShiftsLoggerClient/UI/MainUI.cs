namespace ShiftsLoggerClient.UI;

public class MainUI
{
    public static void DisplayUserMenu()
    {
        Console.Clear();
        Console.WriteLine("1) Start a new shift\n"+
        "2) Finish the current shift\n"+
        "3) Display your last 5 shifts\n");
    }


    public static void DisplayAdminMenu()
    {
        Console.Clear();
        Console.WriteLine("1) Start a new shift\n"+
        "2) Finish the current shift\n"+
        "3) Display your last 5 shifts\n"+
        "4) Search employees by name\n"+
        "5) Find employee by id\n"+
        "6) Add new employee\n"+
        "7) Modify a employee\n");
    }
    public static void DisplayLoginMenu(string? errorMessage)
    {
        Console.Clear();
        if(errorMessage != null)
            Console.WriteLine($"Error: {errorMessage}.");
        Console.WriteLine("Please enter you employee ID or \"C\" to exit:\n");
    }

    public static void DisplayUIMessage(string? message)
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

    public static void EnterEmployeeID(string? errorMessage)
    {
        Console.Clear();
        if(errorMessage != null)    
            Console.WriteLine($"Error: {errorMessage}.");
        Console.WriteLine("Please enter the employee ID or \"c\" to cancel:");
    }

    public static void EnterEmployeeName(string? errorMessage)
    {
        Console.Clear();
        if(errorMessage != null)    
            Console.WriteLine($"Error: {errorMessage}.");
        Console.WriteLine("Please enter the new employee name (within 100 characters):");
    }

    public static void IsEmployeeAdmin()
    {
        Console.Clear();
        Console.WriteLine("Is the new employee admin? [y/N]\n");
    }
}