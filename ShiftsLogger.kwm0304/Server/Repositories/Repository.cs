using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repositories.Interfaces;

namespace Server.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
  private readonly ShiftLoggerContext _context;
  private readonly DbSet<T> _dbSet;
  public Repository(ShiftLoggerContext context)
  {
    _context = context;
    _dbSet = _context.Set<T>();
  }

  public async Task CreateAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteByIdAsync(int id)
  {
    var entity = await _dbSet.FindAsync(id);
    if (entity != null)
    {
      _dbSet.Remove(entity);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<List<T>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public async Task<T?> GetByIdAsync(int id)
  {
    return await _dbSet.FindAsync(id);
  }

  public async Task UpdateAsync(T entity)
  {
    _context.Entry(entity).State = EntityState.Modified;
    await _context.SaveChangesAsync();
  }
}