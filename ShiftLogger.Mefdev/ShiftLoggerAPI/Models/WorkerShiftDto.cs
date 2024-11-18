namespace ShiftLogger.Mefdev.ShiftLoggerAPI.Models;

public record WorkerShiftDto(int Id, string Name, DateTime Start,  DateTime End, TimeSpan? Duration=null);