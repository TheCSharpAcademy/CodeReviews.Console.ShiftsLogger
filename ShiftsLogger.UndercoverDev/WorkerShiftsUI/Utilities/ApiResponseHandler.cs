using System.Net.Http.Json;

namespace WorkerShiftsUI.Utilities;
public class ApiResponseHandler
{
    public static async Task<T?> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            throw new HttpRequestException($"API request failed: {response.StatusCode}");
        }
    }
}
