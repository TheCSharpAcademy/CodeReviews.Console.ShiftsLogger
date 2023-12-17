using ShiftsLogger.Models;
using ShiftsLoggerUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI
{
    internal class MenuBuilders
    {
        private readonly ShiftsLoggerService _shiftsLoggerService;

        public MenuBuilders(ShiftsLoggerService shiftsLoggerService)
        {
            _shiftsLoggerService = shiftsLoggerService;
        }
        internal async void MainMenu()
        {
            Console.WriteLine("Shift management: \n");
            Console.WriteLine("Press 1 to add a new shift\n");
            Console.WriteLine("Press 2 to show all shifts\n");
            Console.WriteLine("Press 3 to show a specific shift\n");
            Console.WriteLine("Press 4 to update a specific shift\n");
            Console.WriteLine("Press 5 to delete a specific shift\n");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    var newShift = getUserInput.getUserShiftInfo();
                    _shiftsLoggerService.AddShift(newShift);
                    break;
                case "2":
                    await _shiftsLoggerService.GetShifts();
                    break;
                //case "3":
                //    ShiftsLoggerService.GetShiftById();
                //    break;
                //case "4":
                //    ShiftsLoggerService.UpdateShift();
                //    break;
                //case "5":
                //    ShiftsLoggerService.DeleteShift();
                //    break;
                default:
                    Console.WriteLine("Invalid input");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
