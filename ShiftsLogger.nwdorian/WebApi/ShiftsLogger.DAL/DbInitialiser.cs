using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.DAL;
public class DbInitialiser
{
	private readonly ShiftsContext _context;

	public DbInitialiser(ShiftsContext context)
	{
		_context = context;
	}

	public async Task RunAsync()
	{
		await _context.Database.MigrateAsync();
	}
}
