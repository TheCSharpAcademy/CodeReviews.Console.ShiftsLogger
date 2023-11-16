using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTrackerUI
{
    internal class UserInput
    {
        public static string AddShift()
        {
            Console.WriteLine("Enter your name");
            var name = Console.ReadLine();

            Console.WriteLine("Please enter the start date of your shift in format DD:MM:YYYY");
            var startDate = Console.ReadLine();

            Console.WriteLine("Please enter the start time of the shift.");
            var startTime = Console.ReadLine();
            
            Console.WriteLine("Please enter the End Time of the shift");
            var endTime = Console.ReadLine();

            var duration
            
        }
    }
}
