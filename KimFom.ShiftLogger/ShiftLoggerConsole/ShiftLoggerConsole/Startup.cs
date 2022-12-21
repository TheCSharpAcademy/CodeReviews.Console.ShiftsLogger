using ShiftLoggerConsole.Controller;

namespace ShiftLoggerConsole;

public class Startup : IStartup
{
    private readonly IShiftController _shiftController;

    public Startup(IShiftController shiftController)
    {
        _shiftController = shiftController;
    }
    public async Task Run()
    {
        await _shiftController.Start();
    }
}