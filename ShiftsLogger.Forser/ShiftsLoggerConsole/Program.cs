using ShiftsLoggerConsole.API;

class Program
{
    private static HttpClient apiClient = new()
    {
        BaseAddress = new Uri("https://localhost:7000/api/Shift/")
    };
    static async Task Main()
    {
        var apiRepo = new ShiftLoggerApiAccess(apiClient);
        var controller = new ShiftLoggerController(apiRepo);
        var shiftApp = new UserInterface(controller);

        await shiftApp.Run();
    }
}