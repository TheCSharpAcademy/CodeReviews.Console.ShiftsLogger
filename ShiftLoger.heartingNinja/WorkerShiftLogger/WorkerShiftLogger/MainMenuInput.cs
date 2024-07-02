namespace ShiftLogger;

internal class MainMenuInput
{
    internal static async Task Main()
    {
        Console.WriteLine("Welcome to the Super Hero Worker Log in UI!");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Manager");
            Console.WriteLine("2. Worker");
            Console.WriteLine("3. Quit");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ManagmentInput.ManagerUI();
                    break;

                case "2":
                    await WorkerInput.WorkerUI();
                    break;

                case "3":
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    return;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}