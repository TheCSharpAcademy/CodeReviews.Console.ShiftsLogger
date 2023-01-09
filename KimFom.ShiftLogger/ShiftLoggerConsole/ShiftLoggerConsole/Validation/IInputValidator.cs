namespace ShiftLoggerConsole.Validation;

public interface IInputValidator
{
    public bool IsValidName(string name);
    public bool IsValidId(string temp, ref int id);
}