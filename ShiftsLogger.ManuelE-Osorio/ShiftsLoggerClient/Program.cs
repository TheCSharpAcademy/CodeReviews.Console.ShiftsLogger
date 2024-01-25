using System.Diagnostics.SymbolStore;
using System.Web;
using ShiftsLoggerClient.Controllers;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient;

public class ShiftsLoggerClientProgram
{
    public static void Main()
    {
        // var shift = new Shift(new DateTime(2024,1,25, 20, 0, 0).ToUniversalTime(), 
        //     null);
        // Console.WriteLine(shift.Length);
        var dataCont = new DataController();
        dataCont.Main();
    }
}