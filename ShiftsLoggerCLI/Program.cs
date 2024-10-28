using ShiftsLoggerCLI.Enums;
using ShiftsLoggerCLI.Handlers;


namespace ShiftsLoggerCLI;
internal class Program
{
    static  void Main(string[] args)
    {   InputHandler inputHandler = new();
        WorkerHandler workerHandler = new(inputHandler);
        ShiftsHandler shiftsHandler = new(inputHandler);
        ResponseManager.InitResponseManager();
        bool isRunning = true;
        
        while (isRunning)
        {
            Console.Clear();
            switch (MenuBuilder.GetOption())
            {
                case Option.Workers:
                    workerHandler.HandleWorkers();
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

