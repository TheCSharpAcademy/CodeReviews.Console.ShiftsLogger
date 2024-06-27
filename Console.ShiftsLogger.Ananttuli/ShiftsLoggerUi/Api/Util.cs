using System.Text.Json;

namespace ShiftsLoggerUi.Api;

public class Util
{
    public static T AssertNonNull<T>(T? item)
    {
        if (item == null)
        {
            throw new Exception("Missing value");
        }

        return item;
    }

    public static string? TryParseJsonStringOrNull(JsonElement element, string propertyName)
    {
        bool success = element.TryGetProperty(
            $"{propertyName}",
            out JsonElement value
        );

        if (success)
        {
            var cleanedValue = value.ToString().Trim();
            return cleanedValue.Length > 0 ? cleanedValue : null;
        }

        return null;
    }
}

public class Response<T>(bool success, T data, string? message = null)
{
    public bool Success { get; } = success;

    public T Data { get; } = data;

    public string? Message = message;

    public void Deconstruct(out bool success, out T data)
    {
        success = Success;
        data = Data;
    }
}