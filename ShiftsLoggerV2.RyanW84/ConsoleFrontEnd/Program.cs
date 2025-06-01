using ConsoleFrontEnd.MenuSystem;

namespace ConsoleFrontEnd;

public class Program
{
    public static async Task Main(string[] args)
    {
        await MainMenu.DisplayMainMenu();
    }
}
