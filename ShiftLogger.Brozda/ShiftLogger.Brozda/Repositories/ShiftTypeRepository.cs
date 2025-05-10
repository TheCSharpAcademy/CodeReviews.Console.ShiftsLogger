using Microsoft.EntityFrameworkCore;

using ShiftLogger.Brozda.API.Data;
using ShiftLogger.Brozda.API.Mapping;
using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Repositories
{
    /// <summary>
    /// Handles access and modification to Shift Types table
    /// Implements <see cref="IShiftTypeRepository"/>
    /// </summary>
    public class ShiftTypeRepository : IShiftTypeRepository
    {
        public readonly ShiftLoggerContext DbContext;

        /// <summary>
        /// Initializes new instance of <see cref="ShiftTypeRepository"/>
        /// </summary>
        /// <param name="dbContext"><see cref="ShiftLoggerContext"/> used for Db operations</param>
        public ShiftTypeRepository(ShiftLoggerContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ShiftTypeDto> Add(ShiftTypeDto dto)
        {
            var entity = dto.FromDto();

            if (entity is null)
                throw new ArgumentException("Invalid DTO in create method");

            await DbContext.ShiftTypes.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity.ToDto()!;
        }

        /// <inheritdoc/>
        public async Task<List<ShiftTypeDto>> GetAll()
        {
            var entities = await DbContext.ShiftTypes.ToListAsync();
            return entities
                .Select(x => x.ToDto()!)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<ShiftTypeDto?> GetById(int id)
        {
            var entity = await DbContext.ShiftTypes.FindAsync(id);
            return entity.ToDto();
        }

        /// <inheritdoc/>
        public async Task<ShiftTypeDto?> Update(ShiftTypeDto dto, int id)
        {
            var entity = await DbContext.ShiftTypes.FindAsync(id);

            if (entity is null)
                return null;

            entity.MapUpdateFromDto(dto);

            await DbContext.SaveChangesAsync();

            return entity.ToDto();
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteById(int id)
        {
            var affectedRows = await DbContext.ShiftTypes
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return affectedRows > 0;
        }
    }
}