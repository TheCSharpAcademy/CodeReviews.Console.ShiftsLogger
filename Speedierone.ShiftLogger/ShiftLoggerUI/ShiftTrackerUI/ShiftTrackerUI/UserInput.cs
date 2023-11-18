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
    }
}
