using System.Net.Http.Json;
using Spectre.Console;

namespace WorkerShiftsUI.Utilities;
public class ApiResponseHandler
{
    public static async Task<T?> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return default;
            }

            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content))
            {
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            throw new HttpRequestException($"API request failed: {response.StatusCode}");
        }
    }

}
