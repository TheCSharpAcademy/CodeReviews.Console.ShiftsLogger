using System.Diagnostics.SymbolStore;
using ShiftsLoggerClient.Controllers;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient;

public class ShiftsLoggerClientProgram
{
    public static void Main()
    {
        var dataCont = new DataController();
        dataCont.Main();
    }
}