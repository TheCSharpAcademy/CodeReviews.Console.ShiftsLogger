using ShiftsLoggerUI;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Welcome to the Shifts Logger!");
        await ShowMenu();
    }

    static async Task ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Shifts Logger Menu ---");
            Console.WriteLine("1. View Workers");
            Console.WriteLine("2. Add Worker");
            Console.WriteLine("3. Delete Worker");
            Console.WriteLine("4. View Shifts");
            Console.WriteLine("5. Add Shift");
            Console.WriteLine("6. Update Shift");
            Console.WriteLine("7. Delete Shift");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    await Menu.ViewWorkers();
                    break;
                case "2":
                    await Menu.AddWorker();
                    break;
                case "3":
                    await Menu.DeleteWorker();
                    break;
                case "4":
                    await Menu.ViewShifts();
                    break;
                case "5":
                    await Menu.AddShift();
                    break;
                case "6":
                    await Menu.UpdateShift();
                    break;
                case "7":
                    await Menu.DeleteShift();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
                    break;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
