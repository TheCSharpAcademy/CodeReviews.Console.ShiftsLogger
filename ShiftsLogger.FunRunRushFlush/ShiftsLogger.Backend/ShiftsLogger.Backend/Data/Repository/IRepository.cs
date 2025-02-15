using ShiftsLogger.Domain;

namespace ShiftsLogger.Backend.Data;

public interface IRepository<TEntity> where TEntity : Entity
{
    ValueTask<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    ValueTask<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    ValueTask CreateAsync(TEntity entity, CancellationToken cancellationToken);
    ValueTask<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    ValueTask<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}