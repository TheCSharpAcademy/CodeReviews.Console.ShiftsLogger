namespace ShiftsLoggerAPI.Models;

public class ServiceResponse<T>
{
    public T? Data { get; set; }

    public bool Success { get; set; } = true;

    public string Message { get; set; } = null;

    public string Error { get; set; } = null;
}