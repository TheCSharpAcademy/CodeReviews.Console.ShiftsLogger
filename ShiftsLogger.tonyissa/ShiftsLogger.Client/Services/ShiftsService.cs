using ShiftsLogger.Client.Repositories;
using ShiftsLogger.Client.UI;
using Spectre.Console;

namespace ShiftsLogger.Client.Services;

public static class ShiftsService
{
    public static async Task ListShiftsAsync()
    {
        var list = await ShiftsRepository.GetShiftsAsync();
        ShiftsUI.ListEntries(list);
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}