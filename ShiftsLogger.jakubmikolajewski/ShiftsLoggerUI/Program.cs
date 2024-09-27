using ShiftsLoggerUI;

internal class Program
{
    static bool exit;

    public static void Main(string[] args)
    {
        while (!exit)
        {
            MainAsync().Wait();
        }
    }
    public static async Task MainAsync()
    {
        Console.Clear();
        exit = await UserInput.SwitchMenuChoice(UserInput.ShowMenu());
    }
}