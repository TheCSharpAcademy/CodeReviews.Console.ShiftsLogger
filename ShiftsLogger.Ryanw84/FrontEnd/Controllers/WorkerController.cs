using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ShiftsLogger.Ryanw84.Data;
using ShiftsLogger.Ryanw84.Models;

namespace FrontEnd.Controllers;

internal class WorkerController
{
    private static readonly ShiftsDbContext _dbContext;

    internal WorkerController(ShiftsDbContext dbContext)
    {
        using var db = dbContext;
    }

    internal static List<Worker> GetAllWorkers()
    {
        using var db = _dbContext;
        var workers = db
            .Worker.Include(workers => workers.Shifts)
            .Include(workers => workers.Locations)
            .ToList();

        return workers;
    }

    internal static Worker GetWorkerById(int workerId)
    {
        using var db = _dbContext;

        var worker = db
            .Worker.Include(workers => workers.Shifts)
            .Include(workers => workers.Locations)
            .FirstOrDefault(w => w.WorkerId == workerId);

        return worker;
    }
}
