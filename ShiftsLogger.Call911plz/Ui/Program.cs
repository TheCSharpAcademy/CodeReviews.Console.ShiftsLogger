namespace Ui;

using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        MainMenuController mainMenu = new();
        await mainMenu.StartAsync();
    }
}
