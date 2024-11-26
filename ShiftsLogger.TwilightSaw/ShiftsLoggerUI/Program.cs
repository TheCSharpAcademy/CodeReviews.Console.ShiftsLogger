using ShiftsLoggerUI.Controllers;
using ShiftsLoggerUI.View;

namespace ShiftsLoggerUI;

class Program
{
    static async Task Main(string[] args)
    {
        using HttpClient client = new HttpClient();

        var shiftController = new ShiftController(client);
        var menu = new Menu(shiftController);

        await menu.MainMenu();
    }
}