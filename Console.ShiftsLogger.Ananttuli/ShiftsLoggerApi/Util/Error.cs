namespace ShiftsLoggerApi.Util;

public enum ErrorType
{
    BusinessRuleValidation,
    DatabaseNotFound
}

public class Error(ErrorType errorType, string? message = null)
{
    public ErrorType Type { get; set; } = errorType;
    public string? Message { get; set; } = message;
}