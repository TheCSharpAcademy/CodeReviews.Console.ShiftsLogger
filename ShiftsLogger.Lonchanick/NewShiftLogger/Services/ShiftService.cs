using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Lonchanick.ContextDataBase;
using ShiftsLogger.Lonchanick.Models;

namespace ShiftsLogger.Lonchanick.Services;

public class ShiftService: IShiftService
{
    ContextDB contexdb;

    public ShiftService(ContextDB contexdb)
    {
        this.contexdb = contexdb;
    }

    public async Task<IEnumerable<Shift>> getShifts()
    {
        var shifts = contexdb.Shifts.Include(p=>p.Worker);
        return shifts;
    }

    public async Task SaveShift(Shift shift)
    {
        contexdb.Shifts.Add(shift);
        await contexdb.SaveChangesAsync();

    }

    public async Task UpdateShift(int id, Shift shift)
    {
        Shift? ToLookingFor = contexdb.Shifts.Find(id);

        if(ToLookingFor is not null)
        {
            ToLookingFor.CheckTypeField = shift.CheckTypeField;
            await contexdb.SaveChangesAsync();

        }
    }

    public async Task DeleteShift(int id)
    {
        Shift? ToLookingFor = contexdb.Shifts.Find(id);
        if (ToLookingFor is not null)
        {
            contexdb.Remove(ToLookingFor);
            await contexdb.SaveChangesAsync();
        }
    }

}

public interface IShiftService
{
    public Task DeleteShift(int id);
    public Task UpdateShift(int id, Shift shift);
    public Task SaveShift(Shift shift);
    public Task<IEnumerable<Shift>> getShifts();

}
