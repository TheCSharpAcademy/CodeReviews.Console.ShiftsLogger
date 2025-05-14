using ShiftsLoggerUI.View;

namespace ShiftsLoggerUI;

class Program
{
    static async Task Main(string[] args)
    {
        MainView mainView = new MainView();
        await mainView.Start();
    }
}
