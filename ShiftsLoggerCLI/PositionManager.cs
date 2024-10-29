using ShiftsLoggerCLI.Models;

namespace ShiftsLoggerCLI;


internal static class PositionManager
{
    public static List<string> Positions { get; private set; } = ["Add new position"];
    public static void SetPositions(List<WorkerRead> workers)
    {
        Positions = workers.Select(w=>w.Position).Distinct().ToList();
        Positions.Add("Add new position");
    }
}