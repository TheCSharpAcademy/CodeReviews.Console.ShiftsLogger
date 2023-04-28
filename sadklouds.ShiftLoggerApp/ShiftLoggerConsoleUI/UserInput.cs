using System.Globalization;

namespace ShiftLoggerConsoleUI;
internal static class UserInput
{
    public static int GetIdInput()
    {
        int id;
        Console.Write("Enter shift id: ");
        string? textId = Console.ReadLine();
        while (Validator.IsValid(textId) == false)
        {
            Console.WriteLine("Invalid id given");
            GetIdInput();
        }
        int.TryParse(textId, out id);
        return id;
    }

    public static DateTime GetShiftStartInput()
    {
        Console.Write("Please enter shift start date (dd/MM/yyyy HH:mm): ");
        string textDate = Console.ReadLine();
        DateTime startDate;
        while (Validator.IsValidDateFormat(textDate) == false)
        {
            Console.Write("Invalid shift start format given, Correct formatting is dd/MM/yyyy HH:mm: ");
            textDate = Console.ReadLine();
        }
        DateTime.TryParseExact(textDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
        return startDate;

    }

    public static DateTime GetEndDateInput(DateTime startDate)
    {
        Console.Write("Please enter shift end date (dd/MM/yyyy HH:mm): ");
        string textDate = Console.ReadLine();
        DateTime endDate;
        while (Validator.IsValidDateFormat(textDate) == false)
        {
            Console.Write("Invalid  shift end date format given. Correct formatting is dd/MM/yyyy HH:mm");
            textDate = Console.ReadLine();
        }
        DateTime.TryParseExact(textDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
        while (Validator.IsValidEndDate(textDate, startDate) == false)
        {
            Console.WriteLine("Shift cannot end before it has started, Correct formatting is dd/MM/yyyy HH:mm.");
            GetEndDateInput(startDate);
        }
        return endDate;
    }
}
