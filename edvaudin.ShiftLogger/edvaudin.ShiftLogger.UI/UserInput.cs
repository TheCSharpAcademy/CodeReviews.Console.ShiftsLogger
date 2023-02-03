namespace ShiftLogger.UI
{
    internal class UserInput
    {
        internal static string GetOption()
        {
            string input = Console.ReadLine();
            while (!Validator.IsValidOption(input))
            {
                Console.Write("\nThis is not a valid input. Please enter one of the above options: ");
                input = Console.ReadLine();
            }
            return input;
        }

        internal static DateTime GetTime()
        {
            throw new NotImplementedException();
        }
    }
}
