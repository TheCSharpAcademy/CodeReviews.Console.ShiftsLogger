using ShiftsLoggerClient.Controllers;
using ShiftsLoggerClient.Helpers;

namespace ShiftsLoggerClient;

public class ShiftsLoggerClientProgram
{
    public static void Main()
    {
        var baseAddress = JsonHelper.AppSettings();
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