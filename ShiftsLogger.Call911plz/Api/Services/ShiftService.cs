
public interface IShiftService
{
    public List<Shift> GetAllShifts(int workerId);
    public Shift? GetShiftById(int workerId, int id);
    public Shift? CreateShift(Shift shift);
    public Shift? UpdateShift(Shift shift);
    public string? DeleteShift(int id);
}

public class ShiftService : IShiftService
{
    private readonly ShiftsLoggerDbContext _dbContext;

    public ShiftService(ShiftsLoggerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Shift? CreateShift(Shift shift)
    {
        if (_dbContext.Workers.Find(shift.WorkerId) == null)
            return null;

        var savedShift = _dbContext.Shifts.Add(shift);
        _dbContext.SaveChanges();
        return savedShift.Entity;
    }

    public string? DeleteShift(int id)
    {
        Shift? savedShift = _dbContext.Shifts.Find(id);

        if (savedShift == null)
            return null;
        
        _dbContext.Shifts.Remove(savedShift);
        
        _dbContext.SaveChanges();

        return $"Successfully deleted shift with id: {id}";
    }

    public List<Shift> GetAllShifts(int workerId)
    {
        return _dbContext.Shifts.Where(shift => shift.WorkerId == workerId).ToList();
    }

    public Shift? GetShiftById(int workerId, int id)
    {
        Shift? savedShift = _dbContext.Shifts.Find(workerId, id);
        return savedShift;
    }

    public Shift? UpdateShift(Shift shift)
    {
        if (_dbContext.Workers.Find(shift.WorkerId) == null)
            return null;
        
        Shift? savedShift = _dbContext.Shifts.Find(shift.Id);

        if (savedShift == null)
            return null;

        _dbContext.Entry(savedShift).CurrentValues.SetValues(shift);
        _dbContext.SaveChanges();

        return savedShift;
    }
}