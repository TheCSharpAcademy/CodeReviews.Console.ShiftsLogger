namespace ShiftsLoggerApi.Util;

public class Result<T>(T? data, Error? error)
{
    public T? Data { get; set; } = data;
    public Error? Error { get; set; } = error;

    public void Deconstruct(out T? data, out Error? error)
    {
        data = Data;
        error = Error;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(data, default);
    }

    public static Result<T> Fail(Error? error)
    {
        return new Result<T>(default, error);
    }

}
