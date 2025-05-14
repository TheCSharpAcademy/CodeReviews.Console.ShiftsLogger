using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.KamilKolanowski.Models;
using ShiftsLogger.KamilKolanowski.Models.Data;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Services;

internal class WorkerDbService
{
    private readonly ShiftsLoggerDbContext _context;

    public WorkerDbService(ShiftsLoggerDbContext context)
    {
        _context = context;
    }

    internal async Task AddWorkerAsync(Worker worker)
    {
        try
        {
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add worker due to: {ex.Message}");
        }
    }

    internal async Task UpdateWorkerAsync(Worker worker)
    {
        try
        {
            _context.Workers.Update(worker);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to edit worker due to: {ex.Message}");
        }
    }

    internal async Task DeleteWorkerAsync(int workerId)
    {
        try
        {
            var worker = await _context.Workers.FindAsync(workerId);
            if (worker != null)
            {
                _context.Workers.Remove(worker);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete worker due to: {ex.Message}");
        }
    }

    internal async Task<Worker?> ReadWorkerAsync(int id)
    {
        try
        {
            return await _context.Workers.FirstOrDefaultAsync(worker => worker.WorkerId == id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read worker: {ex.Message}");
            return null;
        }
    }

    internal async Task<List<Worker>> ReadAllWorkersAsync()
    {
        try
        {
            return await _context.Workers.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read all workers: {ex.Message}");
            return new List<Worker>();
        }
    }
}
