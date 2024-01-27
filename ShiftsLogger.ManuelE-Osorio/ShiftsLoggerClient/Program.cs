using ShiftsLoggerClient.Controllers;

namespace ShiftsLoggerClient;

public class ShiftsLoggerClientProgram
{
    public static void Main()
    {
        var dataCont = new DataController();
        dataCont.Main();
    }
}