using ShiftsLoggerV2.RyanW84.Models;

namespace ShiftsLoggerV2.RyanW84.Services;

public interface IShiftService
	{
	public List<Shift> GetAllShifts( );
	public Shift? GetShiftById( int id );
	public Shift CreateShift(Shift shift );
	public Shift? UpdateShift(int id , Shift updatedShift);
	public string? DeleteShift(int id);
	}
