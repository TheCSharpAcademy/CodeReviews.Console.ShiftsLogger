namespace ShiftsUI
{
    internal class Program
    {
        static private UserInterface _userInterface = new();
        static void Main(string[] args)
        {
            _userInterface.MainMenu().Wait();
        }
    }
}
