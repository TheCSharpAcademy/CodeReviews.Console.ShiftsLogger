namespace ShiftLoggerUI.Services;

using ShiftLoggerUI;

internal class ConnectionHelper
{
    private readonly APIClient _client;

    public ConnectionHelper(APIClient client)
    {
        _client = client;
    }

    public async Task<bool> CheckConnectionAsync(int retries)
    {
        for (int i = 0; i < retries; i++)
        {
            try
            {
                // In future applications implement a dedicated health check endpoint for more efficient connection testing.
                // The current approach using GetAllEmployeesAsync works for this test application,
                // but is not ideal for production due to higher data usage and potential performance impact.
                // A lightweight health check endpoint would be more suitable for frequent connectivity tests.
                await _client.GetAllEmployeesAsync();
                Console.WriteLine("Connection established successfully.");
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed connection... Attempt #{i + 1}");
                Console.WriteLine($"{ex.Message}");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
        
        return false;
    }
}