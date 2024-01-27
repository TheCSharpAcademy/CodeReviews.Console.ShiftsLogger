namespace ShiftsLoggerClient.UI;

public class MainUI
{
    public static void DisplayUserMenu(string? name)
    {
        Console.Clear();
        Console.WriteLine($"Hello {name}. Press one of the following options:\n"+
        "1) Start a new shift\n"+
        "2) Finish the current shift\n"+
        "3) Display your last 5 shifts\n"+
        "Press ESC to exit the app.\n");
    }

    public static void DisplayAdminMenu(string? name)
    {
        Console.Clear();
        Console.WriteLine($"Hello {name}. Press one of the following options:\n"+
        "1) Start a new shift\n"+
        "2) Finish the current shift\n"+
        "3) Display your last 5 shifts\n"+
        "4) Search employees by name\n"+
        "5) Find employee by id\n"+
        "6) Add new employee\n"+
        "7) Modify a employee\n"+
        "Press ESC to exit the app.\n");
    }

    public static void DisplayLoginMenu(string? errorMessage)
    {
        Console.Clear();
        if(errorMessage != null)
            Console.WriteLine($"Error: {errorMessage}.");
        Console.WriteLine("Please enter your employee ID or press ESC to exit:\n");
    }

    public static void DisplayUIMessage(string? message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.WriteLine("Press any key to continue.");
    }

    public static void DisplayList<T>(List<T>? table) where T: class
    {
        Console.Clear();
        TableUI.PrintTable(table);
        Console.WriteLine("Press any key to continue.");
    }

    public static void EnterEmployeeID(string? errorMessage, string modifier)
    {
        Console.Clear();
        if(errorMessage != null)    
            Console.WriteLine($"Error: {errorMessage}.");
        Console.WriteLine($"Please enter the employee ID to {modifier} or press ESC to cancel:");
    }

    public static void EnterEmployeeName(string? errorMessage, string modifier)
    {
        Console.Clear();
        if(errorMessage != null)    
            Console.WriteLine($"Error: {errorMessage}.");
        Console.WriteLine($"Please enter the employee name to {modifier} (within 100 characters) or press ESC to cancel:");
    }

    public static void IsEmployeeAdmin(string modifier)
    {
        Console.Clear();
        Console.WriteLine($"Is the employee to {modifier} admin? [y/N]\n");
    }

    public static void LoadingMessage()
    {
        Console.Clear();
        Console.WriteLine("Loading ...");
    }

    public static void WelcomeMessage()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Shifts Logger app!");
        Thread.Sleep(3000);
    }

    public static void ExitMessage()
    {
        Console.Clear();
        Console.WriteLine("Thank you for using the Shifts Logger app!");
        Thread.Sleep(3000);
    }

    public static void FinishShiftConfirmation()
    {
        Console.Clear();
        Console.WriteLine("Do you want to finish your current shift? [y/N]\n");
    }
}