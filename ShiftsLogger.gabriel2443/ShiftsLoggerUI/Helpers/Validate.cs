namespace ShiftsLoggerUI.Helpers
{
    internal static class Validate
    {
        internal static string ReadStringInput(string prompt, bool allowNull)
        {
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine()?.Trim();
                if (!allowNull && string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            } while (!allowNull && string.IsNullOrEmpty(input));

            return input;
        }

        internal static DateTime ReadDateTimeInput(string prompt)
        {
            DateTime dateTime;
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "HH:mm", null, System.Globalization.DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }
                Console.WriteLine("Invalid date time. Please enter the time in HH:mm (24 hour format).");
            }
        }

        internal static int ReadShiftIdInput(string prompt)
        {
            int shiftId;
            while (true)
            {
                Console.WriteLine(prompt);
                var inputId = Console.ReadLine();
                if (int.TryParse(inputId, out shiftId))
                {
                    return shiftId;
                }
                Console.WriteLine("Invalid input. Please enter a valid shift number.");
            }
        }
    }
}