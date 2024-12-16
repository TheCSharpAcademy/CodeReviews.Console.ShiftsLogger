namespace ShiftLoggerUI.Services;

public class ValidationService
{
    public string? ValidateString(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        return input;
    }
}
