using ShiftsLogger.Client.Models;
using ShiftsLogger.Client.Repositories;
using ShiftsLogger.Client.UI;

namespace ShiftsLogger.Client.Services;

public static class ShiftsService
{
    public static async Task ListShiftsAsync()
    {
        var list = await ShiftsRepository.GetShiftsAsync();
        ShiftsUI.ListEntries(list);
    }
}