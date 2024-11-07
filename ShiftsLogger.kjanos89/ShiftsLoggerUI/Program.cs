namespace ShiftsLoggerUI;

class Program
{
    static void Main(string[] args)
    {
        Service.InitializeClient();
        Menu menu = new Menu();
        menu.ShowMenu();
    }
}