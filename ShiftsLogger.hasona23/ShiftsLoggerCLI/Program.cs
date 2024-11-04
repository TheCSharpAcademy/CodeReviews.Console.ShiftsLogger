using ShiftsLoggerCLI.Enums;
using ShiftsLoggerCLI.Services;


namespace ShiftsLoggerCLI;
internal class Program
{
    static  void Main(string[] args)
    {   InputHandler inputHandler = new();
        WorkerService workerService = new(inputHandler);
        ShiftService shiftService = new(inputHandler);
        bool isRunning = true;
        
        while (isRunning)
        {
            Console.Clear();
            switch (MenuBuilder.GetOption())
            {
                case Option.Workers:
                    workerService.HandleWorkers();
                    break;
                case Option.Shifts:
                    shiftService.HandleShifts();
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

