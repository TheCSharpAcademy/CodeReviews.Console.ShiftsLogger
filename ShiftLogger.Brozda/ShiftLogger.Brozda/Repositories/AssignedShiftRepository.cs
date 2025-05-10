using Microsoft.EntityFrameworkCore;
using ShiftLogger.Brozda.API.Data;
using ShiftLogger.Brozda.API.Mapping;
using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Repositories
{
    /// <summary>
    /// Handles access and modification to Assigned Shifts table
    /// Implements <see cref="IAssignedShiftRepository"/>
    /// </summary>
    public class AssignedShiftRepository : IAssignedShiftRepository
    {
        public readonly ShiftLoggerContext DbContext;

        /// <summary>
        /// Initializes new instance of <see cref="AssignedShiftRepository"/>
        /// </summary>
        /// <param name="dbContext"><see cref="ShiftLoggerContext"/> used for Db operations</param>

        public AssignedShiftRepository(ShiftLoggerContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<AssignedShiftDto> Add(AssignedShiftDto dto)
        {
            var entity = dto.FromDto();

            if (entity is null)
                throw new ArgumentException("Invalid DTO in create method");

            await DbContext.AssignedShifts.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity.ToDto()!;
        }

        /// <inheritdoc/>
        public async Task<List<AssignedShiftDto>> GetAll()
        {
            var entities = await DbContext.AssignedShifts.ToListAsync();

            return entities
                .Select(s => s.ToDto()!)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<List<AssignedShiftDto>> GetAllForWorker(int id)
        {
            var entities = await DbContext.AssignedShifts.Where(x => x.WorkerId == id).ToListAsync();

            return entities
                .Select(s => s.ToDto()!)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<List<AssignedShiftMappedDto>> GetAllForWorkerMapped(int id)
        {
            var entities = await DbContext.AssignedShifts
                .Where(s => s.WorkerId == id)
                .Include(s => s.Worker)
                .Include(s => s.ShiftType)
                .Select(s => new AssignedShiftMappedDto()
                {
                    Id = s.Id,
                    WorkerName = s.Worker.Name,
                    ShiftTypeName = s.ShiftType.Name,
                    Date = s.Date,
                    StartTime = s.ShiftType.StartTime,
                    EndTime = s.ShiftType.EndTime,
                }).ToListAsync();

            return entities;
        }

        /// <inheritdoc/>
        public async Task<List<AssignedShiftMappedDto>> GetShiftsForDateMapped(DateTime date)
        {
            var entities = await DbContext.AssignedShifts
                .Where(s => (s.Date.Year == date.Year && s.Date.Month == date.Month && s.Date.Day == date.Day))
                .Include(s => s.Worker)
                .Include(s => s.ShiftType).Select(s => new AssignedShiftMappedDto()
                {
                    Id = s.Id,
                    WorkerName = s.Worker.Name,
                    ShiftTypeName = s.ShiftType.Name,
                    Date = s.Date,
                    StartTime = s.ShiftType.StartTime,
                    EndTime = s.ShiftType.EndTime,
                }).ToListAsync();

            return entities;
        }

        /// <inheritdoc/>
        public async Task<AssignedShiftDto?> GetById(int id)
        {
            var entity = await DbContext.AssignedShifts.FindAsync(id);

            return entity.ToDto();
        }

        /// <inheritdoc/>
        public async Task<AssignedShiftDto?> Update(AssignedShiftDto dto, int id)
        {
            var entity = await DbContext.AssignedShifts.FindAsync(id);

            if (entity is null)
                return null;

            entity.MapUpdateFromDto(dto);
            await DbContext.SaveChangesAsync();

            return entity.ToDto();
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteById(int id)
        {
            var affectedRows = await DbContext.AssignedShifts
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return affectedRows > 0;
        }
    }
}