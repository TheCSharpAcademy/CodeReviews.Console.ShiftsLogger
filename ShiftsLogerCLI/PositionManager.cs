using ShiftsLogerCLI.Models;

namespace ShiftsLogerCLI;


internal static class PositionManager
{
    public static List<string> Positions { get; private set; } = [];
    public static void SetPositions(List<Worker> workers)
    {
        Positions = workers.Select(w=>w.Position).Distinct().ToList();    
    }
}