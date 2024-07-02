namespace ShiftsLogger.UI.Exceptions;

public class ApiException : Exception
{
    public ApiException(string message) : base($"[API PROBLEM]: {message}")
    {
    }
}