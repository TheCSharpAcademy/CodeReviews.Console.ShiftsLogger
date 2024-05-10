namespace shiftloggerconsole.UserInterface;

internal class UserInput
{
    internal static int GetShiftIdInput()
    {
        Console.WriteLine("\nPlease select the id of the shift you wish to edit");
        var selectedId = Console.ReadLine();
        var shiftId = 0;

        while (!Int32.TryParse(selectedId, out shiftId))
        {
            Console.WriteLine("please enter a shift id. it must be a number");
            selectedId = Console.ReadLine();
        }

        return shiftId;
    }

    internal static DateTime GetPunchIn()
    {
        Console.WriteLine("Please enter the clock-in date and time (format: yyyy-MM-dd HH:mm:ss):");
        while (true)
        {
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter the date and time in the format yyyy-MM-dd HH:mm:ss");
            }
        }
    }

    internal static DateTime GetPunchOut()
    {
        Console.WriteLine("Please enter the clock-out date and time (format: yyyy-MM-dd HH:mm:ss):");
        while (true)
        {
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter the date and time in the format yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}
