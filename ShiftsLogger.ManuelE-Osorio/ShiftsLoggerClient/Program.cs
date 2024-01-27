using ShiftsLoggerClient.Controllers;

namespace ShiftsLoggerClient;

public class ShiftsLoggerClientProgram
{
    public static void Main()
    {
        var baseAddress = JsonController.AppSettings();
        DataController dataCont;
        if(baseAddress != null)
        {
            dataCont = new DataController(baseAddress);
            dataCont.Main();
        }
        else
        {
            Console.WriteLine("Please configure your appsettings.json");
            Thread.Sleep(5000);
        }
    }
}