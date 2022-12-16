namespace ShiftLoggerConsole.Controller;

public class Menus
{
    public void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("MAIN MENU");
        Console.WriteLine("---------------------------\n");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("a to View All Shifts");
        Console.WriteLine("v to View a Single Shift");
        Console.WriteLine("s to Start a Shift");
        Console.WriteLine("e to End a Shift End Time");
        Console.WriteLine("d to Delete a Shift");
        Console.WriteLine("c to Close Application\n");
        Console.WriteLine("Enter choice and hit Enter");
        Console.Write("Your choice? ");
    }
}