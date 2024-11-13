using System.Runtime.CompilerServices;

namespace ShiftLoggerAPI.Controllers;
internal static class Utilities
{   
    // 0x0100 flag -> Hint to In-Line flag 
    // The metadata token has the unique ID each function
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDuration(double start, double end)
        => Math.Round(end - start, 2);
}