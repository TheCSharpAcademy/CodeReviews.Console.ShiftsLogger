namespace STUDY.ASP.ShiftLoggerTryThreeUserInterface;
internal class Helper
{
    public static void ReturnToMainMenu(HttpClient client, string ApiBaseUrl)
    {
        Console.WriteLine("Press Any Key to Return to Main Menu");
        Console.ReadLine();

        Console.Clear();
        MainMenu.ShowMainMenu(client, ApiBaseUrl);
    }
}
