using ShiftLogger.Brozda.API.Models;
using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Mapping
{
    /// <summary>
    /// Provides means to map DB models to shared DTO models
    /// Do not map Id in oposite to standard mapping
    /// </summary>
    public static class UpdateMapping
    {
        public static void MapUpdateFromDto(this ShiftType entity, ShiftTypeDto dto)
        {
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;
        }

        public static void MapUpdateFromDto(this AssignedShift entity, AssignedShiftDto dto)
        {
            entity.Date = dto.Date;
            entity.ShiftTypeId = dto.ShiftTypeId;
        }

        public static void MapUpdateFromDto(this Worker entity, WorkerDto dto)
        {
            entity.Name = dto.Name;
        }
    }
}