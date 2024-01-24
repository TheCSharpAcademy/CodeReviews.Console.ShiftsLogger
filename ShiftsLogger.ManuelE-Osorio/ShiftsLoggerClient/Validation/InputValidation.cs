namespace ShiftsLoggerClient.Validation;

public class InputValidation
{
    public static bool IntValidation(string input)
    {
        return int.TryParse(input, out int result);
    }
}