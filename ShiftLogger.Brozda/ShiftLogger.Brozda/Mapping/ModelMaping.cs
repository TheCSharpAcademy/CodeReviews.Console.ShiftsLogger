using ShiftLogger.Brozda.API.Models;
using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Mapping
{
    /// <summary>
    /// Provides means to map DB models to shared DTO models
    /// </summary>
    public static class ModelMaping
    {
        public static WorkerDto? ToDto(this Worker? entity)
        {
            if (entity is null)
                return null;

            return new WorkerDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public static Worker FromDto(this WorkerDto entity)
        {
            return new Worker
            {
                Name = entity.Name,
            };
        }

        public static AssignedShiftDto? ToDto(this AssignedShift? entity)
        {
            if (entity is null)
                return null;

            return new AssignedShiftDto
            {
                Id = entity.Id,
                WorkerId = entity.WorkerId,
                ShiftTypeId = entity.ShiftTypeId,
                Date = entity.Date,
            };
        }

        public static AssignedShift FromDto(this AssignedShiftDto entity)
        {
            return new AssignedShift
            {
                WorkerId = entity.WorkerId,
                ShiftTypeId = entity.ShiftTypeId,
                Date = entity.Date,
            };
        }

        public static ShiftTypeDto? ToDto(this ShiftType? entity)
        {
            if (entity is null)
                return null;

            return new ShiftTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
            };
        }

        public static ShiftType FromDto(this ShiftTypeDto entity)
        {
            return new ShiftType
            {
                Name = entity.Name,
                Description = entity.Description,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
            };
        }
    }
}