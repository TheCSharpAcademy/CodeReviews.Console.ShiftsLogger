using ShiftsLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI.Helpers
{
    internal class getUserInput
    {
        internal static ShiftModel getUserShiftInfo()
        {
            Console.WriteLine("Enter start time: ");
            DateTime startTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter end time: ");
            DateTime endTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter employee name: ");
            string workerName = Console.ReadLine();

            ShiftModel newShift = new ShiftModel
            {
                StartTime = startTime,
                EndTime = endTime,
                WorkerName = workerName,
            };
            return newShift;
        }
    }
}
