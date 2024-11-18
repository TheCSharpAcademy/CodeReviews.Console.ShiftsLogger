namespace ShiftLogger.Mefdev.ShiftLoggerUI.Dtos;

public record WorkerShiftDto(int Id, string Name, DateTime Start,  DateTime End, TimeSpan Duration=default);