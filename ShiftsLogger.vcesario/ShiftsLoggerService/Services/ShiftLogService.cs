public class ShiftLogService : IShiftLogService
{
    private readonly ShiftLogsDbContext m_DbContext;

    public ShiftLogService(ShiftLogsDbContext context)
    {
        m_DbContext = context;
    }

    public List<Shift> GetShifts()
    {
        return m_DbContext.Shifts.ToList();
    }
}