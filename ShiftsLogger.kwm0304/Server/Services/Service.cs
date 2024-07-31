using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Services;

public class Service<T>(IRepository<T> repository) : IService<T> where T : class
{
  private readonly IRepository<T> _repository = repository;

  public async Task AddAsync(T entity)
  {
    await _repository.CreateAsync(entity);
  }

  public async Task DeleteAsync(int id)
  {
    await _repository.DeleteByIdAsync(id);
  }

  public async Task<List<T>> GetAllAsync()
  {
    return await _repository.GetAllAsync();
  }

  public async Task<T> GetByIdAsync(int id)
  {
    return await _repository.GetByIdAsync(id);
  }

  public async Task UpdateAsync(T entity)
  {
    await _repository.UpdateAsync(entity);
  }
}