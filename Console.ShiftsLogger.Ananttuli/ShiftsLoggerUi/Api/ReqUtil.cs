using System.Net.Http.Json;
using System.Text.Json;

namespace ShiftsLoggerUi.Api;

public class ReqUtil
{
    public static T AssertNonNull<T>(T? item)
    {
        if (item == null)
        {
            throw new Exception("Missing value");
        }

        return item;
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

public class ApiErrorResponse
{
    public int? Type { get; set; }
    public string? Message { get; set; }

    public static async Task<Response<string?>> ExtractErrorFromResponse(HttpResponseMessage? response)
    {
        var unknownError = new Response<string?>(true, "Could not retrieve response");

        try
        {
            if (response == null)
            {
                return unknownError;
            }

            if (response.IsSuccessStatusCode)
            {
                return new Response<string?>(false, null);
            }

            if (response.Content != null)
            {
                var apiErrorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                return new Response<string?>(
                    true,
                    apiErrorResponse?.Message ?? "Unknown error"
                );
            }

            throw new Exception();
        }
        catch (Exception)
        {
            return unknownError;
        }
    }
}