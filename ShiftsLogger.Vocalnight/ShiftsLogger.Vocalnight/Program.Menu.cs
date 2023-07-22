
partial class Program
{
    static async void viewMenu()
    {
        while (true)
        {

            Console.WriteLine("Welcome to the Shift Logger");
            Console.WriteLine("Select an option with the numpad");
            Console.WriteLine(@"
1 - Add new shift
2 - Show shifts
3 - Delete shifts
4 - Register an Employee
5 - Show Employees
6 - Exit");

            ConsoleKey choice = Console.ReadKey(intercept: true).Key;

            switch (choice)
            {
                case ConsoleKey.NumPad1:
                    await AddShift();
                    break;

                case ConsoleKey.NumPad2:
                    await ShowShifts(false);
                    break;

                case ConsoleKey.NumPad3:
                    await DeleteShift();
                    break;

                case ConsoleKey.NumPad4:
                    await CreateEmployee();
                    break;

                case ConsoleKey.NumPad5:
                    await ShowEmployees();
                    break;

                case ConsoleKey.NumPad6:
                    Console.Clear();
                    Console.Write("Bye!");
                    return;
            }
        }
    }
}

