using ShiftsLoggerUI.View;

namespace ShiftsLoggerUI;

class Program
{
    static async Task Main()
    {
        MainView mainView = new MainView();
        await mainView.Start();
    }
}
