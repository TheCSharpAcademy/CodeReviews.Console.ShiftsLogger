using static ShiftsLoggerUI.Helpers;

namespace ShiftsLoggerUI;

public static class DataValidation
{
    public static int GetNumberInput()
    {
        var userInput = Console.ReadLine();
        int number;

        while (!int.TryParse(userInput, out number))
        {
            DisplayError($"{userInput} is not a valid number. Please try again.");
            userInput = Console.ReadLine();
        }

        return number;
    }

    public static string GetTextInput()
    {
        var textInput = Console.ReadLine();

        while (string.IsNullOrEmpty(textInput))
        {
            DisplayError("Input can't be empty, try again");
            textInput = Console.ReadLine();
        }

        return textInput;
    }

    public static int GetShiftIdInput()
    {
        var id = GetNumberInput();

        while (!ShiftExists(id))
        {
            DisplayError($"The ID: {id} is invalid, try again");
            id = GetNumberInput();
        }

        return id;
    }

    public static DateTime GetShiftInput()
    {
        var input = Console.ReadLine();

        while (!DateTime.TryParseExact(input, "HH:mm", System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None, out _))
        {
            DisplayError("Invalid Format, try again");
            input = Console.ReadLine();
        }

        var shift = DateTime.ParseExact(input, "HH:mm", System.Globalization.CultureInfo.InvariantCulture,
                                             System.Globalization.DateTimeStyles.None);

        return shift;
    }
}