namespace ShiftsLoggerUI;

internal class ShiftsController
{
    internal static void RunApp()
    {
        while (true)
        {
            UserInterface.ShowAccountMenu();
            UserInterface.ShowMainMenu();
        }
    }

    internal static Task<bool> Login((string, string) value)
    {
        return ShiftsService.Login(value);
    }

    internal static void Register((string, string) value)
    {
        ShiftsService.Register(value);
    }

    internal static void AddShift((DateTime, DateTime) value)
    {
        ShiftsService.AddShift(value);
    }

    internal static Task<List<Shift>> GetShifts()
    {
        return ShiftsService.GetShifts();
    }

    internal static void DeleteShiftById(int id)
    {
        ShiftsService.DeleteById(id);
    }

    internal static void UpdateShift(Shift shift, (DateTime, DateTime) value)
    {
        ShiftsService.UpdateShift(shift, value);
    }
}
