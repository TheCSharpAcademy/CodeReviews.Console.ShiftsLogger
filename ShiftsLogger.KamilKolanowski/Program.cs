using ShiftsLogger.KamilKolanowski.Services;

namespace ShiftsLogger.KamilKolanowski;

class Program
{
    static void Main(string[] args)
    {
        WorkerService workerService = new();
        workerService.AddWorker();
    }
}
