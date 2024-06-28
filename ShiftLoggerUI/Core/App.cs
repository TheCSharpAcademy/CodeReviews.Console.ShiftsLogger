using ShiftLoggerUI;

public class App
{
    private readonly APIClient _client;
    private readonly ConnectionHelper _connectionHelper;
    private bool _isRunning = true;

    public App(string baseUrl)
    {
        var httpClient = new HttpClient();
        _client = new APIClient(baseUrl, httpClient);
        _connectionHelper = new ConnectionHelper(_client);
    }

    public async Task Run()
    {
        while (_isRunning)
        {
            if (await _connectionHelper.CheckConnectionAsync(5))
            {
                await HandleUserInteractionAsync();
            }
            else
            {
                await HandleConnectionFailureAsync();
                break;
            }
        }
    }

    private async Task HandleUserInteractionAsync()
    {
        // Menu options here... (enums) [including exit]
        // Use UserInputManager to handle user interactions
        // Use service classes for API client operations
        // return _isRunning = false for exit..
    }

    private async Task HandleConnectionFailureAsync()
    {
        await Console.Out.WriteLineAsync("Could not connect. Application will close on next key press.");
        Console.ReadLine();
    }
}

