namespace ShiftsLoggerAPI.Data;

public class DbInitialiser
{
    private readonly ShitftsLoggerDbContext _context;

    public DbInitialiser(ShitftsLoggerDbContext context)
    {
        _context = context;
    }

    public async Task RunAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }
}