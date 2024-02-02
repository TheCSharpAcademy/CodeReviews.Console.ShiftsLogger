using shiftsLogger.doc415.Context;
using shiftsLogger.doc415.Models;
namespace shiftsLogger.doc415.Services;

public class ShiftService
{
    static List<Shift> Shifts { get; }

    static ShiftService()
    {

    }

    public static List<Shift> GetAll()
    {
        ShiftDataContext db = new();
        var Shifts = db.Shifts.ToList();
        return Shifts;
    }

    public static Shift GetByID(int _id)
    {
        ShiftDataContext db = new();
        try
        {
            var result = db.Shifts.Single(x => x.Id == _id);
            return result;
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static void Add(Shift shift)
    {
        ShiftDataContext db = new();
        db.Add(shift);
        db.SaveChanges();
    }

    public static void Delete(int id)
    {
        ShiftDataContext db = new();
        var shift = db.Shifts.Single(x => x.Id == id);
        db.Remove(shift);
        db.SaveChanges();
    }

    public static void Update(Shift shift)
    {
        ShiftDataContext db = new();
        var shiftToUpdate = db.Shifts.Single(x => x.Id == shift.Id);
        shiftToUpdate.Duration = shift.Duration;
        shiftToUpdate.StartTime = shift.StartTime;
        shiftToUpdate.EndTime = shift.EndTime;
        shiftToUpdate.Name = shift.Name;
        db.Update(shiftToUpdate);
        db.Entry(shiftToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        db.SaveChanges();
    }
}
