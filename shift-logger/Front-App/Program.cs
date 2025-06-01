namespace Front_App;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Menu menu = new Menu(new ShiftApiClient());
        await menu.ShowMenu();
    }
}