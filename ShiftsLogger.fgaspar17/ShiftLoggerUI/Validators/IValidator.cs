using Spectre.Console;

namespace ShiftsLoggerUI;

public interface IValidator
{
    string ErrorMsg { get; set; }
    ValidationResult Validate(string? input);
}