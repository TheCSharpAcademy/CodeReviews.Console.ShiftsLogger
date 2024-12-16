namespace ShiftLogger.ShiftTrack;


public class Program()
{
    static async Task Main()
    {
        App app = new App();
        await app.InitializeClientAsync();
    }
}
