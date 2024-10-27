using ShiftsLogerCLI.Enums;

namespace ShiftsLogerCLI;
internal class Program
{
    static void Main(string[] args)
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            switch (MenuBuilder.GetOption())
            {
                case Option.Workers:
                    MenuBuilder.DisplayShiftsWorkerOptions(true);
                    break;
                case Option.Shifts:
                    MenuBuilder.DisplayShiftsWorkerOptions(false);
                    break;
                case Option.Help:
                    MenuBuilder.DisplayHelpMenu();
                    break;
                case Option.Exit:
                    isRunning = false;
                    break;
            }

        }
    }
}

