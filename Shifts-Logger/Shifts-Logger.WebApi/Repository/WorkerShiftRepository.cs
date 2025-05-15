using Microsoft.EntityFrameworkCore;
using ShiftsLogger.WebApi.Data;
using ShiftsLogger.WebApi.Models;

namespace ShiftsLogger.WebApi.Repository;

public class WorkerShiftRepository
{
    private readonly WorkerShiftContext _context;

    public WorkerShiftRepository(WorkerShiftContext context)
    {
        _context = context;
    }

    internal async Task<List<Shift>> ReadAllWorkersAllShifts()
    {
        List<Shift> allShifts = await _context.Shifts.ToListAsync();

        return allShifts;
    }

    internal async Task<List<Shift>?> ReadAllShiftsForWorker(int id)
    {
        Worker? worker = await _context.Workers.SingleOrDefaultAsync(w => w.Id == id);
        if (worker == null) return null;

        List<Shift> workerShifts =  await _context.Shifts.Where(s => s.WorkerId == id).ToListAsync();

        return workerShifts;
    }
    internal async Task<List<Worker>> ReadAllWorkers()
    {
        List<Worker> workers = await _context.Workers.ToListAsync();
        return workers;
    }

    internal Worker? ReadWorker(int id)
    {
        return _context.Workers.SingleOrDefault(w => w.Id == id);
    }

    internal async Task<Worker> AddWorker(string name)
    {
        Worker newWorker = new Worker { Name = name };
        await _context.Workers.AddAsync(newWorker);
        await _context.SaveChangesAsync();
        return newWorker;
    }

    internal async Task<Shift> AddShiftToWorker(int workerId, DateTime startTime, DateTime endTime)
    {
        Shift newShift = new Shift() {
            WorkerId = workerId,
            StartTime = startTime,
            EndTime = endTime };

        await _context.Shifts.AddAsync(newShift);
        await _context.SaveChangesAsync();

        return newShift;
    }

    internal async Task<bool> UpdateWorker(int id, string newName)
    {
        Worker? worker = await _context.Workers.SingleOrDefaultAsync(w => w.Id == id);
        if (worker != null)
        {
            worker.Name = newName;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    internal async Task<bool> UpdateShift(int id, DateTime newStartTime, DateTime newEndTime)
    {
        Shift? shift = await _context.Shifts.SingleOrDefaultAsync(s => s.Id == id);
        if (shift != null)
        {
            shift.StartTime = newStartTime;
            shift.EndTime = newEndTime;

            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    internal async Task<bool> DeleteWorker(int id)
    {
        Worker? worker = await _context.Workers.SingleOrDefaultAsync(w => w.Id == id);

        if (worker != null)
        {
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    internal async Task<bool> DeleteShift(int id)
    {
        Shift? shift = await _context.Shifts.SingleOrDefaultAsync(s => s.Id == id);
        if (shift != null)
        {
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }

}
