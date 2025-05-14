namespace ShiftsLogger.KamilKolanowski.Services;

internal class DataFetcher
{
    private static readonly HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("http://localhost:5000/"),
    };

    internal async Task<string> GetAsync(string endpoint)
    {
        using HttpResponseMessage response = await sharedClient.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return jsonResponse;
    }
}
