public class ShiftLogService : IShiftLogService
{
    private readonly ShiftLogsDbContext m_DbContext;

    public ShiftLogService(ShiftLogsDbContext context)
    {
        m_DbContext = context;
    }

    public Shift CreateShift(Shift shift)
    {
        var newShift = m_DbContext.Shifts.Add(shift);
        m_DbContext.SaveChanges();
        return newShift.Entity;
    }

    public string? DeleteShift(int id)
    {
        Shift? shift = m_DbContext.Shifts.Find(id);

        if (shift == null)
        {
            return null;
        }

        m_DbContext.Shifts.Remove(shift);
        m_DbContext.SaveChanges();

        return $"Successfully deleted shift with id: {id}";
    }

    public Shift? GetShiftById(int id)
    {
        Shift? shift = m_DbContext.Shifts.Find(id);
        return shift == null ? null : shift;
    }

    public List<Shift> GetShifts()
    {
        return m_DbContext.Shifts.ToList();
    }

    public Shift? UpdateShift(Shift updatedShift)
    {
        Shift? shift = m_DbContext.Shifts.Find(updatedShift.Id);

        if (shift == null)
        {
            return null;
        }

        m_DbContext.Entry(shift).CurrentValues.SetValues(updatedShift);
        m_DbContext.SaveChanges();

        return shift;
    }
}