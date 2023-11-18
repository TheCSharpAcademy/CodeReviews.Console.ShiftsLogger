using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTrackerUI
{
    internal class UserInput
    {
        public static async Task AddShift()
        {
            Console.Clear();
            await ShiftService.GetShifts();

            Console.WriteLine("\nEnter your name");
            var name = Console.ReadLine();

            Console.WriteLine("\nPlease enter the start date of your shift in format DD/MM/YYYY");
            var startDate = UserValidation.CheckDate();

            Console.WriteLine("\nPlease enter the start time of the shift in format HH:MM.");
            var startTime = UserValidation.CheckTime();
            
            Console.WriteLine("\nPlease enter the End Time of the shift");
            var endTime = UserValidation.CheckTime();

            var duration = Helpers.GetDuration(startTime, endTime);

            await ShiftService.PostShift(name, startDate, startTime, endTime, duration);

        }
        public static async Task PutShift()
        {
            Console.Clear();
            await ShiftService.GetShifts();

            var Id = UserValidation.GetNumberInput("Please enter the Id of the record you wish to edit.");
           
            Console.WriteLine("\nPlease enter your name");
            var name = Console.ReadLine();

            Console.WriteLine("\nPlease enter the start date of shift in format DD/MM/YYYY");
            var startDate = UserValidation.CheckDate();

            Console.WriteLine("\nPlease enter the start time of the shift in format HH:MM");
            var startTime = UserValidation.CheckTime();

            Console.WriteLine("\nPlease enter the End Time of the shift");
            var endTime = UserValidation.CheckTime();

            var duration = Helpers.GetDuration(startTime, endTime);

            await ShiftService.PutShift(Id, name, startDate, startTime, endTime, duration);
        }
        public static async Task DeleteShift()
        {
            Console.Clear();
            await ShiftService.GetShifts();

            var Id = UserValidation.GetNumberInput("Please enter Id of the record you wish to delete");

            await ShiftService.DeleteShift(Id);
        }
    }
}
