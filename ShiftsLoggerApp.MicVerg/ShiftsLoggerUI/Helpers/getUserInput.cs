using ShiftsLogger.Models;

namespace ShiftsLoggerUI.Helpers
{
    internal class getUserInput
    {
        internal static ShiftModel GetUserShiftInfo()
        {
            Console.Clear();

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

        internal static ShiftModel GetUserNewShiftInfo()
        {
            Console.WriteLine("Enter a new start time: ");
            DateTime startTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter a new end time: ");
            DateTime endTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter a new employee name: ");
            string workerName = Console.ReadLine();

            ShiftModel updatedShift = new ShiftModel
            {
                StartTime = startTime,
                EndTime = endTime,
                WorkerName = workerName,
            };
            return updatedShift;
        }

        internal static string GetUserShiftId(string message)
        {
            Console.WriteLine("\n" + message);
            string output = Console.ReadLine();
            return output;
        }
    }
}
