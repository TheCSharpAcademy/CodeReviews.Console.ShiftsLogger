using ShiftsLogger.AdityaVaidya.Data;
using ShiftsLogger.AdityaVaidya.Models;

namespace ShiftsLogger.AdityaVaidya.Queries;

public class DbQueries
{
    internal static void AddWorker(Worker worker)
    {
        using var context = new ShiftContext();
        context.Add(worker);
        context.SaveChanges();
    }

    internal static void DeleteWorker(int workerId)
    {
        using var context = new ShiftContext();
        var worker = context.Workers.Find(workerId);
        if (worker != null)
        {
            context.Remove(worker);
            context.SaveChanges();
        }
    }

    internal static void EditWorker(int workerId, Worker worker)
    {
        using var context = new ShiftContext();
        var existingWorker = context.Workers.Find(workerId);
        if (existingWorker != null)
        {
            existingWorker.Name = worker.Name;
            existingWorker.EmailId = worker.EmailId;
        }
        context.SaveChanges();
    }

    internal static bool IsGivenWorkerIdPresent(int workerId)
    {
        using var context = new ShiftContext();
        var worker = context.Workers.Find(workerId);
        if (worker == null)
            return false;
        return true;
    }

    internal static void CreateNewShift(Shift shift)
    {
        using var context = new ShiftContext();
        context.Add(shift);
        context.SaveChanges();
    }

    internal static void DeleteShift(int shiftId)
    {
        using var context = new ShiftContext();
        var currentShift = context.Shifts.Find(shiftId);
        if (currentShift != null)
        {
            context.Remove(currentShift);
            context.SaveChanges();
        }
    }

    internal static void EditShift(Shift editedShift, int shiftId)
    {
        using var context = new ShiftContext();
        var existingShift = context.Shifts.Find(shiftId);
        if (existingShift != null)
        {
            existingShift.Date = editedShift.Date;
            existingShift.StartTime = editedShift.StartTime;
            existingShift.EndTime = editedShift.EndTime;
            existingShift.Duration = editedShift.Duration;
            
        }
        context.SaveChanges();
    }

    internal static bool IsGivenShiftIdPresent(int workerId)
    {
        using var context = new ShiftContext();
        var shift = context.Shifts.Find(workerId);
        if (shift == null)
            return false;
        return true;
    }

    internal static List<Shift> GetShiftsByWorkerId(int workerId)
    {
        using var context = new ShiftContext();
        var shifts = context.Shifts
            .Where(s => s.WorkerId == workerId)
            .ToList();
        return shifts;
        //Calling Code
        //var shifts = GetShiftsByWorkerId(workerId);
        //if (shifts.Any())
        //{
        //    foreach (var shift in shifts)
        //    {
        //        Console.WriteLine($"ShiftId: {shift.ShiftId}, Date: {shift.Date}, StartTime: {shift.StartTime}, EndTime: {shift.EndTime}, Duration: {shift.Duration}");
        //    }
        //}
        //else
        //{
        //    Console.WriteLine("No shifts found for the given worker ID.");
        //}
    }
}
