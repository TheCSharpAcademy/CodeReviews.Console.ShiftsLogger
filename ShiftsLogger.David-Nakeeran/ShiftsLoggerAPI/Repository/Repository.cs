using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;

namespace ShiftsLoggerAPI.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entityToBeDeleted = await _dbSet.FindAsync(id);
            if (entityToBeDeleted != null)
            {
                _dbSet.Remove(entityToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while deleting a shift", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred when trying to delete", ex);
        }
    }
}