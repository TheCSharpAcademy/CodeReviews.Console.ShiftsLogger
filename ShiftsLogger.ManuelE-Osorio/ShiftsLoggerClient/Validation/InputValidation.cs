namespace ShiftsLoggerClient.Validation;

public class InputValidation
{
    private static readonly int NameLenght = 100;
    public static bool IntValidation(string input)
    {
        return int.TryParse(input, out int result);
    }

    public static bool NameValidation(string input)
    {
        if(input.Length > 0 && input.Length <= NameLenght)
            return true;
        return false;
    }
}