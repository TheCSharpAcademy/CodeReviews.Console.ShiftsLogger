using ShiftsLogger.Bina28.Data;
using ShiftsLogger.Bina28.Models;

namespace ShiftsLogger.Bina28.Services;

public class ShiftService : IShiftService{

	private readonly ShiftsDbContext _dbContext;
	public ShiftService(ShiftsDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public Shift Create(Shift shift)
	{
		var savedShift = _dbContext.Shifts.Add(shift);
		_dbContext.SaveChanges();
		return savedShift.Entity;
	}

	public string? Delete(int id)
	{
		var savedShift = _dbContext.Shifts.Find(id);
		if (savedShift == null)
		{
			return "Shift not found";
		}
		_dbContext.Shifts.Remove(savedShift);
		_dbContext.SaveChanges();
		return $"Shift with id: {id} deleted successfully";
	}

	public List<Shift> GetAll()
	{
		return _dbContext.Shifts.ToList();
	}

	public Shift? GetById(int id)
	{
		var savedShift = _dbContext.Shifts.Find(id);
		if (savedShift == null)
		{
			return null;
		}
		else return savedShift;
	}

	public Shift Update(int id, Shift updatedShift)
	{
		var savedShift = _dbContext.Shifts.Find(id);
		if (savedShift == null)
		{
			return null;
		}

		savedShift.EmployeeId = updatedShift.EmployeeId;
		savedShift.StartTime = updatedShift.StartTime;
		savedShift.EndTime = updatedShift.EndTime;
		savedShift.ShiftType = updatedShift.ShiftType;
		savedShift.Notes = updatedShift.Notes;

		_dbContext.SaveChanges();
		return savedShift;
	}
}
