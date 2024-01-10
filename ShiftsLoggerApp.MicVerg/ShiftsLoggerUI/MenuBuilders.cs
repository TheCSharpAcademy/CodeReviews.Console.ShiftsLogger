using ShiftsLoggerUI.Helpers;

namespace ShiftsLoggerUI
{
    internal class MenuBuilders
    {
        private readonly ShiftsLoggerService _shiftsLoggerService;

        public MenuBuilders(ShiftsLoggerService shiftsLoggerService)
        {
            _shiftsLoggerService = shiftsLoggerService;
        }
        internal async Task MainMenu()
        {
            bool isAppRunning = true;

            while (isAppRunning)
            {
                Console.Clear();
                Console.WriteLine("Shift management: \n");
                Console.WriteLine("Press 1 to add a new shift\n");
                Console.WriteLine("Press 2 to show all shifts\n");
                Console.WriteLine("Press 3 to show a specific shift\n");
                Console.WriteLine("Press 4 to update a specific shift\n");
                Console.WriteLine("Press 5 to delete a specific shift\n");
                Console.WriteLine("Press 0 to exit\n");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        var newShift = getUserInput.GetUserShiftInfo();
                        await _shiftsLoggerService.AddShift(newShift);
                        break;
                    case "2":
                        await _shiftsLoggerService.GetShifts();
                        break;
                    case "3":
                        var idToGet = getUserInput.GetUserShiftId("What ID do you want to look up?");
                        await _shiftsLoggerService.GetShiftById(int.Parse(idToGet));
                        break;
                    case "4":
                        var idToUpdate = getUserInput.GetUserShiftId("What ID do you want to update?");
                        await _shiftsLoggerService.UpdateShift(int.Parse(idToUpdate), getUserInput.GetUserNewShiftInfo());
                        break;
                    case "5":
                        var idToDelete = getUserInput.GetUserShiftId("What ID do you want to delete");
                        await _shiftsLoggerService.DeleteShift(int.Parse(idToDelete));
                        break;
                    case "0":
                        isAppRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
