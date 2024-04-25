namespace STUDY.ASP.ShiftLoggerTryThreeUserInterface;
class Program
{
    static HttpClient client = new HttpClient();
    const string ApiBaseUrl = "https://localhost:7188/api/shiftlogger";
    static void Main(string[] args)
    {
        MainMenu.ShowMainMenu(client, ApiBaseUrl);
    }
}
