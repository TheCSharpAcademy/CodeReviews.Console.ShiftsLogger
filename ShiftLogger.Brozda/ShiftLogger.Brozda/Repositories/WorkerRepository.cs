using Microsoft.EntityFrameworkCore;
using ShiftLogger.Brozda.API.Data;
using ShiftLogger.Brozda.API.Mapping;
using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Repositories
{
    /// <summary>
    /// Handles access and modification to Workers table
    /// Implements <see cref="IWorkerRepository"/>
    /// </summary>
    public class WorkerRepository : IWorkerRepository
    {
        public readonly ShiftLoggerContext DbContext;

        /// <summary>
        /// Initializes new instance of <see cref="WorkerRepository"/>
        /// </summary>
        /// <param name="dbContext"><see cref="ShiftLoggerContext"/> used for Db operations</param>
        public WorkerRepository(ShiftLoggerContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<WorkerDto> Add(WorkerDto dto)
        {
            var entity = dto.FromDto();

            if (entity is null)
                throw new ArgumentException("Invalid DTO in create method");
            await DbContext.Workers.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity.ToDto()!;
        }

        /// <inheritdoc/>
        public async Task<List<WorkerDto>> GetAll()
        {
            var entities = await DbContext.Workers.ToListAsync();
            return entities
                .Select(x => x.ToDto()!)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<WorkerDto?> GetById(int id)
        {
            var entity = await DbContext.Workers.FindAsync(id);

            return entity.ToDto();
        }

        /// <inheritdoc/>
        public async Task<WorkerDto?> Update(WorkerDto dto, int id)
        {
            var entity = await DbContext.Workers.FindAsync(id);

            if (entity is null)
                return null;

            entity.MapUpdateFromDto(dto);

            await DbContext.SaveChangesAsync();

            return entity.ToDto();
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteById(int id)
        {
            var affectedRows = await DbContext.Workers
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            await DbContext.SaveChangesAsync();

            return affectedRows > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DoesWorkerExist(int id)
        {
            return await DbContext.Workers.AnyAsync(x => x.Id == id);
        }
    }
}