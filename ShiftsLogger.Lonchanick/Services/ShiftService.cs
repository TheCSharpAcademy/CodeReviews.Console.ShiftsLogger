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

    public IEnumerable<Shift> getShifts()
    {
        return contexdb.Shifts;
    }

    public async Task SaveShift(Shift shift)
    {
        contexdb.Shifts.Add(shift);
        await contexdb.SaveChangesAsync();

    }

    public async Task UpdateShift(Guid id, Shift shift)
    {
        Shift? ToLookingFor = contexdb.Shifts.Find(id);

        if(ToLookingFor is not null)
        {
            ToLookingFor.CheckTypeField = shift.CheckTypeField;
            await contexdb.SaveChangesAsync();

        }
    }

    public async Task DelteShift(Guid id)
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
    public Task DelteShift(Guid id);
    public Task UpdateShift(Guid id, Shift shift);
    public Task SaveShift(Shift shift);
    public IEnumerable<Shift> getShifts();
}
