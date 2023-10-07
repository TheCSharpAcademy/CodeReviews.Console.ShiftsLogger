using ShiftLogger;
ShiftService.CheckApiIsConnected();
GetMainMenu();
static void GetMainMenu()
{
    Console.Clear();
    bool closeApp = false;
    while (closeApp == false)
    {
        Console.WriteLine("\nMain Menu");
        Console.WriteLine("---------\n");
        Console.WriteLine("What would you like to do?\n");
        Console.WriteLine("Type 1 to View all Shifts");
        Console.WriteLine("Type 2 to Insert a Shift");
        Console.WriteLine("Type 3 to Delete a Shift");
        Console.WriteLine("Type 4 to Update a Shift\n");
        Console.WriteLine("Type 0 to Close Application");
        Console.WriteLine("---------------------------\n");

        string command = Console.ReadLine();

        switch (command)
        {
            case "0":
                Console.WriteLine("\nGoodbye");
                closeApp = true;
                Environment.Exit(0);
                break;
            case "1":
                ShiftService.GetAllShifts();
                break;
            case "2":
                ShiftService.InsertAShift();
                break;
            case "3":
                ShiftService.DeleteAShift();
                break;
            case "4":
                ShiftService.UpdateAShift();
                break;
            default:
                Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.");
                break;
        }
    }
}


