using static ShiftsLoggerUI.InterfaceApi;

namespace ShiftsLoggerUI;

public static class Helpers
{
    public static void DisplayError(string error)
    {
        Console.WriteLine($"\n|--->  {error}  <---|\n");
    }

    public static bool ShiftExists(int id)
    {
        var exists = false;

        try
        {
            if (GetShift(id).Result != null) exists = true;
        }
        catch
        {
            exists = false;
        }

        return exists;
    }
}