using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI
{
    internal class MenuBuilders
    {
        internal static void MainMenu()
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
                    ShiftsLoggerUIService.AddShift();
                    break;
                case "2":
                    ShiftsLoggerUIService.GetShifts();
                    break;
                case "3":
                    ShiftsLoggerUIService.GetShiftById();
                    break;
                case "4":
                    ShiftsLoggerUIService.UpdateShift();
                    break;
                case "5":
                    ShiftsLoggerUIService.DeleteShift();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
