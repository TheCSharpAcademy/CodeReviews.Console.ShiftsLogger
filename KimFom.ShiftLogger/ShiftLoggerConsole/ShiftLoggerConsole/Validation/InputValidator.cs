namespace ShiftLoggerConsole.Validation;

public class InputValidator : IInputValidator
{
    public bool IsValidName(string name)
    {
        return !string.IsNullOrEmpty(name);
    }

    public bool IsValidId(string temp, ref int id)
    {
        return int.TryParse(temp, out id);
    }
}