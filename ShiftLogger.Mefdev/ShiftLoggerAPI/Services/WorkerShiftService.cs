using Microsoft.EntityFrameworkCore;
using ShiftLogger.Mefdev.ShiftLoggerAPI.Models;

namespace ShiftLogger.Mefdev.ShiftLoggerAPI.Services;

public class WorkerShiftService
{
    private readonly WorkerShiftContext _context;

    public WorkerShiftService(WorkerShiftContext context)
    {
        _context = context;
    }

    public async Task<WorkerShift> CreateWorkerShift(WorkerShift workerShift)
    {
        var shift = await _context.WorkerShifts.FindAsync(workerShift.Id);
        if(shift is not null)
        {
            throw new Exception("The worker shift is already exists");
        }
        await _context.AddAsync(workerShift);
        await _context.SaveChangesAsync();
        return workerShift;
    }

    public async Task<List<WorkerShift>> GetWorkerShifts()
    {
         var shitfs = await _context.WorkerShifts.ToListAsync();
         await _context.SaveChangesAsync();
         return shitfs;
    }

    public async Task<WorkerShift?> GetWorkerShift(int id)
    {
        var shift = await _context.WorkerShifts.FindAsync(id);
        await _context.SaveChangesAsync();
        return shift;
    }

    public async Task<WorkerShift?> UpdateWorkerShift(int id, WorkerShift newWorkerShift)
    {
        var shiftToUpdate = await _context.WorkerShifts.FindAsync(id);
        if(shiftToUpdate is null)
        {
            return null;
        }
        shiftToUpdate.EmployeeName = newWorkerShift.EmployeeName;
        shiftToUpdate.EmployeeName = newWorkerShift.EmployeeName;
        shiftToUpdate.StartDate = newWorkerShift.StartDate;
        shiftToUpdate.EndDate = newWorkerShift.EndDate;

        _context.Entry(shiftToUpdate).State = EntityState.Modified;
        try
            {
                await _context.SaveChangesAsync();
            }
        catch (DbUpdateConcurrencyException)
        { 
            throw;
        }
        return shiftToUpdate;
    }
    
    public async Task<bool> DeleteWorkerShift(int id)
    {
        var shift = await _context.WorkerShifts.FindAsync(id);
        if(shift is null)
        {
            return false;
        }
        _context.Remove(shift);
        await _context.SaveChangesAsync();
        try
            {
                await _context.SaveChangesAsync();
            }
        catch (DbUpdateConcurrencyException)
        { 
            throw;
        }
        return true;
    }
}