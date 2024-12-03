using ShiftsLogger.Console.UI7;

namespace ShiftsLogger.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenuPrompt mainMenuPrompt = new MainMenuPrompt();
            mainMenuPrompt.MainMenuSelection();
        }
    }
}
