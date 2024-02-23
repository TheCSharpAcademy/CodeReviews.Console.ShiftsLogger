using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebAPI.Models;

namespace ShiftsLoggerWebAPI.Services;

public class ShiftService : IShiftService
{
	private readonly ShiftContext _context;

	public ShiftService(ShiftContext context)
	{
		_context = context;
	}

	public async Task<Shift> CreateShiftAsync(Shift shift)
	{
		_context.Shifts.Add(shift);
		await _context.SaveChangesAsync();

		return shift;
	}

	public async Task DeleteShiftAsync(long id)
	{
		var shift = await _context.Shifts.FindAsync(id);
		if (shift == null)
		{
			throw new KeyNotFoundException($"Shift with ID {id} not found.");
		}

		_context.Shifts.Remove(shift);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
	{
		return await _context.Shifts.ToListAsync();
	}

	public async Task<Shift> GetShiftByIdAsync(long id)
	{
		return await _context.Shifts.FindAsync(id);
	}

	public async Task UpdateShiftAsync(long id, Shift shift)
	{
		if (id != shift.Id)
		{
			throw new BadRequestException("Shift ID mismatch.");
		}

		_context.Entry(shift).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!ShiftExists(id))
			{
				throw new KeyNotFoundException($"Shift with ID {id} not found.");
			}
			else
			{
				throw;
			}
		}
	}

	private bool ShiftExists(long id)
	{
		return _context.Shifts.Any(e => e.Id == id);
	}
}
