using ShiftLogger.API.Models;

namespace ShiftLogger.UI
{
    internal class UserInput
    {
        public static DateTime GetStartTime()
        {
            string input = Console.ReadLine();

            while (!Validator.IsValidDateInput(input))
            {
                Console.WriteLine("\nInvalid date and time. Use the format: dd/MM/yyyy HH:mm:ss.");
                input = Console.ReadLine();
            }
            return Validator.ConvertToDate(input);
        }

        public static DateTime GetEndTime(DateTime startTime)
        {
            string input = Console.ReadLine();

            while (!Validator.IsValidDateInput(input))
            {
                Console.WriteLine("\nInvalid date and time. Use the format: dd/MM/yyyy HH:mm:ss.");
                input = Console.ReadLine();
            }
            if (!Validator.IsDateAfterStartTime(input, startTime))
            {
                Console.WriteLine("\nYou cannot have finished coding before you started! Enter a different end time.");
                GetEndTime(startTime);
            }
            return Validator.ConvertToDate(input);
        }

        public static string GetOption()
        {
            string input = Console.ReadLine();
            while (!Validator.IsValidOption(input))
            {
                Console.Write("\nThis is not a valid input. Please enter one of the above options: ");
                input = Console.ReadLine();
            }
            return input;
        }

        internal static async Task<int> GetId()
        {
            List<Shift> shifts = await ShiftService.LoadShifts();
            List<int> validIds = shifts.Select(o => o.Id).ToList();
            bool validIdEntered = false;
            while (!validIdEntered)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (validIds.Contains(result) || result == -1)
                    {
                        validIdEntered = true;
                        return result;
                    }
                }
                Console.Write("\nThis is not a valid id, please enter a number or to return to main menu type '-1': ");
            }
            return -1;
        }
    }
}
