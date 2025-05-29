using ConsoleFrontEnd.MenuSystem;


namespace ConsoleFrontEnd;

public class Program
{
	public static async Task Main(string[] args)
	{
		MainMenu mainMenu = new();
		await mainMenu.DisplayMainMenu();
	}
}
