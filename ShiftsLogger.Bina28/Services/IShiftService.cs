using ShiftsLogger.Bina28.Models;

namespace ShiftsLogger.Bina28.Services;

public interface IShiftService
{	public List<Shift> GetAll();
	public Shift? GetById(int id);
	public Shift Create(Shift shift);
	public Shift Update(int id, Shift updatedShift);
	public string? Delete(int id);
}
