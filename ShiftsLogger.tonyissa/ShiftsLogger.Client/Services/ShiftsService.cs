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

    public static async Task SubmitShiftAsync()
    {
        var shift = ShiftsUI.GetShiftInformation();
        await ShiftsRepository.CreateShiftAsync(shift);
        AnsiConsole.WriteLine("Shift created successfully. Press any key to continue...");
        Console.ReadKey();
    }

    public static async Task ChangeShiftAsync()
    {
        var list = await ShiftsRepository.GetShiftsAsync();
        ShiftsUI.ListEntries(list);

        var selection = ShiftsUI.GetShiftId(list);
        var shift = ShiftsUI.GetShiftInformation();
        shift.Id = selection;

        await ShiftsRepository.UpdateShiftAsync(shift);
        AnsiConsole.WriteLine("Shift updated successfully. Press any key to continue...");
        Console.ReadKey();
    }

    public static async Task RemoveShiftAsync()
    {
        var list = await ShiftsRepository.GetShiftsAsync();
        ShiftsUI.ListEntries(list);

        var selection = ShiftsUI.GetShiftId(list);

        if (!ShiftsUI.ConfirmSelection())
        {
            return;
        }

        await ShiftsRepository.DeleteShiftAsync(selection);
        AnsiConsole.WriteLine("Shift deleted successfully. Press any key to continue...");
        Console.ReadKey();
    }
}