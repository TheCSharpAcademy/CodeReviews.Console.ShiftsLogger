using Spectre.Console.Rendering;

namespace ShiftsLoggerUI;

public class Program
{
    public static void Main(string[] args)
    {
        bool appRunning = true;
        while (appRunning) appRunning = UserInterface.MainMenu(appRunning);
    }
}