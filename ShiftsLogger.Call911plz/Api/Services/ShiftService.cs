
public interface IShiftService
{
    public List<Shift>? GetShiftsByWorkerId(int workerId);
    public Shift? GetShiftByShiftId(int workerId, int id);
    public Shift? CreateShift(Shift shift);
    public Shift? UpdateShift(Shift shift);
    public string? DeleteShift(int workerId, int id);
    public List<Shift>? GetAllShifts();
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
        if (DoesWorkerExists(shift.WorkerId) == false)
            return null;

        var savedShift = _dbContext.Shifts.Add(shift);
        _dbContext.SaveChanges();
        return savedShift.Entity;
    }

    public List<Shift>? GetShiftsByWorkerId(int workerId)
    {
        if (DoesWorkerExists(workerId) == false)
            return null;
        
        return _dbContext.Shifts.Where(shift => shift.WorkerId == workerId).ToList();
    }

    public Shift? GetShiftByShiftId(int workerId, int id)
    {   
        List<Shift>? shiftsOfWorker = GetShiftsByWorkerId(workerId);
        if (shiftsOfWorker == null)
            return null;

        // Does not need a shift check as it does not alter db so can return null
        Shift? savedShift = shiftsOfWorker.Find(shift => shift.Id == id);
        return savedShift;
    }

    public string? DeleteShift(int workerId, int id)
    {
        Shift? savedShift = GetShiftByShiftId(workerId, id);
        if (savedShift == null)
            return null; 
        
        _dbContext.Shifts.Remove(savedShift);
        _dbContext.SaveChanges();
        return $"Successfully deleted shift with id: {id}";
    }

    public Shift? UpdateShift(Shift shift)
    {
        Shift? savedShift = GetShiftByShiftId(shift.WorkerId, shift.Id);
        if (savedShift == null)
            return null;

        _dbContext.Entry(savedShift).CurrentValues.SetValues(shift);
        _dbContext.SaveChanges();
        return savedShift;
    }

    // Debug/Admin purposes only
    public List<Shift>? GetAllShifts()
    {
        List<Shift>? shifts = _dbContext.Shifts.ToList();
        return shifts;
    }

    private bool DoesWorkerExists(int workerId)
    {
        return _dbContext.Workers.Where(worker => worker.WorkerId == workerId).Any();
    }
}